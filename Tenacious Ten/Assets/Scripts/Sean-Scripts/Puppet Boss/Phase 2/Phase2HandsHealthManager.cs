using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2HandsHealthManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;
    public bool LeftHandKilled, RightHandKilled;

    [SerializeField]
    bool isLeftHand, isRightHand;

    [SerializeField]
    AudioSource hurtSound;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        LeftHandKilled = false;
        RightHandKilled = false;
    }

    void FixedUpdate()
    {
        if (enemyHealth <= 10 && isLeftHand)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject,1f);
            LeftHandKilled = true;
        }
        if (enemyHealth <= 10 && isRightHand)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject,1f);
            RightHandKilled = true;
        }
    }

    public void giveDamage(int damageToGive)
    {
        hurtSound.Play();
        enemyHealth -= damageToGive;
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        sr.color = new Color(1, 1, 0.3f, 1);
        yield return new WaitForSeconds(0.03f);
        sr.color = new Color(1, 1, 1, 1);
    }
}