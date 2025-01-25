using System.Collections.Generic;
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
    }
}