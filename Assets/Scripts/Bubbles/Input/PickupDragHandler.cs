using System;
using UnityEngine;
using Utility;

namespace Bubbles.InteractableInput
{
    public class PickupDragHandler : MonoBehaviour
    {
        public Interactable InteractableUnderMouse { get; private set; }
        public Pickup PickupUnderMouse { get; private set; }

        private void Update()
        {
            var interactableDetected = CanvasUtility.DetectUnderCursor<Interactable>();
            var pickupDetected = CanvasUtility.DetectUnderCursor<Pickup>();

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
            
            InteractableUnderMouse = interactableDetected;
            PickupUnderMouse = pickupDetected;

            if (Input.GetMouseButtonDown(0))
            {
                if (PickupUnderMouse != null)
                {
                    PickupUnderMouse.Click();
                }
            }
        }
    }
}