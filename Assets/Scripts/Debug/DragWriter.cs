using System;
using Bubbles.InteractableInput;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DebugGraphics
{
    public class DragWriter : MonoBehaviour
    {
        [SerializeField] private DragInteractionHandler _dragHandler;
        [SerializeField] private TextMeshProUGUI _draggingField;

        private void Update()
        {
            var dragging = _dragHandler.CurrentlyDragging;

            if (dragging == null)
            {
                _draggingField.text = "Dragging: Nothing";
            }
            else
            {
                _draggingField.text = $"Dragging: {dragging.ParentID}";
            }
        }
    }
}