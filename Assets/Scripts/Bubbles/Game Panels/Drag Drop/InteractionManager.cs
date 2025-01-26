using System;
using System.Collections.Generic;
using System.Linq;
using Bubbles.GamePanels;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubbles.InteractableInput
{
    public class InteractionManager : MonoBehaviour
    {
        public static event Action<SceneInteraction, bool> OnInteract;
        
        [Header("Dependencies")]
        [SerializeField] private DragInteractionHandler _dragHandler;
        [FormerlySerializedAs("_sceneManager")] [SerializeField] private GameSceneManager _gameSceneManager;
        private Panel _panelUnderMouse;
        [Header("Debug")] 
        [SerializeField] private bool _showValidInteractions;
        [SerializeField] private bool _showInvalidInteractions;

        private void OnEnable()
        {
            _dragHandler.OnStartPickup += HandlePickup;
            _dragHandler.OnEndPickup += HandleRelease;
        }

        private void OnDisable()
        {
            _dragHandler.OnStartPickup -= HandlePickup;
            _dragHandler.OnEndPickup -= HandleRelease;
        }

        private void Update()
        {
            HandleDragOnInteractableHover();
        }
        
        private void HandleDragOnInteractableHover()
        {
            PanelPickup currentlyDragging = _dragHandler.CurrentlyDragging;
            if (currentlyDragging == null) return;
            Panel detectedUnderMouse = _dragHandler.PanelUnderMouse;
            bool underMouseChanged = detectedUnderMouse != _panelUnderMouse;
            if (underMouseChanged)
            {
                if (detectedUnderMouse != null)
                {
                    bool canInteract = _gameSceneManager.CanInteract(currentlyDragging, detectedUnderMouse);
                    HighlightState toState;
                    if (canInteract)
                    {
                        toState = HighlightState.HoverYes;
                    }
                    else if (currentlyDragging.ParentID == detectedUnderMouse.ID)
                    {
                        toState = HighlightState.Disabled;
                    }
                    else
                    {
                        toState = _showInvalidInteractions ? HighlightState.HoverNo : HighlightState.Disabled;
                    }
                    detectedUnderMouse.SetHighlight(toState);
                }

                if (_panelUnderMouse != null)
                {
                    if (!_showValidInteractions)
                    {
                        _panelUnderMouse.SetHighlight(HighlightState.Disabled);
                    }
                    else
                    {
                        bool isValid = GetInteractablePanels(currentlyDragging).Contains(_panelUnderMouse);
                        _panelUnderMouse.SetHighlight(isValid ? HighlightState.CanInteract : HighlightState.Disabled);
                    }
                }
            }

            _panelUnderMouse = detectedUnderMouse;
        }

        private List<Panel> GetAllPanels()
        {
            Panel[] allPanelsInScene = FindObjectsByType<Panel>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return allPanelsInScene.ToList();
        }

        private List<Panel> GetInteractablePanels(PanelPickup pickup)
        {
            Panel[] allPanelsInScene = FindObjectsByType<Panel>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            GameScene activeScene = _gameSceneManager.ActiveScene;
            List<Panel> validPanels = allPanelsInScene.Where(x =>
            {
                SceneInteraction requested = new SceneInteraction(pickup.ParentID, x.ID);
                return activeScene.IsValidInteraction(requested);
            }).ToList();
            return validPanels;
        }

        private void HandlePickup(PanelPickup pickup, Vector2 _)
        {
            if (_showValidInteractions)
            {
                List<Panel> interactable = GetInteractablePanels(pickup);
                foreach (Panel p in interactable)
                {
                    p.SetHighlight(HighlightState.CanInteract);
                }
            }
        }
        
        private void HandleRelease(PanelPickup pickup)
        {
            List<Panel> allPanels = GetAllPanels();
            foreach (Panel p in allPanels)
            {
                p.SetHighlight(HighlightState.Disabled);
            }
            
            Panel panelUnderMouse = _dragHandler.PanelUnderMouse;
            if (panelUnderMouse == null) return;
            SceneInteraction interaction = new SceneInteraction(pickup.ParentID, panelUnderMouse.ID);
            bool success = _gameSceneManager.TryInteraction(interaction);
            OnInteract?.Invoke(interaction, success);
        }
        
        
    }
}