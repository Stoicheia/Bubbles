using System;
using System.Collections.Generic;
using System.Linq;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bubbles.Manager
{
    public class InteractablesManager : SerializedMonoBehaviour
    {
        [field: SerializeField][field: ReadOnly] public List<Interactable> AllInteractables { get; private set; }
        [SerializeField] private PickupDragHandler _dragHandler;

        private void Awake()
        {
            AllInteractables = FindObjectsByType<Interactable>(FindObjectsSortMode.InstanceID).ToList();
        }

        private void OnEnable()
        {
            _dragHandler.OnEndPickup += HandleReleasePickup;
        }
        
        public List<Interactable> GetActiveInteractables()
        {
            List<Interactable> active = AllInteractables.Where(x => x.IsActive).ToList();
            return active;
        }
        
        private void HandleReleasePickup(Pickup pickup)
        {
            Interactable underMouse = _dragHandler.InteractableUnderMouse;
            if (underMouse == null) return;
        }
    }
}