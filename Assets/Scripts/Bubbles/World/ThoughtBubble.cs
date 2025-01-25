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
        [SerializeField] private RectTransform _scenePrefab;
        [field: SerializeField] public ThoughtBubbleStateAsset State { get; private set; }

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
            Destroy(_scenePrefab.gameObject);
            _scenePrefab = Instantiate(State.Scene, _sceneRoot).GetComponent<RectTransform>();
        }
        
        [Button]
        public void SetHighlight(HighlightState state)
        {
            State.Scene.SetHighlight(state);
        }
    }
}