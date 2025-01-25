using System;
using UnityEngine;
using Utility;

namespace Bubbles.Input
{
    public class PickupDragHandler : MonoBehaviour
    {
        public Interactable InteractableUnderMouse => _interactableUnderMouse;
        private Interactable _interactableUnderMouse;

        private void Update()
        {
            _interactableUnderMouse = CanvasUtility.DetectUnderCursor<Interactable>();
        }
    }
}