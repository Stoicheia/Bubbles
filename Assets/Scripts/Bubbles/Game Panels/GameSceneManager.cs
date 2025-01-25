using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubbles.GamePanels
{
    public class GameSceneManager : SerializedMonoBehaviour
    {
        public event Action<SceneInteraction> OnFailInteraction;
        public event Action<SceneInteraction, GameScene> OnTransitionFromInteraction;
        public GameScene ActiveScene
        {
            get => _activeScenePrefab;
            set => _activeScenePrefab = value;
        }
        public Dictionary<SlotID, PanelField> PanelFields => _panelFields;

        [OdinSerialize] private Dictionary<SlotID, PanelField> _panelFields;
        [SerializeField] private RectTransform _sceneRoot;
        [SerializeField] [ReadOnly] private GameScene _activeScenePrefab;
        [SerializeField] [ReadOnly] private bool _isLocked;

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
            LoadScene(_loadOnStart);
        }

        public void LoadScene(GameScene scene)
        {
            StartCoroutine(SceneTransitionSequence(scene));
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
                Debug.Log($"Ending {scene.Ending.number} reached.");
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
                Debug.Log($"Ending {scene.Ending.number} reached.");
            }
        }

        public bool TryInteraction(SceneInteraction interaction)
        {
            if (_activeScenePrefab == null)
            {
                Debug.LogError("Attempted an interaction while no active scene was detected.");
                return false;
            }

            bool hasInteraction = _activeScenePrefab.IsValidInteraction(interaction);
            if (!hasInteraction)
            {
                OnFailInteraction?.Invoke(interaction);
                return false;
            }

            GameScene toScene = _activeScenePrefab.GetNextScene(interaction);
            LoadScene(toScene);
            
            OnTransitionFromInteraction?.Invoke(interaction, _activeScenePrefab);
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