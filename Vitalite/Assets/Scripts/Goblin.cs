using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A*
using Pathfinding;

public class Goblin : MonoBehaviour
{
    //animation
    private SpriteRenderer renderer2d;
    private Animator animator;
    private float hitAnimTime;
    private bool isFading;
    private bool fadeActivated;

    //health
    public float health;
    private float tempHealth;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public CircleCollider2D cc;

    //A*
    public AIPath aiPath;
    public AIBase aibase;

    void Start()
    {
        //animation
        renderer2d = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isFading = false;
        fadeActivated = false;

        //health
        tempHealth = health;
    }

    void Update()
    {
        //movement
        //var movement = Input.GetAxis("Horizontal"); //this tracks player input
        var movement = transform.position.x;

        //A*
        if (health > 0)
        {
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);

            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        
        //animation
        if (movement == 0)
        {
            animator.SetInteger("AnimState", 0);
        } else
        {
            animator.SetInteger("AnimState", 1);
        }

        if (tempHealth != health && health <= 0)
        {
            hitAnimTime += Time.deltaTime;
            animator.SetInteger("AnimState", 4);
            aibase.maxSpeed = 0f;
            Destroy(rb);
            Destroy(bc);
            Destroy(cc);
            isFading = true;
            if (isFading && !fadeActivated)
            {
                startFading();
                fadeActivated = true;
            }
            if (hitAnimTime >= 1f)
            {
                Destroy(gameObject);
            }
            
        } else if (tempHealth != health)
        {
            hitAnimTime += Time.deltaTime;
            animator.SetInteger("AnimState", 3);
            if (hitAnimTime >= 0.4f)
            {
                hitAnimTime = 0f;
                tempHealth = health;
            }
        }
    }

    //health
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    //fade-out
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            Color c = renderer2d.material.color;
            c.a = f;
            renderer2d.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startFading()
    {
        StartCoroutine("FadeOut");
    }
}
