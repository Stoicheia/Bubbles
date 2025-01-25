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
        }

        public override List<ItemAsset> GetReceivableItems()
        {
            return State.TransitionsByAddItem.Keys.ToList();
        }

        public override Image GetImageToHighlight()
        {
            return State.Scene.ImageToHighlight;
        }
        
        [Button(ButtonSizes.Large)]
        public void RenderCurrentStateInstantly()
        {
            if(_sceneObject != null)
                Destroy(_sceneObject.gameObject);
            _sceneObject = Instantiate(State.Scene, _sceneRoot).GetComponent<RectTransform>();
        }
        
        [Button]
        public void SetHighlight(HighlightState state)
        {
            State.Scene.SetHighlight(state);
        }
    }
}