using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsVitalite;
    public Animator animator;
    public float attackRange;
    public int damage;

    private bool faceRight;
    private bool attackOccurred;

    void Start()
    {
        faceRight = true;
        attackOccurred = false;
    }

    void Update()
    {
        if (faceRight)
        {
            attackPos.transform.localPosition = new Vector3(0.334f, -0.422f);
        }
        else
        {
            attackPos.transform.localPosition = new Vector3(-0.334f, -0.422f);
        }

        if (attackOccurred)
        {
            GetComponent<Goblin>().Animation(2);
            timeBtwAttack -= Time.deltaTime;

            if (timeBtwAttack <= 0)
            {
                attackOccurred = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeBtwAttack <= 0 && !attackOccurred)
        {
            if (collision.gameObject.layer == 8)
            {
                Collider2D[] vitaliteToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsVitalite);
                for (int i = 0; i < vitaliteToDamage.Length; i++)
                {
                    vitaliteToDamage[i].GetComponent<VitaliteHealth>().TakeDamage(damage);
                }

                timeBtwAttack = startTimeBtwAttack;
                attackOccurred = true;
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
