using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bubbles.GamePanels;
using DG.Tweening;
using Ending;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public class GameSceneTransitionAnims : SerializedMonoBehaviour
    {
        public static event Action Pop;
        
        [OdinSerialize][ReadOnly] private Dictionary<SlotID, PanelField> _panelFields;
        [SerializeField] private GameSceneManager _sceneManager;
        [SerializeField] private EndingTransitioner _endingTransitioner;

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
            new List<SlotID>() {SlotID.Bubble1, SlotID.Bubble2, SlotID.Bubble3, SlotID.Char1, SlotID.Char2, SlotID.Char3, SlotID.Environment};

        private IEnumerator ChainTransition(SceneInteraction fromInteraction, GameScene toScene)
        {
            _sceneManager.SetLock(true);
            
            if (toScene.IsEndingScene)
            {
                int endingNumber = toScene.Ending;
                yield return _endingTransitioner.TransitionToEnding(endingNumber);
            }
            
            HashSet<SlotID> involvedIDs = new HashSet<SlotID>() {fromInteraction.From, fromInteraction.To};
            HashSet<PanelField> involvedPanels = involvedIDs.Select(x => _panelFields[x]).ToHashSet();
            var seqs = new List<Coroutine>();
            foreach (SlotID id in involvedIDs)
            {
                PanelField field = _sceneManager.LoadScenePanel(toScene, id);
                PanelPopTransition anim = field.GetComponent<PanelPopTransition>();
                anim.DisappearImmediately();
                seqs.Add(StartCoroutine(anim.TransitionInSeq(_easeIn, _easeOut, _duration)));
                Pop?.Invoke();
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
                    anim.DisappearImmediately();
                    StartCoroutine(anim.TransitionInSeq(_easeIn, _easeOut, _duration));
                    Pop?.Invoke();
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
            }
        }
    }
}