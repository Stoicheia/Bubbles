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
        private void Start()
        {
            _panelField = GetComponent<PanelField>();
        }

        public override IEnumerator TransitionInSeq(Ease easeIn, Ease easeOut, float duration)
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.zero;
            Tween scaleIn = pTransform.DOScale(1, duration).SetEase(easeIn);
            yield return scaleIn.WaitForCompletion();
        }

        public override IEnumerator TransitionOutSeq(Ease easeIn, Ease easeOut, float duration)
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.one;
            Tween scaleIn = pTransform.DOScale(0, duration).SetEase(easeOut);
            yield return scaleIn.WaitForCompletion();
        }
        
        
        public void DisappearImmediately()
        {
            var pTransform = _panelField.transform;
            pTransform.localScale = Vector3.zero;
        }
    }
}