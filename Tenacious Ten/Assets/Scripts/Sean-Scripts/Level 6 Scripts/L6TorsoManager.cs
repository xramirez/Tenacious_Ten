using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6TorsoManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;

    [SerializeField] public bool torsoIsDead;

    [SerializeField] AudioSource hurtSound;

    bool moveToStart;
    [SerializeField] float moveSpeedLeft;

    public Level6Manager L6Manager;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        torsoIsDead = false;
        moveToStart = false;
        L6Manager = FindObjectOfType<Level6Manager>();
    }

    void FixedUpdate()
    {
        if (enemyHealth <= 5)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            torsoIsDead = true;
        }
        if (moveToStart == false && L6Manager.handsDead)
        {
            transform.position = new Vector3(transform.position.x - moveSpeedLeft, transform.position.y, transform.position.z);
            if (transform.position.x <= -53)
            {
                moveToStart = true;
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