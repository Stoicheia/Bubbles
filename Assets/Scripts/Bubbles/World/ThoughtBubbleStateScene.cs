using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles
{
    // Prefab that goes into ThoughtBubbleStateAsset
    public class ThoughtBubbleStateScene : SerializedMonoBehaviour
    {
        [SerializeField] private List<Pickup> _pickups;
    }
}