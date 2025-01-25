using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles.Graphics
{
    [CreateAssetMenu(fileName = "Graphics Settings", menuName = "Graphics Settings", order = 0)]
    public class GraphicsSettings : SerializedScriptableObject
    {
        [Header("Interactable Highlight")] 
        [field: OdinSerialize] public Dictionary<HighlightState, Color> HighlightColors { get; private set; }
        [field: SerializeField] public float IntensityTransitionSeconds { get; private set; }
        [field: SerializeField] public float PulsateAmplitude { get; private set; }
        [field: SerializeField] public float PulsateFrequency { get; private set; }
    }
}