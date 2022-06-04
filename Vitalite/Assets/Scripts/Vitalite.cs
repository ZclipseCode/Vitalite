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

    // Start is called before the first frame update
    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        renderer2D = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float jumpVelocity = 10f;
            body2D.velocity = Vector2.up * jumpVelocity;
        }

        body2D.AddForce(new Vector2 (forceX, forceY));
    }
}
