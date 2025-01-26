using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ending
{
    public class EndingBook : MonoBehaviour
    {
        [SerializeField] private Sprite _lockedSprite;
        [SerializeField] private EndingRepo _endingRepo;
        [SerializeField] private GridLayoutGroup _gridOfImages;

        private void OnEnable()
        {
            Render(_endingRepo);
        }

        public void Render(EndingRepo endingRepo)
        {
            Image[] images = _gridOfImages.gameObject.GetComponentsInChildren<Image>();
            for (int i = 0; i < Math.Min(images.Length, endingRepo.Endings.Count); i++)
            {
                Image rep = images[i];
                if (endingRepo.Endings[i].IsComplete)
                {
                    rep.sprite = endingRepo.Endings[i].Graphic;
                }
                else
                {
                    rep.sprite = _lockedSprite;
                }
            }

            for (int i = endingRepo.Endings.Count; i < images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
            }
        }
    }
}