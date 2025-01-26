using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bubbles.GamePanels;
using DG.Tweening;
using Ending;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public class GameSceneTransitionAnims : SerializedMonoBehaviour
    {
        public static event Action<SlotID, Panel> OnPop;
        
        [OdinSerialize][ReadOnly] private Dictionary<SlotID, PanelField> _panelFields;
        [SerializeField] private GameSceneManager _sceneManager;
        [SerializeField] private EndingTransitioner _endingTransitioner;
        [SerializeField] private RectTransform _overlayDuringTransition;

        [Header("Settings")]
        [SerializeField] private float _gapBetweenPopsSecs;
        [SerializeField] private Ease _easeIn;
        [SerializeField] private Ease _easeOut;
        [SerializeField] private float _duration;

        private void Start()
        {
            _panelFields = _sceneManager.PanelFields;
        }

        [Button]
        public void TransitionTo(SceneInteraction fromInteraction, GameScene toScene)
        {
            StartCoroutine(ChainTransition(fromInteraction, toScene));
        }
        private readonly List<SlotID> _naturalOrder = 
            new List<SlotID>() {SlotID.Char1, SlotID.Char2, SlotID.Char3, SlotID.Bubble1, SlotID.Bubble2, SlotID.Bubble3, SlotID.Environment};

        private bool IsCharacter(SlotID id)
        {
            return id == SlotID.Char1 || id == SlotID.Char2 || id == SlotID.Char3;
        }

        private IEnumerator ChainTransition(SceneInteraction fromInteraction, GameScene toScene)
        {
            _sceneManager.SetLock(true);
            
            if (toScene.IsEndingScene)
            {
                int endingNumber = toScene.Ending;
                yield return _endingTransitioner.TransitionToEnding(endingNumber);
                yield break;
            }
            
            _overlayDuringTransition.gameObject.SetActive(true);
            
            HashSet<SlotID> involvedIDs = new HashSet<SlotID>() {fromInteraction.From, fromInteraction.To};
            HashSet<PanelField> involvedPanels = Enumerable.ToHashSet(involvedIDs.Select(x => _panelFields[x]));
            var seqs = new List<Coroutine>();
            foreach (SlotID id in involvedIDs)
            {
                PanelField field = _sceneManager.LoadScenePanel(toScene, id);
                PanelPopTransition anim = field.GetComponent<PanelPopTransition>();
                if (!IsCharacter(id))
                {
                    anim.DisappearImmediately();
                    seqs.Add(StartCoroutine(anim.TransitionInSeq(_easeIn, _easeOut, _duration)));
                }

                OnPop?.Invoke(id, field.ActivePanelInstance);
            }
            yield return new WaitForSeconds(_gapBetweenPopsSecs);

            foreach (SlotID id in _naturalOrder)
            {
                if (involvedIDs.Contains(id)) continue;
                Panel oldPanel = _sceneManager.PanelFields[id].ActivePanelInstance;
                PanelField field = _sceneManager.LoadScenePanel(toScene, id);
                Panel newPanel = _sceneManager.PanelFields[id].ActivePanelInstance;
                bool samePanelKinda = oldPanel.IsSameAs(newPanel);
                if (!samePanelKinda)
                {
                    PanelPopTransition anim = field.GetComponent<PanelPopTransition>();
                    if (!IsCharacter(id))
                    {
                        anim.DisappearImmediately();
                        StartCoroutine(anim.TransitionInSeq(_easeIn, _easeOut, _duration));
                    }

                    OnPop?.Invoke(id, field.ActivePanelInstance);
                    yield return new WaitForSeconds(_gapBetweenPopsSecs);
                }
            }
            _sceneManager.SetActiveScene(toScene);
            if (toScene.IsEndingScene)
            {
                yield return new WaitForSeconds(_endingTransitioner.WaitBefore);
                int endingNumber = toScene.Ending;
                yield return _endingTransitioner.TransitionToEnding(endingNumber);
            }
            else
            {
                _sceneManager.SetLock(false);
                _overlayDuringTransition.gameObject.SetActive(false);
            }
        }
    }
}