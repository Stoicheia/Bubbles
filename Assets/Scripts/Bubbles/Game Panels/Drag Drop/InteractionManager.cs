using System;
using System.Collections.Generic;
using System.Linq;
using Bubbles.GamePanels;
using UnityEngine;

namespace Bubbles.InteractableInput
{
    public class InteractionManager : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private DragInteractionHandler _dragHandler;
        [SerializeField] private SceneManager _sceneManager;

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
        }

        private List<Panel> GetAllPanels()
        {
            Panel[] allPanelsInScene = FindObjectsByType<Panel>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            return allPanelsInScene.ToList();
        }

        private List<Panel> GetInteractablePanels(PanelPickup pickup)
        {
            Panel[] allPanelsInScene = FindObjectsByType<Panel>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            GameScene activeScene = _sceneManager.ActiveScene;
            List<Panel> validPanels = allPanelsInScene.Where(x =>
            {
                SceneInteraction requested = new SceneInteraction(pickup.ParentID, x.ID);
                return activeScene.IsValidInteraction(requested);
            }).ToList();
            return validPanels;
        }

        private void HandlePickup(PanelPickup pickup, Vector2 _)
        {
            List<Panel> interactable = GetInteractablePanels(pickup);
            foreach (Panel p in interactable)
            {
                p.SetHighlight(HighlightState.CanInteract);
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
            _sceneManager.TryInteraction(interaction);
        }
        
        
    }
}