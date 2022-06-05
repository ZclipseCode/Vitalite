using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitalite : MonoBehaviour
{
    public float speed = 150f;
    public Vector2 maxVelocity = new Vector2(60, 100);

    public bool faceLeft = false;

    private Rigidbody2D body2D;
    private SpriteRenderer renderer2D;
    private Animator animator;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        renderer2D = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var absVelX = Mathf.Abs(body2D.velocity.x);

        var forceX = 0f;
        var forceY = 0f;


        //right and left
        if (Input.GetKey("d"))
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = speed;

                if (faceLeft == true)
                {
                    renderer2D.flipX = forceX < 0;
                }
            }

            animator.SetInteger("AnimState", 1);
        } else if (Input.GetKey("a"))
        {
            if (absVelX < maxVelocity.x)
            {
                forceX = -speed;
                renderer2D.flipX = forceX < 0;

                faceLeft = true;
            }

            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }

        body2D.AddForce(new Vector2(forceX, forceY));

        //jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 6f;
            body2D.velocity = Vector2.up * jumpVelocity;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down * .1f, groundLayerMask);
        return raycastHit2d.collider;
    }
}
