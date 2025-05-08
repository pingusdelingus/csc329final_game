using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;
  [Header("Gravity")]
  public float baseGravity = 2;
  public float maxFallSpeed = 18f;
  public int jumpsRemaining;
  public float fallSpeedMultiplier = 5f;

    [Header("Ground Check")]
    public Transform groundCheckPos;

    public Vector2 groundCheckSize = new Vector2(1.0f, 1.0f);
    private BoxCollider2D boxCollider;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    private void Gravity(){
            if (rb.linearVelocity.y < 0){
                rb.gravityScale = baseGravity * fallSpeedMultiplier; // fall faster
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));

            }
            else{
                rb.gravityScale = baseGravity;

            }
    }
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
        boxCollider = GetComponent<BoxCollider2D>();
    }// end of start method

    // Update is called once per frame
    void Update()
    {
        groundCheck();
        Gravity();
    }// end of update

    void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }// end of move


    public void Jump(InputAction.CallbackContext context)
    {
//       Debug.Log(isGrounded()); 
           if ( jumpsRemaining > 0){
        if (context.performed){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }else if (context.canceled){
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce / 2);
            jumpsRemaining--;
        }
    }
    }// end of jump


    private void groundCheck(){
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer)){
            jumpsRemaining = maxJumps;
        }
    }// end of is grounded method

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }// end of ondraw


}// end of playermovement