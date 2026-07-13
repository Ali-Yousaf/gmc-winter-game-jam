using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class CatController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 6f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;
    private bool isRunning;
    private bool isAttacking;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        // Don't accept movement input while attacking
        if (!isAttacking)
        {
            GetInput();
            HandleJump();
            HandleAttack();
        }

        UpdateAnimations();
        Flip();
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        float speed = isRunning ? runSpeed : walkSpeed;

        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isRunning = Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(moveInput) > 0;
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttacking = true;
            moveInput = 0;
            animator.SetTrigger("Attack");
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer);
    }

    void UpdateAnimations()
    {
        float speedParameter = 0;

        if (Mathf.Abs(moveInput) > 0)
        {
            speedParameter = isRunning ? 2 : 1;
        }

        animator.SetFloat("Speed", speedParameter);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void Flip()
    {
        if (moveInput > 0)
            spriteRenderer.flipX = true;
        
        else if (moveInput < 0)
            spriteRenderer.flipX = false;
    }

    // Called from an Animation Event
    public void EndAttack()
    {
        isAttacking = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}