using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2((movingRight ? 1 : -1) * speed, rb.velocity.y);

        if (anim != null)
            anim.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0.1f);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.8f, groundLayer);
        Debug.DrawRay(groundCheck.position, Vector2.down * 0.8f, Color.green);

        if (!groundInfo)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        sr.flipX = !sr.flipX;
    }
}
