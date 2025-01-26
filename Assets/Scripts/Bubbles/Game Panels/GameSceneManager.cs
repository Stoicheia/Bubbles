using System;
using System.Collections;
using System.Collections.Generic;
using Bubbles.Graphics.Transitions;
using Ending;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubbles.GamePanels
{
    public class GameSceneManager : SerializedMonoBehaviour
    {
        public static event Action OnTransition;
        public event Action<SceneInteraction> OnFailInteraction;
        public event Action<SceneInteraction, GameScene> OnTransitionFromInteraction;
        public GameScene ActiveScene
        {
            get => _activeScenePrefab;
            set => _activeScenePrefab = value;
        }

        public bool IsLocked => _isLocked;
        public Dictionary<SlotID, PanelField> PanelFields => _panelFields;

        [OdinSerialize] private Dictionary<SlotID, PanelField> _panelFields;
        [SerializeField] private RectTransform _sceneRoot;
        [SerializeField] [ReadOnly] private GameScene _activeScenePrefab;
        [SerializeField] [ReadOnly] private bool _isLocked;
        [SerializeField] private GameSceneTransitionAnims _anims;

        [Header("Debug")]
        [SerializeField] private GameScene _loadOnStart;

        private void Awake()
        {
            foreach (var kvp in _panelFields)
            {
                kvp.Value.ID = kvp.Key;
            }
        }

        private void Start()
        {
            DeleteTestVisualisation();
            LoadSceneInstantly(_loadOnStart);
        }

        public void LoadSceneWithAnimation(SceneInteraction fromInteraction, GameScene scene)
        {
            _anims.TransitionTo(fromInteraction, scene);
        }

        public void DeleteTestVisualisation()
        {
            foreach (var kvp in _panelFields)
            {
                Destroy(kvp.Value.GetComponentInChildren<Panel>().gameObject);
            }
        }

        [Button(ButtonSizes.Large)]
        public void LoadSceneInstantly(GameScene scene)
        {
            Dictionary<SlotID, Panel> newScenePanels = scene.PanelPrefabs;
            foreach (var kvp in _panelFields)
            {
                SlotID id = kvp.Key;
                PanelField existingPanelField = kvp.Value;

                if (newScenePanels.ContainsKey(id))
                {
                    existingPanelField.LoadInstantly(newScenePanels[id]);
                }
                else
                {
                    existingPanelField.UnloadInstantly();
                }
            }

            _activeScenePrefab = scene;
            if (scene.IsEndingScene)
            {
                Debug.Log($"Ending {scene.Ending} reached.");
            }
        }

        [Button]
        public PanelField LoadScenePanel(GameScene toScene, SlotID idToLoad)
        {
            PanelField toField = PanelFields[idToLoad];
            Panel panelPrefab = toScene.PanelPrefabs[toField.ID];
            
            toField.LoadInstantly(panelPrefab);
            return toField;
        }

        public void SetLock(bool on)
        {
            _isLocked = on;
        }

        public void SetActiveScene(GameScene scene)
        {
            ActiveScene = scene;
            if (scene.IsEndingScene)
            {
                Debug.Log($"Ending {scene.Ending} reached.");
            }
        }

        public bool TryInteraction(SceneInteraction interaction)
        {
            if (_activeScenePrefab == null)
            {
                Debug.LogError("Attempted an interaction while no active scene was detected.");
                return false;
            }

            if (_isLocked)
            {
                Debug.LogError("Cannot attempt transition while locked.");
                return false;
            }

            bool hasInteraction = _activeScenePrefab.IsValidInteraction(interaction);
            if (!hasInteraction)
            {
                OnFailInteraction?.Invoke(interaction);
                return false;
            }

            GameScene toScene = _activeScenePrefab.GetNextScene(interaction);
            LoadSceneWithAnimation(interaction, toScene);
            
            OnTransitionFromInteraction?.Invoke(interaction, _activeScenePrefab);
            OnTransition?.Invoke();
            return true;
        }

        public bool CanInteract(PanelPickup pickup, Panel panel)
        {
            return _activeScenePrefab.IsValidInteraction(new SceneInteraction(pickup.ParentID, panel.ID));
        }

        private IEnumerator SceneTransitionSequence(GameScene toScene)
        {
            _isLocked = true;
            yield return new WaitForSeconds(0.2f);
            LoadSceneInstantly(toScene);
            _isLocked = false;
        }
    }
}