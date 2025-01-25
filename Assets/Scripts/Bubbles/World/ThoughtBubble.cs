using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public class ThoughtBubble : Interactable
    {
        [field: SerializeField] public ThoughtBubbleStateAsset State { get; private set; }

        public override List<ItemAsset> GetReceivableItems()
        {
            return State.TransitionsByAddItem.Keys.ToList();
        }

        public override Image GetImageToHighlight()
        {
            return State.Scene.ImageToHighlight;
        }

        public void SetHighlight(HighlightState state)
        {
            State.Scene.SetHighlight(state);
        }
    }
}