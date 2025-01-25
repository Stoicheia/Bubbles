using System;
using System.Collections.Generic;
using Bubbles.Manager;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "Thought Bubble State", menuName = "Thought Bubble State", order = 0)]
    public class ThoughtBubbleStateAsset : SerializedScriptableObject
    {
        [field: SerializeField] public bool IsInteractable { get; private set; }
        [field: SerializeField] public ThoughtBubbleStateScene Scene { get; private set; }
        [field: OdinSerialize] public Dictionary<ItemAsset, ThoughtBubbleStateAsset> TransitionsByAddItem { get; private set; }
        [field: OdinSerialize] public Dictionary<ItemAsset, ThoughtBubbleStateAsset> TransitionsByRemoveItem { get; private set; }
        [field: OdinSerialize] public Dictionary<ReactionInput, ThoughtBubbleStateAsset> TransitionsByReaction { get; private set; }
        [ReadOnly] [SerializeField] private List<ItemAsset> _itemsInScene;

        private void OnValidate()
        {
            if (Scene == null) return;
            _itemsInScene = Scene.GetItemsInScene();
            foreach (ItemAsset item in _itemsInScene)
            {
                TransitionsByRemoveItem.TryAdd(item, null);
            }
        }
    }

    [Serializable]
    public struct ReactionInput
    {
        public InteractableSlot Slot;
        public ItemAsset Item;
    }
}