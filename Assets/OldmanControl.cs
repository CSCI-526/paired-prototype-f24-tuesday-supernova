using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of left/right movement
    public float jumpForce = 10f; // Jump height
    public float acceleration = 5f; // How quickly the player accelerates
    public Transform GroundCheck1; // Reference to the ground check position
    public LayerMask groundLayer; // Layer representing the ground

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is grounded using OverlapCircle
        isGrounded = Physics2D.OverlapCircle(GroundCheck1.position, 0.15f, groundLayer);

        // Get the horizontal input (A/D or Left/Right)
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        Vector2 targetVelocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, acceleration * Time.deltaTime);

        // Jump if the player is grounded and presses W or Up Arrow
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}


