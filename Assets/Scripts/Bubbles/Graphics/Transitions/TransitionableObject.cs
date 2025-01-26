using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public abstract class TransitionableObject : SerializedMonoBehaviour
    {
        protected Coroutine ActiveAnimation { get; private set; }

        [Button]
        public void TransitionIn(Ease easeIn, Ease easeOut, float duration)
        {
            if (ActiveAnimation != null)
            {
                StopCoroutine(ActiveAnimation);
            }

            StartCoroutine(TransitionInSeq(easeIn, easeOut, duration));
        }

        [Button]
        public void TransitionOut(Ease easeIn, Ease easeOut, float duration)
        {
            if (ActiveAnimation != null)
            {
                StopCoroutine(ActiveAnimation);
            }

            StartCoroutine(TransitionOutSeq(easeIn, easeOut, duration));
        }

        public abstract IEnumerator TransitionInSeq(Ease easeIn, Ease easeOut, float duration);
        public abstract IEnumerator TransitionOutSeq(Ease easeIn, Ease easeOut, float duration);
    }
}