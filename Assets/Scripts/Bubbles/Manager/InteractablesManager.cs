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

        private void Awake()
        {
            AllInteractables = FindObjectsByType<Interactable>(FindObjectsSortMode.InstanceID).ToList();
        }

        private void OnEnable()
        {
            _dragHandler.OnStartPickup += HandleStartPickup;
            _dragHandler.OnEndPickup += HandleReleasePickup;
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
            ItemAsset item = pickup.Item;
            Interactable underMouse = _dragHandler.InteractableUnderMouse;
            if (underMouse == null) return;
            bool canInteract = underMouse.CanInteract(item);
            if (canInteract)
            {
                underMouse.Interact(item);
            }

            foreach (Interactable active in GetActiveInteractables())
            {
                InteractableHighlight highlighter = active.GetHighlighter();
                if (highlighter == null) continue;
                highlighter.SetHighlightState(HighlightState.CanInteract);
            }
        }
    }
}