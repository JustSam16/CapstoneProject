using UnityEngine;

public class EnemyWalker2D : MonoBehaviour
{
    public float speed = 1.5f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public float wallCheckDistance = 0.15f;
    public float ledgeCheckDistance = 0.6f;

    int dir = 1;
    SpriteRenderer sr;
    Animator anim;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.right * dir * speed * Time.fixedDeltaTime);

        if (anim != null)
            anim.SetBool("isWalking", true);

        bool hitWall = Physics2D.Raycast(wallCheck.position, Vector2.right * dir, wallCheckDistance, groundLayer);
        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, ledgeCheckDistance, groundLayer);

        if (hitWall || noGround)
            Flip();
    }

    void Flip()
    {
        dir *= -1;
        if (sr != null)
            sr.flipX = dir > 0;

        Vector3 wcPos = wallCheck.localPosition;
        wcPos.x = Mathf.Abs(wcPos.x) * dir;
        wallCheck.localPosition = wcPos;
    }
}
