using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaliteHealth : MonoBehaviour
{
    public int health;
    private int tempHealth;

    void Start()
    {
        tempHealth = health;
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
