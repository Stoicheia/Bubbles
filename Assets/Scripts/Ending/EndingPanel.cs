using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ending
{
    public class EndingPanel : MonoBehaviour
    {
        [SerializeField] private Image _imageField;
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _restartButton.onClick.AddListener(RestartGame);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("Start");
        }

        [Button]
        public void Render(EndingAsset asset)
        {
            _imageField.sprite = asset.Graphic;
            _textField.text = asset.Text;
        }
    }
}