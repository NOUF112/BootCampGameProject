using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public float jumbForce = 3.0f;

    private Camera mainCam;

    Rigidbody rb;

    GameManager gameManager;

    public int collectedJewels;

    public int maxHealth=1;
    public int currentHealth;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam=Camera.main;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        jump();
    }

    public void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = mainCam.transform.forward;
        Vector3 cameraRight = mainCam.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 playerDirection = cameraForward.normalized * VerticalInput + cameraRight.normalized * horizontalInput;

        if (playerDirection != Vector3.zero)
        {
            //change player direction
            transform.forward = playerDirection;

            //move player
            transform.position += playerDirection * playerSpeed * Time.deltaTime;
        }

    }

    public void jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up *  jumbForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Damage"))
        {
            Vector3 damageDirection = other.transform.position - transform.position;
            damageDirection.Normalize();
            rb.AddForce(-damageDirection * 2f, ForceMode.Impulse);
            currentHealth -= 1;
            gameManager.updateHealthSlider(currentHealth , maxHealth);
            if (currentHealth <= 0) 
            { 
            gameManager.restart();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jewel"))
        {
            collectedJewels += 1;
            Destroy(other.gameObject);
            gameManager.updateJewelText(collectedJewels);
        }


        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            gameManager.showWinText();

        }

    }
}
