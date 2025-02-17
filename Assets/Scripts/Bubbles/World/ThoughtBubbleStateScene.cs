﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    // Prefab that goes into ThoughtBubbleStateAsset
    public class ThoughtBubbleStateScene : MonoBehaviour
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

        public List<Pickup> GetPickupsInScene()
        {
            return _pickupFields;
        }

        public List<ItemAsset> GetItemsInScene()
        {
            return _pickupFields.Select(x => x.Item).ToList();
        }
    }

    [Serializable]
    public enum HighlightState
    {
        Disabled, CanInteract, HoverYes, HoverNo
    }
}