using System.Collections.Generic;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Bubbles.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    [RequireComponent(typeof(Image))] // For raycasting purposes only. Set alpha to 0.
    public abstract class Interactable : SerializedMonoBehaviour, IMouseInteractor
    {
        [field: SerializeField] public InteractableSlot InteractableSlot { get; private set; }
        [field: SerializeField] public bool IsActive { get; private set; }
        public abstract List<ItemAsset> GetReceivableItems();
        public abstract Image GetImageToHighlight();
        bool IMouseInteractor.IsActive()
        {
            return IsActive;
        }

        public bool CanInteract(ItemAsset item)
        {
            return GetReceivableItems().Contains(item);
        }
        public abstract bool Interact(ItemAsset item);

        public InteractableHighlight GetHighlighter()
        {
            return GetComponentInChildren<InteractableHighlight>();
        }
    }
}