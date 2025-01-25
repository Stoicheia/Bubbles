using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public abstract class TransitionableObject : SerializedMonoBehaviour
    {
        protected Coroutine ActiveAnimation { get; private set; }

        [Button]
        public void TransitionIn()
        {
            if (ActiveAnimation != null)
            {
                StopCoroutine(ActiveAnimation);
            }

            StartCoroutine(TransitionInSeq());
        }

        [Button]
        public void TransitionOut()
        {
            if (ActiveAnimation != null)
            {
                StopCoroutine(ActiveAnimation);
            }

            StartCoroutine(TransitionOutSeq());
        }

        public abstract IEnumerator TransitionInSeq();
        public abstract IEnumerator TransitionOutSeq();
    }
}