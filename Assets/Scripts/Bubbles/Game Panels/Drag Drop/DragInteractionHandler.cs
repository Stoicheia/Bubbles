using System;
using Bubbles.GamePanels;
using Bubbles.Graphics;
using UnityEngine;
using Utility;

namespace Bubbles.InteractableInput
{
    public class DragInteractionHandler : MonoBehaviour
    {
        public event Action<PanelPickup> OnHoverPickup;
        public event Action<Panel> OnHoverPanel;
        public event Action<PanelPickup, Vector2> OnStartPickup;
        public event Action<PanelPickup> OnEndPickup;
        
        public Panel PanelUnderMouse { get; private set; }
        public PanelPickup PickupUnderMouse { get; private set; }
        public PanelPickup CurrentlyDragging { get; private set; }
        [SerializeField] private Canvas _canvas;
        [SerializeField] private PickupInTransit _transitor;
        [SerializeField] private GameSceneManager _gameSceneManager;

        private void OnEnable()
        {
            PanelPickup.OnPickup += HandleStartPickup;
            PanelPickup.OnRelease += HandleEndPickup;
            PanelUnderMouse = null;
            PickupUnderMouse = null;
        }
        private void OnDisable()
        {
            PanelPickup.OnPickup -= HandleStartPickup;
            PanelPickup.OnRelease -= HandleEndPickup;
        }
        
        private void Update()
        {
            if (_gameSceneManager.IsLocked) return;
            var interactableDetected = CanvasUtility.DetectUnderCursor<Panel>();
            var pickupDetected = CanvasUtility.DetectUnderCursor<PanelPickup>();

            if (CurrentlyDragging == null && !_gameSceneManager.IsLocked)
            {
                if (pickupDetected != PickupUnderMouse)
                {
                    if (PickupUnderMouse != null)
                    {
                        PickupUnderMouse.Unhover();
                    }

                    if (pickupDetected != null)
                    {
                        pickupDetected.Hover();
                        OnHoverPickup?.Invoke(pickupDetected);
                    }
                }
            }
            
            if (!_gameSceneManager.IsLocked)
            {
                if (interactableDetected != PanelUnderMouse)
                {
                    if (PanelUnderMouse != null)
                    {
                    }

                    if (interactableDetected != null)
                    {
                        OnHoverPanel?.Invoke(interactableDetected);
                    }
                }
            }

            PanelUnderMouse = interactableDetected;
            PickupUnderMouse = pickupDetected;

            if (CurrentlyDragging == null && !_gameSceneManager.IsLocked)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (PickupUnderMouse != null)
                    {
                        PickupUnderMouse.Click();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (PickupUnderMouse != null)
                    {
                        PickupUnderMouse.Hover();
                    }
                }
            }
        }
                
        private void HandleStartPickup(PanelPickup pickup, Vector2 startPos)
        {
            if (_gameSceneManager.IsLocked) return;
            CurrentlyDragging = pickup;
            OnStartPickup?.Invoke(pickup, startPos);
        }
        
        private void HandleEndPickup(PanelPickup pickup)
        {
            if (_gameSceneManager.IsLocked) return;
            CurrentlyDragging = null;
            OnEndPickup?.Invoke(pickup);
        }

    }
}