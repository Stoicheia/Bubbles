﻿using System;
using Bubbles.GamePanels;
using UnityEngine;
using Utility;

namespace Bubbles.InteractableInput
{
    public class DragInteractionHandler : MonoBehaviour
    {
        public event Action<PanelPickup, Vector2> OnStartPickup;
        public event Action<PanelPickup> OnEndPickup;
        
        public Panel PanelUnderMouse { get; private set; }
        public PanelPickup PickupUnderMouse { get; private set; }
        public PanelPickup CurrentlyDragging { get; private set; }

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
            var interactableDetected = CanvasUtility.DetectUnderCursor<Panel>();
            var pickupDetected = CanvasUtility.DetectUnderCursor<PanelPickup>();
            
            if (pickupDetected != PickupUnderMouse)
            {
                if (PickupUnderMouse != null)
                {
                    PickupUnderMouse.Unhover();
                }

                if (pickupDetected != null)
                {
                    pickupDetected.Hover();
                }
            }
            
            PanelUnderMouse = interactableDetected;
            PickupUnderMouse = pickupDetected;

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
                
        private void HandleStartPickup(PanelPickup pickup, Vector2 startPos)
        {
            CurrentlyDragging = pickup;
            OnStartPickup?.Invoke(pickup, startPos);
        }
        
        private void HandleEndPickup(PanelPickup pickup)
        {
            CurrentlyDragging = null;
            OnEndPickup?.Invoke(pickup);
        }

    }
}