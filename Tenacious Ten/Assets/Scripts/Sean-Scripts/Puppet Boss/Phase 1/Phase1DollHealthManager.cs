using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1DollHealthManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;
    public bool LeftDollKilled, RightDollKilled;

    [SerializeField]
    bool isLeftDoll, isRightDoll;

    [SerializeField]
    AudioSource hurtSound;

    StartOrResetLevel SORL;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        LeftDollKilled = false;
        RightDollKilled = false;

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
    }

    void FixedUpdate()
    {
        if(sr.color.a <= 1f)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.01f);
        }
        
        if (enemyHealth <= 1 && isLeftDoll)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            LeftDollKilled = true;
        }
        else if (enemyHealth <= 1 && isRightDoll)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            RightDollKilled = true;
        }
    }

    private void Update()
    {
        if(SORL.ResetFight)
        {
            LeftDollKilled = false;
            RightDollKilled = false;
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
        sr.color = new Color(1, 1, 0f, sr.color.a);
        yield return new WaitForSeconds(0.03f);
        sr.color = new Color(1, 1, 1, sr.color.a);
    }
}