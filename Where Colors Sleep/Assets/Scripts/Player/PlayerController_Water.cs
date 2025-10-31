using UnityEngine;

public class PlayerController_Water : MonoBehaviour
{
    [Header("Movimento base")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("Abilità speciali")]
    public bool canCrouch = true;
    public bool canSwim = true;

    private bool isCrouching;
    private bool isSwimming;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isWalking;
    private float coyoteTimeCounter;
    private float coyoteTime = 0.1f;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsule;

    private Vector2 standSize;
    private Vector2 crouchSize;
    private Vector2 standOffset;
    private Vector2 crouchOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        standSize = capsule.size;
        crouchSize = new Vector2(capsule.size.x, capsule.size.y * 0.7f);
        standOffset = capsule.offset;
        crouchOffset = new Vector2(capsule.offset.x, capsule.offset.y - 0.15f);
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
        HandleSwim();
        UpdateAnimator();
    }

    void FixedUpdate()
    {
        CheckGrounded();
        if (!isCrouching && isGrounded)
            KeepFeetAligned();
    }

    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = moveInput * moveSpeed;
        rb.velocity = newVelocity;

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void HandleJump()
    {
        // Salto normale fuori dall'acqua
        if (Input.GetKeyDown(KeyCode.Space) && coyoteTimeCounter > 0f && !isSwimming)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0f;
        }

        // Movimento verticale in acqua (Space o W per salire)
        if (isSwimming)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
                rb.velocity = new Vector2(rb.velocity.x, 3f);
            else if (Input.GetKey(KeyCode.S))
                rb.velocity = new Vector2(rb.velocity.x, -3f);
        }
    }

    void HandleCrouch()
    {
        if (!canCrouch || isSwimming) return;

        bool crouchInput = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        if (crouchInput && !isCrouching && isGrounded)
        {
            isCrouching = true;
            capsule.size = crouchSize;
            capsule.offset = crouchOffset;
        }
        else if (!crouchInput && isCrouching)
        {
            if (CanStandUp())
            {
                isCrouching = false;
                capsule.size = standSize;
                capsule.offset = standOffset;
            }
        }
    }

    bool CanStandUp()
    {
        Vector2 castOrigin = new Vector2(transform.position.x, transform.position.y + crouchOffset.y);
        RaycastHit2D ceilingCheck = Physics2D.BoxCast(
            castOrigin,
            new Vector2(capsule.size.x * 0.8f, 0.1f),
            0f,
            Vector2.up,
            standSize.y - crouchSize.y + 0.1f,
            groundLayer
        );
        return ceilingCheck.collider == null;
    }

    void KeepFeetAligned()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        if (hit.collider != null)
        {
            float currentBottomY = transform.position.y + capsule.offset.y - (capsule.size.y / 2f);
            float targetBottomY = hit.point.y;
            float difference = targetBottomY - currentBottomY;

            if (Mathf.Abs(difference) > 0.005f && Mathf.Abs(difference) < 0.08f)
            {
                Vector3 newPos = transform.position;
                newPos.y += difference;
                transform.position = newPos;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    void HandleSwim()
    {
        if (!canSwim) return;
        if (isSwimming)
        {
            float horizontal = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canSwim && other.CompareTag("Water"))
        {
            isSwimming = true;
            rb.gravityScale = 0.3f;
            rb.velocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (canSwim && other.CompareTag("Water"))
        {
            isSwimming = false;
            rb.gravityScale = 3f;
        }
    }

    void UpdateAnimator()
    {
        if (anim == null) return;
        if (HasParameter(anim, "isWalking")) anim.SetBool("isWalking", isWalking);
        if (HasParameter(anim, "isGrounded")) anim.SetBool("isGrounded", isGrounded);
        if (HasParameter(anim, "isCrouching")) anim.SetBool("isCrouching", isCrouching);
        if (HasParameter(anim, "isSwimming")) anim.SetBool("isSwimming", isSwimming);
    }

    bool HasParameter(Animator animator, string paramName)
    {
        foreach (var param in animator.parameters)
            if (param.name == paramName)
                return true;
        return false;
    }

    public bool GetIsWalking() => isWalking;
    public bool GetIsGrounded() => isGrounded;
}
