using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bubbles
{
    [RequireComponent(typeof(RectTransform))]
    public class Pickup : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public static event Action<Pickup> OnPickup;
        
        private Image _restingImageField;
        [field: SerializeField] public ItemAsset Item { get; private set; }

        public void ToggleRestingImage(bool on)
        {
            _restingImageField.enabled = on;
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnPickup?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}