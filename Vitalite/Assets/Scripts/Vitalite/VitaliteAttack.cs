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

    private bool faceRight;

    //knockback
    public float thrust;
    public Rigidbody2D enemy;
    public float knockTime;

    void Start()
    {
        faceRight = true;
    }

    void Update()
    {
      if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Goblin>().TakeDamage(damage);

                    //knockback
                    if (enemy != null)
                    {
                        enemy.isKinematic = false;
                        Vector2 difference = enemy.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        enemy.AddForce(difference, ForceMode2D.Impulse);
                        StartCoroutine(KnockCo(enemy));
                    }
                }

                timeBtwAttack = startTimeBtwAttack;
            }

        }  else
        {
            timeBtwAttack -= Time.deltaTime;
        }

      if (Input.GetKeyDown(KeyCode.D))
        {
            faceRight = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            faceRight = false;
        }

      if (faceRight)
        {
            attackPos.transform.localPosition = new Vector3(0.33f, 0.43f);
        } else
        {
            attackPos.transform.localPosition = new Vector3(-0.33f, 0.43f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    //knockback
    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
        }
    }
}
