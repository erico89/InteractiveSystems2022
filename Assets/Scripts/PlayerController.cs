using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject winTextObject1;
    public GameObject winTextObject2;
    public GameObject winTextObject3;
    public AudioSource pickSound;
    public AudioSource NOTpickSound;

    private Rigidbody rb;
    private int count;
    private int score;
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;

        SetCountText();
        winTextObject.SetActive(false);
        winTextObject1.SetActive(false);
        winTextObject2.SetActive(false);
        winTextObject3.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Score: " + score.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
            if(score == 72)
            {
                winTextObject1.SetActive(true);
                winTextObject2.SetActive(false);
                winTextObject3.SetActive(false);
            }
            else if(score >= 68 && score < 72)
            {
                winTextObject2.SetActive(true);
                winTextObject1.SetActive(false);
                winTextObject3.SetActive(false);
            }
            else
            {
                winTextObject3.SetActive(true);
                winTextObject1.SetActive(false);
                winTextObject2.SetActive(false);
            }

        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            score += 6;
            pickSound.Play();
        }
        if (other.gameObject.CompareTag("NOT PickUp"))
        {
            other.gameObject.SetActive(false);
            score -= 2;
            NOTpickSound.Play();
        }

        SetCountText();
    }
}