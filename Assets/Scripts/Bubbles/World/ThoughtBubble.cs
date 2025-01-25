using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles
{
    public class ThoughtBubble : Interactable
    {
        [field: SerializeField] public ThoughtBubbleStateAsset State { get; private set; }
        
        
    }
}