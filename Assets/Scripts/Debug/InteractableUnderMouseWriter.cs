using System;
using Bubbles;
using Bubbles.InteractableInput;
using TMPro;
using UnityEngine;

namespace DebugGraphics
{
    public class InteractableUnderMouseWriter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private TextMeshProUGUI _textFieldP;
        [SerializeField] private PickupDragHandler _mouseDragHandler;

        private void Update()
        {
            Interactable underMouse = _mouseDragHandler.InteractableUnderMouse;
            if (underMouse == null)
            {
                _textField.text = "Under Mouse: None";
            }
            else
            {
                _textField.text = $"Under Mouse: {underMouse.gameObject.name}";
            }

            Pickup pickup = _mouseDragHandler.PickupUnderMouse;
            if (pickup == null)
            {
                _textFieldP.text = "Under Mouse: None";
            }
            else
            {
                _textFieldP.text = $"Under Mouse: {pickup.Item.name}";
            }
        }
    }
}