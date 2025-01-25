using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.Graphics
{
    public class InteractableHighlight : SerializedMonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _imageField;
        [ReadOnly] [SerializeField] private float _t;
        [ReadOnly] [SerializeField] private float _currentIntensity;
        [ReadOnly] [SerializeField] private Color _currentTargetColor;

        [Header("Graphics Settings")] 
        [OdinSerialize] private Dictionary<HighlightState, Color> _highlightColors;
        [SerializeField] private float _intensityTransitionSeconds;
        [SerializeField] private float _pulsateAmplitude;
        [SerializeField] private float _pulsateFrequency;

        private Coroutine _currentTransition;
        
        private void Update()
        {
            _t += Time.deltaTime;
            Color color = CalculateCurrentColor();
            _imageField.color = color;
        }

        [Button]
        public void SetHighlightState(HighlightState state)
        {
            if (_currentTransition != null)
            {
                StopCoroutine(_currentTransition);
            }
            if (state == HighlightState.Disabled)
            {
                StartCoroutine(DisableHighlightSequence());
            }
            else
            {
                StartCoroutine(EnableHighlightSequence(_highlightColors[state]));
            }
        }
        
        private Color CalculateCurrentColor()
        {
            float pulsateSine = Mathf.Sin(2 * Mathf.PI * _pulsateFrequency * _t); // [-1, 1]
            float pulsateDeviance = _currentIntensity * pulsateSine * _pulsateAmplitude;
            Color target = _currentTargetColor;
            float alpha = target.a * _currentIntensity + pulsateDeviance;
            float a01 = Mathf.Clamp01(alpha);
            return new Color(target.r, target.r, target.r, a01);
        }

        private IEnumerator EnableHighlightSequence(Color to)
        {
            _currentTargetColor = to;
            float startIntensity = _currentIntensity;
            float targetIntensity = 1;
            float secondsSinceStart = 0;
            while (secondsSinceStart < _intensityTransitionSeconds)
            {
                float t = secondsSinceStart / _intensityTransitionSeconds;
                secondsSinceStart += Time.deltaTime;
                float intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                _currentIntensity = intensity;
                yield return null;
            }
        }

        private IEnumerator DisableHighlightSequence()
        {
            _currentTargetColor = new Color(0, 0, 0, 0);
            float startIntensity = _currentIntensity;
            float targetIntensity = 0;
            float secondsSinceStart = 0;
            while (secondsSinceStart < _intensityTransitionSeconds)
            {
                float t = secondsSinceStart / _intensityTransitionSeconds;
                secondsSinceStart += Time.deltaTime;
                float intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
                _currentIntensity = intensity;
                yield return null;
            }
        }
    }
}