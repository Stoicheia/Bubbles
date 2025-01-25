using System;
using System.Collections;
using Bubbles.GamePanels;
using DG.Tweening;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public class PanelPopTransition : TransitionableObject
    {
        [Header("Dependencies")]
        [SerializeField] private PanelField _panelField;
        
        [Header("Settings")]
        [SerializeField] private Ease _easeIn;
        [SerializeField] private Ease _easeOut;
        [SerializeField] private float _duration;

        private void Start()
        {
            _panelField = GetComponent<PanelField>();
        }

        public override IEnumerator TransitionInSeq()
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.zero;
            Tween scaleIn = pTransform.DOScale(1, _duration).SetEase(_easeIn);
            yield return scaleIn.WaitForCompletion();
        }

        public override IEnumerator TransitionOutSeq()
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.one;
            Tween scaleIn = pTransform.DOScale(0, _duration).SetEase(_easeOut);
            yield return scaleIn.WaitForCompletion();
        }
        
        
        public void DisappearImmediately()
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.zero;
        }
    }
}