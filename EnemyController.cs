using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
     [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Gravity")]
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 5f;

    [Header("AI Targeting")]
    public Transform finishPoint;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(1.0f, 1.0f);
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    public Transform player;
    public float detectionRange = 5f;
    private Animator animator;
    public GameObject slashHitbox;
    private bool isSlashing = false;

    public AudioClip slashSound;             
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(RandomSlashLoop());
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
         GameObject finish = GameObject.Find("FinishPoint");
    if (finish != null)
        finishPoint = finish.transform;
    else
        Debug.LogWarning("FinishPoint not found in scene!");
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

void Update()
{
    if (finishPoint == null) return;
ApplyGravity();
        CheckGrounded();
        MoveTowardFinish();
    /*
    float distanceToPlayer = Vector2.Distance(transform.position, player.position);

    if (distanceToPlayer <= detectionRange && !isSlashing)
    {
        // move toward player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

        // flip sprite to face player
        if (direction.x > 0)
            transform.localScale = new Vector3(-2.229038f, 2.275482f, 1);
        else
            transform.localScale = new Vector3(2.229038f, 2.275482f, 1);
    }
    else
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop moving
    } */
}// end of update

private void ApplyGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void CheckGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }
private void MoveTowardFinish()
{
    // 1. Calculate direction to finish line
    Vector2 direction = (finishPoint.position - transform.position).normalized;

    // 2. Move horizontally
    rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

    // 3. Flip sprite to face movement direction
    if (direction.x > 0)
        transform.localScale = new Vector3(-2.229038f, 2.275482f, 1);
    else
        transform.localScale = new Vector3(2.229038f, 2.275482f, 1);

    // 4. Detect if there's ground ahead using a raycast
    Vector2 rayOrigin = new Vector2(transform.position.x + Mathf.Sign(direction.x) * 1.0f, transform.position.y - 0.8f);
    float rayLength = 2f;

    RaycastHit2D groundCheck = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
    Debug.DrawRay(rayOrigin, Vector2.down * rayLength, Color.red); // for visual debugging

    bool isGroundAhead = groundCheck.collider != null;

    // 5. Jump if there's no ground ahead
    if (!isGroundAhead && jumpsRemaining > 0)
{
    Debug.Log("JUMP triggered — Ground ahead: " + isGroundAhead + ", Jumps left: " + jumpsRemaining);
    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    jumpsRemaining--;
}
else
{
    Debug.Log("NO JUMP — Ground ahead: " + isGroundAhead + ", Jumps left: " + jumpsRemaining);
}
   
}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
    IEnumerator RandomSlashLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 5f);
            yield return new WaitForSeconds(waitTime);

            if (!isSlashing)
            {
                StartCoroutine(PerformSlash());
            }
        }
    }

    IEnumerator PerformSlash()
    {
        isSlashing = true;

        // 🔊 Play sound
        if (slashSound != null && audioSource != null)
        {
            Debug.Log("Played sound of enemy slash");
            audioSource.PlayOneShot(slashSound);
        }

        animator.SetTrigger("Slash");

        slashHitbox.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        slashHitbox.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isSlashing = false;
    }
}