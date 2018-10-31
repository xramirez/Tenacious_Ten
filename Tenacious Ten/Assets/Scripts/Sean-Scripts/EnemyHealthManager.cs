using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    //public int pointsOnDeath;     MAY ADD LATER IF WE INTRODUCE A POINT SYSTEM

    // Use this for initialization
    void Start()
    {
        maxHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive)
    {

        enemyHealth -= damageToGive;

    }


}