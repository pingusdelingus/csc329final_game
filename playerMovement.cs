using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class playerMovement : MonoBehaviour
{
    float horizontalInput;
[SerializeField] float moveSpeed = 5.5f;
   [SerializeField] float jumpPower = 7f;
    bool isJumping = false;
     private Animator _animator;
    
    bool isFacingRight = false;

    private int coinCounter = 0;
    
    public TMP_Text counterText;

    public AudioClip jumpSound;

    private AudioSource audioSrc;


    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator =GetComponent<Animator>(); 
        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {  

        if (Input.GetMouseButtonDown(0)){
            FlipSprite();
        }else if (Input.GetMouseButtonDown(1)){
            FlipSprite();
        }
        
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ) && isJumping == false){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            isJumping = true;
            if (jumpSound != null && audioSrc != null)
            {
                audioSrc.PlayOneShot(jumpSound);
            }


        }
        if (horizontalInput != 0){
            _animator.SetBool("isRunning", true);

        }else{

            _animator.SetBool("isRunning", false);
        }
    

    }// end of update

    private void FixedUpdate()
    {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

    }// end of fixed update

    void FlipSprite()
    {
        if (!isFacingRight && horizontalInput < 0f || isFacingRight && horizontalInput > 0f){
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls; 
        }

    }// end of flip sprite method

    private void OnTriggerEnter2D(Collider2D other)
    {


    if (other.CompareTag("Coin") && other.gameObject.activeSelf == true)
    {
        other.gameObject.SetActive(false);
        coinCounter++;
        counterText.text = "Coins : " + coinCounter;
    }

    }// end of ontriggerenter2d
private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset isJumping when player touches the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
             isJumping  = false;
            //_animator.SetBool("isJumping", false);
        }
    }
}// end of playermovmemtn 
