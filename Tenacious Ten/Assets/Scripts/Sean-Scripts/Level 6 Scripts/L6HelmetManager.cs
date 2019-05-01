using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6HelmetManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;
	ChariotMiniReset4 track;
	bool skyStarter = false;

	[SerializeField] public bool helmetIsDead;

    [SerializeField] AudioSource hurtSound;

    public bool moveToStart;
    [SerializeField] float moveSpeedLeft;

    public Level6Manager L6Manager;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        helmetIsDead = false;
        moveToStart = false;
        L6Manager = FindObjectOfType<Level6Manager>();
		track = GetComponent<ChariotMiniReset4>();
	}

    void FixedUpdate()
    {
		if (enemyHealth <= 5)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            helmetIsDead = true;
        }
        if (moveToStart == false && L6Manager.TorsoDead)
        {
			if (skyStarter == false)
			{
				track.damageBenchmark = track.pushBack.TotalDamage;
				skyStarter = true;
			}
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