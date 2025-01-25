using Bubbles.Graphics;
using Bubbles.InteractableInput;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels
{
    public class Panel : SerializedMonoBehaviour, IMouseInteractor
    {
        [field: SerializeField] public SlotID ID { get; set; }
        [SerializeField] private bool _isActive;
        
        [Header("Dependencies")]
        [SerializeField] private Image _background;
        [SerializeField] private Image _toHighlight;
        [SerializeField] private Pickup _pickup;
        
        [Header("Graphics Settings")]
        [SerializeField] private GraphicsSettings _graphicsSettings;

        public bool IsActive()
        {
            return _isActive;
        }
    }
}