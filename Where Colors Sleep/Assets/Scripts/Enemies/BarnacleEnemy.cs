using UnityEngine;

public class BarnacleEnemy : MonoBehaviour
{
    [Header("Dati nemico")]
    public int maxHealth = 3;
    public int damageToPlayer = 1;
    public float attackCooldown = 1.5f;

    [Header("Riferimenti")]
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public LayerMask playerLayer;

    private int currentHealth;
    private bool isDead;
    private bool canAttack = true;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        if (rb != null) rb.isKinematic = true;
    }

    void Update()
    {
        if (!isDead)
        {
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (player != null && canAttack)
        {
            canAttack = false;
            anim.SetTrigger("Bite");
            player.GetComponent<PlayerHealth>()?.TakeDamage(damageToPlayer);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetTrigger("Dead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1.2f);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

