using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Tuto_01 : MonoBehaviour
{
    public Transform targetTransform;
    public Sprite unclickedMouse;
    public Sprite clickedMouse;
    private float movingSpeed = 6f;
    private Image imageComponent;
    private int spriteStage = 0; //0: unclickedMouse, 1:clickedMouse, 2:movingMouse, 3:endWaitingMouse
    private Vector2 startPosition;
    private bool isInCour = false;
    

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteStage == 0)
        {
            if (!isInCour)
            {
                StartCoroutine(UnclickedMouseWaiting());
                Debug.Log("stage 0");
            }
        }

        if (spriteStage == 1)
        {
            if (!isInCour)
            {
                imageComponent.sprite = clickedMouse;
                StartCoroutine(ClickedMouseWaiting());
                Debug.Log("stage 1");
            }
        }

        if (spriteStage == 2)
        {
            if (!isInCour)
            {
                Debug.Log("stage 2");
                transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, movingSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetTransform.position) < 0.1f)
                {
                    Debug.Log("Reached Target!");
                    spriteStage++;
                }
            }
        }

        if (spriteStage == 3)
        {
            if (!isInCour)
            {
                Debug.Log("stage 3");
                StartCoroutine(EndMouseWaiting());
            }
        }
    }

    IEnumerator UnclickedMouseWaiting()
    {
        isInCour = true;
        yield return new WaitForSeconds(0.5f);
        spriteStage++;
        isInCour = false;
    }

    IEnumerator ClickedMouseWaiting()
    {
        isInCour = true;
        yield return new WaitForSeconds(0.5f);
        spriteStage++;
        isInCour = false;
    }

    IEnumerator EndMouseWaiting()
    {
        isInCour = true;
        yield return new WaitForSeconds(0.5f);
        transform.position = startPosition;
        imageComponent.sprite = unclickedMouse;
        spriteStage = 0;
        isInCour = false;
    }
}
