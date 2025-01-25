using System;
using Bubbles.InteractableInput;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Bubbles.Graphics
{
    public class PickupInTransit : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _imageField;
        [SerializeField] private PickupDragHandler _pickupDragHandler;

        private RectTransform _imgTransform;

        private void OnEnable()
        {
            _imgTransform = _imageField.GetComponent<RectTransform>();
            _pickupDragHandler.OnStartPickup += HandleStartPickup;
            _pickupDragHandler.OnEndPickup += HandleEndPickup;
        }
        
        private void Awake()
        {
            _imageField.raycastTarget = false;
        }

        private void Update()
        {
            ImageFollowMouse();
        }
        
        public void Display(ItemAsset item)
        {
            _imageField.enabled = true;
            _imageField.sprite = item.SpriteInTransit;
        }

        public void Disable()
        {
            _imageField.enabled = false;
        }
        
        private void HandleStartPickup(Pickup pickup)
        {
            Display(pickup.Item);
        }
        
        private void HandleEndPickup(Pickup pickup)
        {
            Disable();
        }

        private void ImageFollowMouse()
        {
            // ChatGPT
            // Convert the mouse position to a position in the Canvas space
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform, // The RectTransform of the Canvas
                Input.mousePosition,               // The current mouse position
                _canvas.worldCamera,                // Camera rendering the Canvas (null if Overlay)
                out mousePosition                  // The converted local position
            );

            // Set the position of the UI Image
            _imgTransform.anchoredPosition = mousePosition;
        }
        


    }
}