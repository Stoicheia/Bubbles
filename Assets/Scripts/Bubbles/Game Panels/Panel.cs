using System;
using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels
{
    public class Panel : MonoBehaviour, IMouseInteractor
    {
        [field: SerializeField] public SlotID ID { get; set; }
        [SerializeField] private bool _isActive;
        
        [Header("Dependencies")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _toHighlight;
        [SerializeField] private PanelPickup _pickup;

        [Header("Graphics Settings")] 
        [SerializeField] private PanelHighlight _panelHighlight;
        [SerializeField] private GraphicsSettings _graphicsSettings;

        private void Start()
        {
            _pickup.ParentPanel = this;
        }

        public bool IsActive()
        {
            return _isActive;
        }

        public void SetHighlight(HighlightState highlight)
        {
            _panelHighlight.SetHighlightState(highlight);
        }
    }
}