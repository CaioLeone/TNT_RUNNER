using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidBody;
    private Animator animator;
    private BoxCollider boxCollider;

    private int currentLane = 1;
    private Vector3 verticalTargetPosition;
    
    private bool jumping = false;
    private float jumpStart;
    [Header("Pulo")]
    public float jumpLength;
    public float jumpHeight; 
    
    private bool sliding = false;
    private float slideStart;
    [Header("Escorregar")]
    public float slideLength;
    private Vector3 boxColliderSize;
    
    [Header("Personagem")]
    public float playerSpeed;
    public float minSpeed;
    public float maxSpeed = 30f;
    public float laneSpeed;
    public int maxLife = 3;
    public float invincibleTime = 2f;
    public GameObject model;
    private int currentLife;
    private bool invincible = false;
    static int blinkingValue;

    public float ratioSpeed = 0.01f;

    private UIManager uiManager;
    private int coins;

    public GameObject gameOver;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        currentLife = maxLife;
        playerSpeed = minSpeed;
        blinkingValue = Shader.PropertyToID("_BlinkingValue");
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    void PlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && currentLife > 0)
        {
            ChangeLane(-2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && currentLife > 0)
        {
            ChangeLane(2);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) && currentLife > 0)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) && currentLife > 0)
        {
            Slide();
        }

        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if (ratio >= 1f)
            {
                jumping = false;
                animator.SetBool("jumping", false);
            }
            else
            {
                float adjustRatio = Mathf.Pow(ratio, ratioSpeed);
                verticalTargetPosition.y = Mathf.Sin(adjustRatio * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if (ratio >= 1f)
            {
                sliding = false;
                animator.SetBool("sliding", false);
                boxCollider.size = boxColliderSize;
            }
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = Vector3.forward * playerSpeed;
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < -1 || targetLane > 3)
        {
            return;
        }
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;
            animator.SetFloat("jumpSpeed", playerSpeed / jumpLength);
            animator.SetBool("jumping", true);
            jumping = true;

        }
    }

    void Slide()
    {
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            animator.SetFloat("jumpSpeed", playerSpeed / slideLength);
            animator.SetBool("sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;

            boxCollider.size = newSize;
            sliding = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("Pegou a Latinha");
            coins++;
            uiManager.UpdateCoins(coins);
            other.transform.parent.gameObject.SetActive(false);
        }

        if (invincible)
        {
            return;
        }
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Bateu no obstaculo");
            currentLife--;
            uiManager.UpdateLife(currentLife);

            animator.SetTrigger("hit");
            playerSpeed = 0;
            if (currentLife <= 0)
            {
                Debug.Log("Morreu");
                animator.SetBool("dead", true);

                playerSpeed = 0;
                gameOver.SetActive(true);

            }
            else
            {
                StartCoroutine(Blinking(invincibleTime));
            }
        }
    }

    IEnumerator Blinking(float time)
    {
        invincible = true;
        float timer = 0;
        float currentBlink = 0;
        float lastBlink = 0;
        float blinkPeriod = 0.1f;

        bool enabled = false;

        yield return new WaitForSeconds(1);
        playerSpeed = minSpeed;
        while (timer < time && invincible)
        {
            //Shader.SetGlobalFloat(blinkingValue, currentBlink);
            model.SetActive(enabled);
            yield return null;
            timer += Time.deltaTime;
            lastBlink += Time.deltaTime;
            if (blinkPeriod < lastBlink)
            {
                lastBlink = 0;
                currentBlink = 1f - currentBlink;
                enabled = !enabled;
            }
        }
        model.SetActive(true);
        //Shader.SetGlobalFloat(blinkingValue, 0);
        invincible = false;
    }
}