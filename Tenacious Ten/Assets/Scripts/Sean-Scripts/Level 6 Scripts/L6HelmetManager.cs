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
    
    [SerializeField] float timeBetweenShots;
    float initTimeBetweenShots;

    [SerializeField] GameObject Laser;

    SpriteRenderer eyeSR;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        helmetIsDead = false;
        moveToStart = false;
        L6Manager = FindObjectOfType<Level6Manager>();
		track = GetComponent<ChariotMiniReset4>();

        initTimeBetweenShots = timeBetweenShots;

        eyeSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
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

        if (moveToStart)
        {
            timeBetweenShots -= Time.deltaTime;
            if(timeBetweenShots <= 1.5 && timeBetweenShots >= 0)
            {
                StartCoroutine(eyeFlashWarning());
            }
            else if(timeBetweenShots <= 0)
            {
                Instantiate(Laser, transform.GetChild(0).position, Quaternion.identity);
                timeBetweenShots = initTimeBetweenShots;
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

    IEnumerator eyeFlashWarning()
    {
        eyeSR.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.01f);
        eyeSR.color = new Color(1, 1, 1, 1);
    }
}