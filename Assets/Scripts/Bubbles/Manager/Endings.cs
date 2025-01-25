using UnityEngine;
using UnityEngine.UI;
public class Endings : MonoBehaviour
{
    public int number;
    public bool active;
    public Image endinImg;
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().enabled = active;
    }
}
