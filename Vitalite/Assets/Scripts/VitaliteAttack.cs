using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaliteAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator animator;
    public float attackRange;
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
      if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //animator.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Goblin>().TakeDamage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
            }

        }  else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
