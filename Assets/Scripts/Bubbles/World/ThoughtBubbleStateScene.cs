using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    // Prefab that goes into ThoughtBubbleStateAsset
    public class ThoughtBubbleStateScene : SerializedMonoBehaviour
    {
        [SerializeField] private List<Pickup> _pickupFields;
        [field: SerializeField] public Image ImageToHighlight { get; private set; }

        public void SetHighlight(HighlightState state)
        {
            switch (state)
            {
                case HighlightState.Disabled:
                    break;
                case HighlightState.CanInteract:
                    break;
                case HighlightState.HoverYes:
                    break;
                case HighlightState.HoverNo:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }

    [Serializable]
    public enum HighlightState
    {
        Disabled, CanInteract, HoverYes, HoverNo
    }
}