using UnityEngine;

public class FishEnemy : MonoBehaviour
{
    [Header("Dati")]
    public int maxHealth = 2;
    public int damageToPlayer = 1;
    public float patrolSpeed = 2f;
    public float verticalAmplitude = 0.15f;
    public float verticalFrequency = 2f;
    public float autoPatrolDistance = 3f;
    public float damageCooldown = 0.8f;
    public Transform leftPoint;
    public Transform rightPoint;
    public LayerMask playerLayer;

    private int currentHealth;
    private bool isDead;
    private bool movingRight = true;
    private float startY;
    private float lastDamageTime = -999f;
    private Vector2 leftPos;
    private Vector2 rightPos;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.rotation = Quaternion.identity;

        currentHealth = maxHealth;
        startY = transform.position.y;

        if (leftPoint != null && rightPoint != null)
        {
            leftPos = leftPoint.position;
            rightPos = rightPoint.position;
        }
        else
        {
            leftPos = new Vector2(transform.position.x - autoPatrolDistance, transform.position.y);
            rightPos = new Vector2(transform.position.x + autoPatrolDistance, transform.position.y);
        }
    }

    void Update()
    {
        if (isDead) return;

        float wave = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;
        Vector2 target = movingRight ? rightPos : leftPos;
        transform.position = Vector2.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, startY + wave, transform.position.z);

        if (Vector2.Distance(transform.position, target) < 0.05f)
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastDamageTime < damageCooldown) return;

        lastDamageTime = Time.time;
        var ph = other.GetComponent<PlayerHealth>();
        if (ph != null) ph.TakeDamage(damageToPlayer);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth > 0)
        {
            if (anim != null) anim.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        if (anim != null) anim.SetTrigger("Dead");
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
        Destroy(gameObject, 1.2f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 lp = leftPoint ? leftPoint.position : new Vector3(transform.position.x - autoPatrolDistance, transform.position.y, 0);
        Vector3 rp = rightPoint ? rightPoint.position : new Vector3(transform.position.x + autoPatrolDistance, transform.position.y, 0);
        Gizmos.DrawSphere(lp, 0.06f);
        Gizmos.DrawSphere(rp, 0.06f);
        Gizmos.DrawLine(lp, rp);
    }
}
