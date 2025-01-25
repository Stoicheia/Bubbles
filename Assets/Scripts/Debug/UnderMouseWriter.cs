using System;
using Bubbles;
using Bubbles.GamePanels;
using Bubbles.InteractableInput;
using TMPro;
using UnityEngine;

namespace DebugGraphics
{
    public class UnderMouseWriter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private TextMeshProUGUI _textFieldP;
        [SerializeField] private DragInteractionHandler _mouseDragHandler;

        private void Update()
        {
            Panel underMouse = _mouseDragHandler.PanelUnderMouse;
            if (underMouse == null)
            {
                _textField.text = "Interactable: None";
            }
            else
            {
                _textField.text = $"Interactable: {underMouse.gameObject.name}";
            }

            PanelPickup pickup = _mouseDragHandler.PickupUnderMouse;
            if (pickup == null)
            {
                _textFieldP.text = "Pickup: None";
            }
            else
            {
                _textFieldP.text = $"Pickup: {pickup.ParentID}";
            }
        }
    }
}