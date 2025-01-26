using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels.Automater
{
    public class EnvironmentMaker : MonoBehaviour
    {
        [SerializeField] private Image _itemField;
        [SerializeField] private PanelPickup _pickupField;

        [Button(ButtonSizes.Large)]
        public void Create(EnvironmentMakerData data)
        {
            _itemField.sprite = data.Item;
            _pickupField.gameObject.SetActive(data.Item != null);
        }
    }
}