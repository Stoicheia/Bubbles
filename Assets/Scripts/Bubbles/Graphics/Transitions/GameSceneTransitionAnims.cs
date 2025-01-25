using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bubbles.GamePanels;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Bubbles.Graphics.Transitions
{
    public class GameSceneTransitionAnims : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<SlotID, PanelField> _panelFields;
        [SerializeField] private GameSceneManager _sceneManager;

        private void Start()
        {
            _panelFields = _sceneManager.PanelFields;
        }

        [Button]
        public void TransitionTo(SceneInteraction fromInteraction, GameScene toScene)
        {
            
        }
        
        private readonly List<SlotID> _naturalOrder = 
            new List<SlotID>() {SlotID.Bubble1, SlotID.Bubble2, SlotID.Bubble3, SlotID.Char1, SlotID.Char2, SlotID.Char3, SlotID.Environment};

        private IEnumerator ChainTransition(SceneInteraction fromInteraction, GameScene toScene)
        {
            _sceneManager.SetLock(true);
            HashSet<SlotID> involvedIDs = new HashSet<SlotID>() {fromInteraction.From, fromInteraction.To};
            HashSet<PanelField> involvedPanels = involvedIDs.Select(x => _panelFields[x]).ToHashSet();
            var seqs = new List<Coroutine>();
            foreach (SlotID id in involvedIDs)
            {
                PanelField field = _sceneManager.LoadScenePanel(toScene, id);
                PanelPopTransition anim = field.GetComponent<PanelPopTransition>();
                anim.DisappearImmediately();
                seqs.Add(StartCoroutine(anim.TransitionInSeq()));
            }
            foreach (Coroutine anim in seqs)
            {
                yield return anim;
            }

            foreach (SlotID id in _naturalOrder)
            {
                if (involvedIDs.Contains(id)) continue;
                PanelField field = _sceneManager.LoadScenePanel(toScene, id);
                PanelPopTransition anim = field.GetComponent<PanelPopTransition>();
                anim.DisappearImmediately();
                yield return anim.TransitionOutSeq();
            }
            _sceneManager.SetActiveScene(toScene);
            _sceneManager.SetLock(false);
        }
    }
}