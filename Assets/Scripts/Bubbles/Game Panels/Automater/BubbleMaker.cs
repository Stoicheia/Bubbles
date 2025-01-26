using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles.GamePanels.Automater
{
    public class BubbleMaker : MonoBehaviour
    {
        [Header("Dependencies")] 
        [SerializeField] private List<BubblePreset> Presets;

        [Button]
        public void Create(BubbleMakerData data)
        {
            foreach (BubblePreset preset in Presets)
            {
                if(preset.Root != null)
                    preset.Root.gameObject.SetActive(false);
            }

            int number;
            if (data.PickupSprite == null)
            {
                number = 0;
            }
            else
            {
                number = data.ExtraSprites.Count + 1;
            }

            BubblePreset activePreset = Presets[number];
            if(activePreset.Root != null)
                activePreset.Root.gameObject.SetActive(true);
            for (int i = 0; i < number; i++)
            {
                List<Image> fields = activePreset.PickupImageFields;
                Sprite sprite;
                if (i == 0)
                {
                    sprite = data.PickupSprite;
                }
                else
                {
                    sprite = data.ExtraSprites[i - 1];
                }
                fields[i].sprite = sprite;
            }
        }
    }

    [Serializable]
    public struct BubblePreset
    {
        public RectTransform Root;
        public List<Image> PickupImageFields;
    }
}