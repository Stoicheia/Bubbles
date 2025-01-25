using UnityEngine;
using UnityEngine.UI;
public class Endings : MonoBehaviour
{
    public int number;
    public bool active;
    public Image endinImg;
    public Image imageComponent;
    void Start()
    {
        if (SaveManager.Instance.Endings[(number--)])
        {
            imageComponent = endinImg;
        }
        // Renderer renderer = GetComponent<Renderer>();
        gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().enabled = active;
    }
}
