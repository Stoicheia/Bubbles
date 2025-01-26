using UnityEngine;

public class HideFullScreenEnding : MonoBehaviour
{
    public GameObject fullScreenEnding;

    public void GoHideFullScreenEnding()
    {
        fullScreenEnding.SetActive(false);
    }
}
