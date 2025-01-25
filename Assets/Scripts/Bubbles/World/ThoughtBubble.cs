using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public class ThoughtBubble : Interactable
    {
        [SerializeField] private RectTransform _sceneRoot;
        private RectTransform _sceneObject;
        [field: SerializeField] public ThoughtBubbleStateAsset State { get; private set; }

        private void Start()
        {
            _sceneObject = GetComponentInChildren<ThoughtBubbleStateScene>().GetComponent<RectTransform>();
            if (State != null)
            {
                ChangeState(State);
            }
        }

        private void ChangeState(ThoughtBubbleStateAsset newState)
        {
            State = newState;
            ThoughtBubbleStateScene sceneInstance = RenderCurrentStateInstantly();
            sceneInstance.GetPickupsInScene().ForEach(x => x.ParentInteractable = this);
        }

        public override bool Interact(ItemAsset item)
        {
            if (!CanInteract(item))
            {
                return false;
            }
            ThoughtBubbleStateAsset toState = State.TransitionsByAddItem[item];
            ChangeState(toState);
            
            return true;
        }

        public override bool RemoveChildPickup(Pickup pickup)
        {
            if (pickup.ParentInteractable != this) return false;
            ItemAsset item = pickup.Item;
            Dictionary<ItemAsset, ThoughtBubbleStateAsset> removes = State.TransitionsByRemoveItem;
            if (!removes.ContainsKey(item))
            {
                Debug.LogError($"Interactable ({name}) does not contain transition out of its own pickup removal ({item.name}).");
                return false;
            }
            ThoughtBubbleStateAsset newState = State.TransitionsByRemoveItem[item];
            ChangeState(newState);
            return true;
        }

        public override bool CanInteract(ItemAsset item)
        {
            return GetReceivableItems().Contains(item);
        }

        public List<ItemAsset> GetReceivableItems()
        {
            return State.TransitionsByAddItem.Keys.ToList();
        }

        public override Image GetImageToHighlight()
        {
            return State.Scene.ImageToHighlight;
        }

        [Button(ButtonSizes.Large)]
        public ThoughtBubbleStateScene RenderCurrentStateInstantly()
        {
            if(_sceneObject != null)
                Destroy(_sceneObject.gameObject);
            ThoughtBubbleStateScene sceneInstance = Instantiate(State.Scene, _sceneRoot);
            _sceneObject = sceneInstance.GetComponent<RectTransform>();
            return sceneInstance;
        }
        
        [Button]
        public void SetHighlight(HighlightState state)
        {
            State.Scene.SetHighlight(state);
        }
    }
}