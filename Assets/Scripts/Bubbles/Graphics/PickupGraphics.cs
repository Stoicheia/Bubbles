using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.Graphics
{
    public class PickupGraphics : SerializedMonoBehaviour
    {
        public Sprite Sprite => _imageField.sprite;
        
        [SerializeField] private Image _imageField;
        private Material _imageMaterial;

        private void Start()
        {
            Material mat = Instantiate(_imageField.material);
            _imageField.material = mat;
            _imageMaterial = mat;
        }

        [Button]
        public void SetGraphics(PickupState state)
        {
            switch (state)
            {
                case PickupState.Dragging:
                    SetAbsent();
                    break;
                case PickupState.AtRest:
                    SetAtRest();
                    break;
                case PickupState.Hover:
                    SetHover();
                    break;
                case PickupState.Click:
                    SetClicked();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void SetAtRest()
        {
            _imageMaterial.SetInt("GLOW_ON", 1);
            _imageField.enabled = true;
            _imageField.color = Color.white;
        }

        private void SetAbsent()
        {
            _imageMaterial.SetInt("GLOW_ON", 0);
            _imageField.enabled = false;
        }

        private void SetHover()
        {
            _imageMaterial.SetInt("GLOW_ON", 1);
            _imageField.enabled = true;
            _imageField.color = Color.yellow;
        }

        private void SetClicked()
        {
            _imageMaterial.SetInt("GLOW_ON", 1);
            _imageField.enabled = true;
            _imageField.color = Color.green;
        }
    }
}