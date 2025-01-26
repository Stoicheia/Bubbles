using System;
using Bubbles.GamePanels;
using Bubbles.InteractableInput;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Bubbles.Graphics
{
    public class PickupInTransit : MonoBehaviour
    {
        public RectTransform Rect => _imgTransform;
        public bool HasPickup => _imageField.enabled;
        
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _imageField;
        [SerializeField] private RectTransform _collisionField;
        [SerializeField] private DragInteractionHandler _pickupDragHandler;
        [SerializeField] private float _universalScale = 0.7f;

        private RectTransform _imgTransform;
        private Vector2 _lastDragOffset;

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
            ImageFollowMouse(_lastDragOffset);
        }
        
        public void Display(Sprite sprite)
        {
            _imageField.enabled = true;
            _imageField.sprite = sprite;
            _imageField.SetNativeSize();
            _collisionField.transform.localScale = Vector3.one * _universalScale;
        }
        
        private void MatchSize(RectTransform source, RectTransform destination)
        {
            // Copy anchors, pivot, and size
            destination.anchorMin = source.anchorMin;
            destination.anchorMax = source.anchorMax;
            destination.pivot = source.pivot;
            destination.sizeDelta = source.sizeDelta;
        }

        public void Disable()
        {
            _imageField.enabled = false;
        }
        
        private void HandleStartPickup(PanelPickup panelPickup, Vector2 offset)
        {
            Display(panelPickup.SpriteInTransit);
            _lastDragOffset = offset;
        }
        
        private void HandleEndPickup(PanelPickup panelPickup)
        {
            Disable();
        }

        private void ImageFollowMouse(Vector2 offset)
        {
            Vector3 offset3 = new Vector3(offset.x, offset.y, 0);
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
            Debug.Log(offset3);
        }
        


    }
}