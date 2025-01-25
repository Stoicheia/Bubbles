using System;
using System.Collections.Generic;
using System.Linq;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.Manager
{
    public class InteractablesManager : SerializedMonoBehaviour
    {
        [field: SerializeField][field: ReadOnly] public List<Interactable> AllInteractables { get; private set; }
        [SerializeField] private PickupDragHandler _dragHandler;
        [SerializeField] [ReadOnly] private Interactable _interactableUnderMouse;

        private void Awake()
        {
            AllInteractables = FindObjectsByType<Interactable>(FindObjectsSortMode.InstanceID).ToList();
        }

        private void OnEnable()
        {
            _dragHandler.OnStartPickup += HandleStartPickup;
            _dragHandler.OnEndPickup += HandleReleasePickup;
        }
        
        private void OnDisable()
        {
            _dragHandler.OnStartPickup -= HandleStartPickup;
            _dragHandler.OnEndPickup -= HandleReleasePickup;
        }

        private void Update()
        {
            HandleDragOnInteractableHover();
        }

        private void HandleDragOnInteractableHover()
        {
            Pickup currentlyDragging = _dragHandler.CurrentlyDragging;
            if (currentlyDragging == null) return;
            Interactable detectedUnderMouse = _dragHandler.InteractableUnderMouse;
            bool underMouseChanged = detectedUnderMouse != _interactableUnderMouse;
            if (underMouseChanged)
            {
                if (detectedUnderMouse != null)
                {
                    if (detectedUnderMouse.CanInteract(currentlyDragging.Item))
                    {
                        var gfx = detectedUnderMouse.GetHighlighter();
                        if (detectedUnderMouse.CanInteract(currentlyDragging.Item))
                        {
                            gfx?.SetHighlightState(HighlightState.HoverYes);
                        }
                        else
                        {
                            gfx?.SetHighlightState(HighlightState.HoverNo);
                        }
                    }
                }

                if (_interactableUnderMouse != null)
                {
                    bool canInteract = _interactableUnderMouse.CanInteract(currentlyDragging.Item);
                    _interactableUnderMouse.GetHighlighter()?.SetHighlightState(canInteract ? HighlightState.CanInteract : HighlightState.Disabled);
                }
            }

            _interactableUnderMouse = detectedUnderMouse;
        }

        public IEnumerable<Interactable> GetActiveInteractables()
        {
            IEnumerable<Interactable> active = AllInteractables.Where(x => x.IsActive);
            return active;
        }
        
        private void HandleStartPickup(Pickup pickup, Vector2 _)
        {
            ItemAsset pickedItem = pickup.Item;
            IEnumerable<Interactable> validInteractables = GetActiveInteractables().Where(x => x.CanInteract(pickedItem));
            foreach (Interactable valid in validInteractables)
            {
                InteractableHighlight highlighter = valid.GetHighlighter();
                if (highlighter == null) continue;
                highlighter.SetHighlightState(HighlightState.CanInteract);
            }
        }
        
        private void HandleReleasePickup(Pickup pickup)
        {
            foreach (Interactable active in GetActiveInteractables())
            {
                InteractableHighlight highlighter = active.GetHighlighter();
                if (highlighter == null) continue;
                highlighter.SetHighlightState(HighlightState.Disabled);
            }
            
            ItemAsset item = pickup.Item;
            Interactable underMouse = _dragHandler.InteractableUnderMouse;
            if (underMouse == null) return;
            bool canInteract = underMouse.CanInteract(item);
            if (canInteract)
            {
                underMouse.Interact(item);
            }
        }
    }
}