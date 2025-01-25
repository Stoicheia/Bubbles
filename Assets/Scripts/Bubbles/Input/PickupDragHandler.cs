using System;
using UnityEngine;
using Utility;

namespace Bubbles.InteractableInput
{
    public class PickupDragHandler : MonoBehaviour
    {
        public event Action<Pickup> OnStartPickup;
        public event Action<Pickup> OnEndPickup;
        
        public Interactable InteractableUnderMouse { get; private set; }
        public Pickup PickupUnderMouse { get; private set; }

        private void OnEnable()
        {
            Pickup.OnPickup += HandleStartPickup;
            Pickup.OnRelease += HandleEndPickup;
        }
        private void OnDisable()
        {
            Pickup.OnPickup -= HandleStartPickup;
            Pickup.OnRelease -= HandleEndPickup;
        }
        


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

            if (Input.GetMouseButtonUp(0))
            {
                if (PickupUnderMouse != null)
                {
                    PickupUnderMouse.Hover();
                }
            }
        }
        
                
        private void HandleStartPickup(Pickup pickup)
        {
            OnStartPickup?.Invoke(pickup);
        }
        
        private void HandleEndPickup(Pickup pickup)
        {
            OnEndPickup?.Invoke(pickup);
        }

    }
}