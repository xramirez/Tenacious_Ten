using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6BootsManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;

    [SerializeField] public bool bootsIsDead;

    [SerializeField] AudioSource hurtSound;

    public bool moveToStart;
    [SerializeField] float moveSpeedLeft;

    [SerializeField] float horizArrowTimerL;
    [SerializeField] float horizArrowTimerR;

    [SerializeField] float actualHorizTimer;

    [SerializeField] float bigArrowTimerL;
    [SerializeField] float bigArrowTimerR;

    [SerializeField] float actualBigTimer;


    bool horizArrowSet;
    bool bigArrowSet;
    float horizArrowTimer, bigArrowTimer;

    [SerializeField] GameObject horizontalArrow;
    [SerializeField] GameObject bigArrow;


    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        bootsIsDead = false;
        moveToStart = false;

        horizArrowSet = false;
        bigArrowSet = false;
    }

    void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            bootsIsDead = true;
        }

        if(moveToStart == false)
        {
            transform.position = new Vector3(transform.position.x-moveSpeedLeft, transform.position.y, transform.position.z);
            if (transform.position.x <= -53)
            {
                moveToStart = true;
            }
        }

        if(moveToStart)
        {
            if(!bigArrowSet)
            {
                bigArrowTimer = Random.Range(bigArrowTimerL, bigArrowTimerR);
                bigArrowSet = true;
            }

            if(!horizArrowSet)
            {
                horizArrowTimer = Random.Range(horizArrowTimerL, horizArrowTimerR);
                horizArrowSet = true;
            }

            if(bigArrowSet)
            {
                bigArrowTimer -= Time.deltaTime;
                if(bigArrowTimer <= 0)
                {
                    Instantiate(bigArrow, transform.GetChild(0).position, Quaternion.Euler(new Vector3(0, 0, 90)));
                    bigArrowSet = false;
                }
            }

            if (horizArrowSet)
            {
                horizArrowTimer -= Time.deltaTime;
                if (horizArrowTimer <= 0)
                {
                    Instantiate(horizontalArrow, transform.GetChild(1).position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    horizArrowSet = false;
                }
            }
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