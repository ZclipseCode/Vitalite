using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    //animation
    private SpriteRenderer renderer2d;
    private Animator animator;

    void Start()
    {
        //animation
        renderer2d = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //movement
        var movement = Input.GetAxis("Horizontal");
        //animation
        if (movement == 0)
        {
            animator.SetInteger("AnimState", 0);
        }
    }
}
