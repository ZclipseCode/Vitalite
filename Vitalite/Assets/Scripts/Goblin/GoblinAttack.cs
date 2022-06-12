using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsVitalite;
    public Animator animator;
    public float attackRange;
    public int damage;

    private bool faceRight;

    void Start()
    {
        faceRight = true;
    }

    void Update()
    {
        if (faceRight)
        {
            attackPos.transform.localPosition = new Vector3(0.35f, -0.16f);
        }
        else
        {
            attackPos.transform.localPosition = new Vector3(-0.35f, -0.16f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeBtwAttack <= 0)
        {
            Collider2D[] vitaliteToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsVitalite);
            for (int i = 0; i < vitaliteToDamage.Length; i++)
            {
                vitaliteToDamage[i].GetComponent<VitaliteHealth>().TakeDamage(damage);
            }

            timeBtwAttack = startTimeBtwAttack;

        }
        else
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
