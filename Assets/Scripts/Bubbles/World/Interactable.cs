using System.Collections.Generic;
using Bubbles.Manager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public abstract class Interactable : SerializedMonoBehaviour
    {
        [field: SerializeField] public InteractableSlot InteractableSlot { get; private set; }
        [field: SerializeField] public bool IsActive { get; private set; }
        public abstract List<ItemAsset> GetReceivableItems();
        public abstract Image GetImageToHighlight();
    }
}