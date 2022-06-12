using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitalite : MonoBehaviour
{
    //movement
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D rigidbody2d;

    //animation
    private SpriteRenderer renderer2d;
    private Animator animator;
    private bool faceRight;
    private bool faceRightJump;

    private bool inAttack;
    private float attackAnimTime;

    private void Start() //set to private, I don't know if this matters
    {
        //movement
        rigidbody2d = GetComponent<Rigidbody2D>();

        //animation
        renderer2d = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        faceRight = true;
        faceRightJump = true;
        inAttack = false;
    }

    private void Update() //set to private, I don't know if this matters
    {
        //movement
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody2d.velocity.y) < 0.001f)
        {
            rigidbody2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        //animation
        if (Input.GetMouseButtonDown(0))
        {
            inAttack = true;
        }

        if (inAttack) 
        {
            animator.SetInteger("AnimState", 2);
            attackAnimTime += Time.deltaTime;

            if (attackAnimTime >= 0.6f)
            {
                inAttack = false;
                attackAnimTime = 0;
            }
        }
        else if (movement == 0 && Mathf.Abs(rigidbody2d.velocity.y) < 0.001f && !inAttack) //still
        {
            animator.SetInteger("AnimState", 0);
        }
        else if (Mathf.Abs(rigidbody2d.velocity.y) > 0.001f) //jump
        {
            animator.SetInteger("AnimState", 4);

            if (faceRightJump && movement > 0)
            {
                renderer2d.flipX = false;

                faceRightJump = false;
            }
            else if (!faceRightJump && movement < 0)
            {
                renderer2d.flipX = true;

                faceRightJump = true;
            }
        }
        else if (movement > 0) //right
        {
            if (faceRight == false)
            {
                renderer2d.flipX = false;

                faceRight = true;
            }

            animator.SetInteger("AnimState", 1);
        }
        else if (movement < 0)
        { //left
            if (faceRight == true)
            {
                renderer2d.flipX = true;

                faceRight = false;
            }

            animator.SetInteger("AnimState", 1);
        }
    }
}
