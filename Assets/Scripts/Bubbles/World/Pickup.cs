using System;
using Bubbles.Graphics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bubbles
{
    [RequireComponent(typeof(Image))] // For raycasting purposes only. Set alpha to 0.
    public class Pickup : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static event Action<Pickup> OnPickup;
        public static event Action<Pickup> OnRelease;
        public event Action OnHover;
        public event Action OnClick;
        [field: SerializeField] public ItemAsset Item { get; private set; }
        [SerializeField] private PickupGraphics _graphics;
        private bool _isDragging;
        
        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"Dragging pickup: {Item.name}");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _graphics.SetGraphics(PickupState.Absent);
            OnPickup?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            _graphics.SetGraphics(PickupState.AtRest);
            OnRelease?.Invoke(this);
        }

        public void Hover()
        {
            if (_isDragging) return;
            _graphics.SetGraphics(PickupState.Hover);
            OnHover?.Invoke();
        }

        public void Click()
        {
            if (_isDragging) return;
            _graphics.SetGraphics(PickupState.Click);
            OnClick?.Invoke();
        }

        public void Unhover()
        {
            if (_isDragging) return;
            _graphics.SetGraphics(PickupState.AtRest);
        }
    }

    [Serializable]
    public enum PickupState
    {
        Absent, AtRest, Hover, Click
    }
}