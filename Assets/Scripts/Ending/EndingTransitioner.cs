using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ending
{
    public class EndingTransitioner : MonoBehaviour
    {
        [Header("Dependencies")] 
        [SerializeField] private Image _whitePanel;
        [SerializeField] private RectTransform _everythingElseRoot;
        [SerializeField] private EndingPanel _endingPanel;
        [SerializeField] private EndingRepo _endingRepo;

        [Header("Settings")]
        [field: SerializeField] public float WaitBefore { get; private set; }
        [SerializeField] private float _halfDuration;
        [SerializeField] private float _waitDuration = 0.01f;
        [SerializeField] private Ease _easeIn;
        [SerializeField] private Ease _easeOut;

        public Coroutine TransitionToEnding(EndingAsset asset)
        {
            return StartCoroutine(EndingSequence(asset));
        }

        public Coroutine TransitionToEnding(int ending)
        {
            return StartCoroutine(EndingSequence(_endingRepo.GetEndingAsset(ending)));
        }

        private IEnumerator EndingSequence(EndingAsset asset)
        {
            asset.IsComplete = true;
            _whitePanel.gameObject.SetActive(true);
            var fadeIn = _whitePanel.DOColor(Color.white, _halfDuration).SetEase(_easeIn);
            yield return fadeIn.WaitForCompletion();
            _endingPanel.gameObject.SetActive(true);
            _everythingElseRoot.gameObject.SetActive(false);
            _endingPanel.Render(asset);
            yield return new WaitForSeconds(_waitDuration);
            var fadeOut = _whitePanel.DOColor(Color.clear, _halfDuration).SetEase(_easeOut);
            yield return fadeOut.WaitForCompletion();
        }
    }
}