using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 2f;
    public float pauseDuration = 1f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.6f;
    public float wallCheckDistance = 0.5f;

    [Header("Vita")]
    public int health = 2;
    public float hitStunDuration = 0.3f;

    private enum SpiderState { Walking, Paused, Hit, Dead }
    private SpiderState currentState = SpiderState.Walking;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;
    private SpriteRenderer sr;

    private int direction = 1;
    private float pauseTimer;
    private float hitTimer;
    private bool shouldTurnAround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = 2f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (currentState == SpiderState.Dead) return;

        switch (currentState)
        {
            case SpiderState.Walking:
                HandleWalking();
                break;
            case SpiderState.Paused:
                HandlePause();
                break;
            case SpiderState.Hit:
                HandleHit();
                break;
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (currentState == SpiderState.Dead) return;

        CheckForObstacles();

        if (currentState == SpiderState.Walking)
            rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void HandleWalking()
    {
        if (shouldTurnAround)
        {
            TurnAround();
            shouldTurnAround = false;
        }
        transform.localScale = new Vector3(-direction, 1, 1);
    }

    void HandlePause()
    {
        pauseTimer -= Time.deltaTime;
        if (pauseTimer <= 0f)
            currentState = SpiderState.Walking;
    }

    void HandleHit()
    {
        hitTimer -= Time.deltaTime;
        if (hitTimer <= 0f)
        {
            if (health > 0)
                currentState = SpiderState.Walking;
            else
                Die();
        }
    }

    void CheckForObstacles()
    {
        Vector2 frontPos = new Vector2(transform.position.x + direction * 0.3f, transform.position.y);
        Vector2 groundPos = new Vector2(transform.position.x + direction * 0.4f, transform.position.y - 0.3f);

        bool hasWall = Physics2D.Raycast(frontPos, Vector2.right * direction, wallCheckDistance, groundLayer);
        bool hasGround = Physics2D.Raycast(groundPos, Vector2.down, groundCheckDistance, groundLayer);

        if (hasWall || !hasGround)
        {
            if (currentState == SpiderState.Walking)
            {
                shouldTurnAround = true;
                currentState = SpiderState.Paused;
                pauseTimer = pauseDuration;
            }
        }
    }

    void TurnAround()
    {
        direction *= -1;
    }

    public void TakeDamage(int dmg)
    {
        if (currentState == SpiderState.Dead) return;

        health -= dmg;
        if (health <= 0)
            Die();
        else
        {
            currentState = SpiderState.Hit;
            hitTimer = hitStunDuration;
            rb.velocity = Vector2.zero;
        }
    }

    void Die()
    {
        currentState = SpiderState.Dead;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        if (col != null) col.enabled = false;
        anim.SetBool("isDead", true);
        Destroy(gameObject, 1.5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState == SpiderState.Dead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y < -0.5f)
                {
                    TakeDamage(health);
                    Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                    if (playerRb != null)
                        playerRb.velocity = new Vector2(playerRb.velocity.x, 10f);
                    return;
                }
            }

            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
                ph.TakeDamage(1);
        }
    }

    void UpdateAnimation()
    {
        if (anim == null) return;
        anim.SetBool("isWalking", currentState == SpiderState.Walking);
        anim.SetBool("isHit", currentState == SpiderState.Hit);
        anim.SetBool("isDead", currentState == SpiderState.Dead);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 wallCheck = transform.position + new Vector3(direction * 0.3f, 0, 0);
        Gizmos.DrawLine(wallCheck, wallCheck + new Vector3(direction * wallCheckDistance, 0, 0));

        Gizmos.color = Color.green;
        Vector3 groundCheck = transform.position + new Vector3(direction * 0.4f, -0.3f, 0);
        Gizmos.DrawLine(groundCheck, groundCheck + new Vector3(0, -groundCheckDistance, 0));
    }
}
