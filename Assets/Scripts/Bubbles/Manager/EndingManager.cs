using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject[] EndingsBtns = new GameObject[37];
 
    public void switchPage()
    {
        foreach (GameObject obj in EndingsBtns)
        {
            Endings endings = obj.GetComponent<Endings>();
            if (endings != null)
            {
                endings.active = !endings.active;
            }
        }
    }
}
