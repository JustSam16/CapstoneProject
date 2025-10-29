using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento base")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Abilità speciali")]
    public bool canCrouch;
    public bool canSwim;
    private bool isCrouching;
    private bool isSwimming;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isWalking;

    private Animator anim;
    private CapsuleCollider2D capsule;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        
        canCrouch = PlayerPrefs.GetInt("CrouchUnlocked", 0) == 1;
        canSwim = PlayerPrefs.GetInt("SwimUnlocked", 0) == 1;
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        HandleCrouch();
        HandleSwim();

        if (anim != null)
        {
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isCrouching", isCrouching);
            anim.SetBool("isSwimming", isSwimming);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x * 100f) / 100f, Mathf.Round(pos.y * 100f) / 100f, pos.z);
    }

    void HandleCrouch()
    {
        if (!canCrouch) return;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isCrouching = true;
            if (capsule != null)
                capsule.size = new Vector2(capsule.size.x, 0.6f);
        }
        else
        {
            isCrouching = false;
            if (capsule != null)
                capsule.size = new Vector2(capsule.size.x, 1f);
        }
    }

    void HandleSwim()
    {
        if (!canSwim) return;

        if (isSwimming)
        {
            float vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * 3f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (canSwim && other.CompareTag("Water"))
        {
            isSwimming = true;
            rb.gravityScale = 0.3f;
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

    public bool GetIsWalking() => isWalking;
    public bool GetIsGrounded() => isGrounded;
}
