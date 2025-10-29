using UnityEngine;
using System.Collections;

public class EnemySnail : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 0.8f;
    public float groundCheckDistance = 0.5f;
    public float wallCheckDistance = 0.2f;
    public Transform groundCheck;
    public Transform topCheck;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    [Header("Detection Settings")]
    public float detectionRadius = 1.5f;
    public float hideDuration = 3f;

    [Header("Combat Settings")]
    public int contactDamage = 1;
    public int maxHealth = 3;
    public float damageCooldown = 1f;

    [Header("Sprite Orientation")]
    public bool spriteFacesRight = false;

    int currentHealth;
    int dir = 1;
    bool isHiding = false;
    bool isMoving = true;
    bool isDead = false;
    bool canDamage = true;

    SpriteRenderer sr;
    Animator anim;
    BoxCollider2D boxCol;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        ApplySpriteFacing();

        
        bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
        if (!groundAhead)
            Flip();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (isMoving && !isHiding)
        {
            transform.Translate(Vector2.right * dir * speed * Time.fixedDeltaTime);

            bool groundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
            bool wallAhead = Physics2D.Raycast(transform.position, Vector2.right * dir, wallCheckDistance, groundLayer);

            if (!groundAhead || wallAhead)
                Flip();
        }

        if (!isHiding)
        {
            Collider2D playerNear = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
            if (playerNear)
                StartCoroutine(HideRoutine());
        }

        CheckPlayerSquash();
    }

    void CheckPlayerSquash()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(topCheck.position, 0.3f, playerLayer);
        if (playerHit && !isHiding && !isDead)
        {
            float playerBottom = playerHit.bounds.min.y;
            float snailTop = boxCol.bounds.max.y;
            if (playerBottom > snailTop)
                TakeDamage(maxHealth);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!canDamage || isDead) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(contactDamage);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isHiding) return;

        currentHealth -= damage;
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    IEnumerator HideRoutine()
    {
        isHiding = true;
        isMoving = false;
        anim.SetTrigger("Hide");
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("Hidden", true);
        yield return new WaitForSeconds(hideDuration);
        anim.SetBool("Hidden", false);
        anim.SetTrigger("Unhide");
        yield return new WaitForSeconds(0.5f);
        isHiding = false;
        isMoving = true;
    }

    IEnumerator Die()
    {
        isDead = true;
        boxCol.enabled = false;
        yield return new WaitForSeconds(0.2f);

        float t = 0f;
        float fadeTime = 0.6f;
        Color c = sr.color;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeTime);
            sr.color = c;
            yield return null;
        }

        Destroy(gameObject);
    }

    void Flip()
    {
        dir *= -1;
        ApplySpriteFacing();

        Vector3 pos = groundCheck.localPosition;
        pos.x = Mathf.Abs(pos.x) * dir;
        groundCheck.localPosition = pos;
    }

    void ApplySpriteFacing()
    {
        if (spriteFacesRight)
            sr.flipX = (dir < 0);
        else
            sr.flipX = (dir > 0);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * dir * wallCheckDistance);
    }
}
