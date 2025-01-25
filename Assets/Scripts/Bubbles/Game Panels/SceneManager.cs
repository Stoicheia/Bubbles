using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.GamePanels
{
    public class SceneManager : SerializedMonoBehaviour
    {
        public event Action<SceneInteraction> OnFailInteraction;
        public event Action<SceneInteraction, GameScene> OnTransitionFromInteraction;
        
        [SerializeField] private RectTransform _sceneRoot;
        [SerializeField] [ReadOnly] private GameScene _activeScene;
        [SerializeField] [ReadOnly] private bool _isLocked;

        public void LoadScene(GameScene scene)
        {
            StartCoroutine(SceneTransitionSequence(scene));
        }

        [Button(ButtonSizes.Large)]
        public void LoadSceneInstantly(GameScene scene)
        {
            if (_activeScene != null)
            {
                Destroy(_activeScene.gameObject);
            }
            
            GameScene sceneInstance = Instantiate(scene, _sceneRoot);
            _activeScene = sceneInstance;
        }

        public bool TryInteraction(SceneInteraction interaction)
        {
            if (_activeScene == null)
            {
                Debug.LogError("Attempted an interaction while no active scene was detected.");
                return false;
            }

            bool hasInteraction = _activeScene.IsValidInteraction(interaction);
            if (!hasInteraction)
            {
                OnFailInteraction?.Invoke(interaction);
                return false;
            }

            GameScene toScene = _activeScene.GetNextScene(interaction);
            LoadScene(toScene);
            
            OnTransitionFromInteraction?.Invoke(interaction, _activeScene);
            return true;
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