using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "Item Asset", menuName = "Item Asset", order = 0)]
    public class ItemAsset : SerializedScriptableObject
    {
        [field: OdinSerialize] public Sprite SpriteInTransit { get; private set; }
        [field: OdinSerialize] public Dictionary<ItemAsset, ItemAsset> EnvironmentTransitions { get; private set; }

        public bool CanInteractEnvironment(ItemAsset itemAsset)
        {
            return EnvironmentTransitions.ContainsKey(itemAsset);
        }

        public ItemAsset GetEnvironmentTransition(ItemAsset itemAsset)
        {
            return CanInteractEnvironment(itemAsset) ? EnvironmentTransitions[itemAsset] : null;
        }
    }
}