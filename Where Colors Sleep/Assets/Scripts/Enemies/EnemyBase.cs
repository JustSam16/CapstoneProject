using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHealth = 3;
    public LayerMask playerLayer;
    public float topCheckRadius = 0.3f;
    public Transform topCheck;
    public int contactDamage = 1;
    public float damageCooldown = 1f;

    int currentHealth;
    bool isDead;
    bool canDamage = true;
    Animator anim;
    SpriteRenderer sr;
    BoxCollider2D boxCol;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        Collider2D playerHit = Physics2D.OverlapCircle(topCheck.position, topCheckRadius, playerLayer);

        // ✅ se non trova niente o non è il player, ignora
        if (playerHit == null) return;
        if (!playerHit.CompareTag("Player")) return;

        float playerBottom = playerHit.bounds.min.y;
        float slimeTop = boxCol.bounds.max.y;

        if (playerBottom > slimeTop)
        {
            DieBySquash();
            return;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isDead || !canDamage) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                float playerBottom = other.bounds.min.y;
                float slimeTop = boxCol.bounds.max.y;

                if (playerBottom <= slimeTop)
                {
                    playerHealth.TakeDamage(contactDamage);
                    StartCoroutine(DamageCooldown());
                }
            }
        }
    }

    System.Collections.IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (anim != null)
            anim.SetTrigger("Hit");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        if (anim != null)
            anim.SetTrigger("Death");
        DisableEnemy();
    }

    void DieBySquash()
    {
        isDead = true;
        if (anim != null)
            anim.SetTrigger("Squashed");
        StartCoroutine(FixPositionAfterSquash());
        DisableEnemy();
    }

    System.Collections.IEnumerator FixPositionAfterSquash()
    {
        yield return new WaitForSeconds(0.05f);
        if (boxCol != null)
        {
            float correction = boxCol.size.y * 0.5f;
            transform.position = new Vector3(transform.position.x, transform.position.y - correction, transform.position.z);
        }
    }

    void DisableEnemy()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        Destroy(gameObject, 0.8f);
    }
}
