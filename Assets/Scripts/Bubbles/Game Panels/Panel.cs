using System;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels
{
    [RequireComponent(typeof(Image))]
    public class Panel : MonoBehaviour, IMouseInteractor
    {
        [field: SerializeField] public SlotID ID { get; set; }
        [SerializeField] private bool _isLocked;
        
        [Header("Dependencies")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _toHighlight;
        [SerializeField] private PanelPickup _pickup;

        [Header("Graphics Settings")] 
        [SerializeField] private PanelHighlight _panelHighlight;
        [SerializeField] private GraphicsSettings _graphicsSettings;

        private void Start()
        {
            GetComponent<Image>().color = Color.clear;
            _pickup.ParentPanel = this;
        }

        public bool IsActive()
        {
            return !_isLocked;
        }

        public void SetHighlight(HighlightState highlight)
        {
            _panelHighlight.SetHighlightState(highlight);
        }
    }
}