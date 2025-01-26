using UnityEngine;
using UnityEngine.UI;

public class ShowEnding : MonoBehaviour
{
    public GameObject fullScreenEnding;

    public void ShowEndingSprite_FullScreen()
    {
        //grab current img
        Image imageComponent = GetComponent<Image>();
        Sprite currentSprite = imageComponent.sprite;

        //set "full screen ending Img"
        Image fullScreenEndingImageComponent = fullScreenEnding.GetComponent<Image>();
        fullScreenEndingImageComponent.sprite = currentSprite;

        //call set active both img and the button
        fullScreenEnding.SetActive(true);
        
    }
}
