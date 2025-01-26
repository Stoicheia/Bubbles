using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneMover : MonoBehaviour
{
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadEndingsScene()
    {
        SceneManager.LoadScene("Endings");
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void End()
    {
        Application.Quit();
    }
}
