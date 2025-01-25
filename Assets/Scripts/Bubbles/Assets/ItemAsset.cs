using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles
{
    [CreateAssetMenu(fileName = "Item Asset", menuName = "Item Asset", order = 0)]
    public class ItemAsset : SerializedScriptableObject
    {
        [field: OdinSerialize] public Sprite SpriteInTransit { get; private set; }
    }
}