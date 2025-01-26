using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels.Automater
{
    public class CharacterMaker : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Image _baseCharacterField;
        [SerializeField] private Image _pickupCharacterField;
        [SerializeField] private Image _foregroundField;
        [SerializeField] private Image _expressionField;
        
        [Button(ButtonSizes.Large)]
        public void Create(CharacterMakerData data)
        {
            if (data.Base != null)
            {
                _baseCharacterField.sprite = data.Base;
                _pickupCharacterField.sprite = data.Base;
                _foregroundField.sprite = data.Base;
            }

            _expressionField.sprite = data.Expression;
        }
    }
}