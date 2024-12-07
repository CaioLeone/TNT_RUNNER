using UnityEngine;
using System.Collections;

public class NewTutorial : MonoBehaviour 
{
    public GameObject jumpAnimation;
    public GameObject slideAnimation;
    public GameObject leftAnimation;
    public GameObject rightAnimation;
    public GameObject avatarLeft;
    public GameObject avatarRight;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    void Start()
    {
        StartCoroutine(PlayTutorial());
    }

    void Update()
    {
        if(isMovingLeft)
        {
            avatarLeft.transform.position = new Vector3(
                avatarLeft.transform.position.x - 0.5f * Time.deltaTime,
                avatarLeft.transform.position.y,
                avatarLeft.transform.position.z
            );
        }
        if(isMovingRight)
        {
            avatarLeft.transform.position = new Vector3(
                avatarLeft.transform.position.x - 0.5f * Time.deltaTime,
                avatarLeft.transform.position.y,
                avatarLeft.transform.position.z
            );
        }
    }

    IEnumerator PlayTutorial()
    {
        jumpAnimation.SetActive(false);
        slideAnimation.SetActive(false);
        leftAnimation.SetActive(false);
        rightAnimation.SetActive(false);

        jumpAnimation.SetActive(true);
        yield return new WaitForSeconds(3f); 
        jumpAnimation.SetActive(false);

        
        slideAnimation.SetActive(true);
        yield return new WaitForSeconds(3f);
        slideAnimation.SetActive(false);

        
        leftAnimation.SetActive(true);
        yield return StartCoroutine(moveLeft());
        leftAnimation.SetActive(false);

        
        rightAnimation.SetActive(true);
        yield return StartCoroutine(moveRight());
        rightAnimation.SetActive(false);
    }

    IEnumerator moveLeft()
    {
        float duration = 1.5f; 
        float elapsedTime = 0f;

        Vector3 startPosition = avatarLeft.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x - 2f, startPosition.y, startPosition.z);

        while (elapsedTime < duration / 2)
        {
            float t = elapsedTime / (duration / 2);
            avatarLeft.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < duration / 2)
        {
           float t = elapsedTime / (duration / 2);
            avatarLeft.transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        avatarLeft.transform.position = startPosition;
    } 

     IEnumerator moveRight()
    {
        float duration = 1.5f; 
        float elapsedTime = 0f;

        Vector3 startPosition = avatarRight.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x + 2f, startPosition.y, startPosition.z);

        while (elapsedTime < duration / 2)
        {
            float t = elapsedTime / (duration / 2);
            avatarRight.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < duration / 2)
        {
           float t = elapsedTime / (duration / 2);
            avatarRight.transform.position = Vector3.Lerp(targetPosition, startPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        avatarLeft.transform.position = startPosition;

        Debug.Log("Moving right");
    }   
}