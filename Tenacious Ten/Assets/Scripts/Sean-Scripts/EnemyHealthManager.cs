using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;
    [SerializeField]
    AudioSource hurtSound;

    public GameObject deathEffect;

    SpriteRenderer sr;

    bool hasFlashed;

    //public int pointsOnDeath;     MAY ADD LATER IF WE INTRODUCE A POINT SYSTEM

    // Use this for initialization
    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        hasFlashed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //sr.color = new Color(1, 1, 1, 1);
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
   
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive)
    {
        //sr.color = new Color(1, 1, 0.5f, 1);
        enemyHealth -= damageToGive;
        hurtSound.Play();
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        sr.color = new Color(1, 1, 0.3f, sr.color.a);
        yield return new WaitForSeconds(0.03f);
        sr.color = new Color(1, 1, 1, sr.color.a);
    }

}