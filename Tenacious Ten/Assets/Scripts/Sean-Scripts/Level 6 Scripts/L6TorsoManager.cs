using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6TorsoManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;
	ChariotMiniReset3 track;
	[SerializeField]
	ChariotMiniReset4 track4;
	bool skyStarter = false;

	[SerializeField] public bool torsoIsDead;

    [SerializeField] AudioSource hurtSound;

    public bool moveToStart;
    [SerializeField] float moveSpeedLeft;

    public Level6Manager L6Manager;

	[SerializeField] GameObject BouncyBall;
	Transform EmitLoc;
	int currentCannon = 1;// 1 2 and 3
	[SerializeField]
	public bool readyToShoot = false;
	[SerializeField] float shotTimer;
	float initShotTimer;
	[SerializeField] int shotCounter;
	int reloadNumber;
	[SerializeField]
	float reloadTime;

	[SerializeField]
	AudioSource cannonSound;

	void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        torsoIsDead = false;
        moveToStart = false;
        L6Manager = FindObjectOfType<Level6Manager>();
		track = GetComponent<ChariotMiniReset3>();
		EmitLoc = this.gameObject.transform.GetChild(currentCannon);
		readyToShoot = false;
		initShotTimer = shotTimer;
		reloadNumber = shotCounter;
	}

    void FixedUpdate()
    {
        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            torsoIsDead = true;
        }
        if (moveToStart == false && L6Manager.handsDead)
        {
			if (skyStarter == false)
			{
				track.damageBenchmark = track.pushBack.TotalDamage;
				track4.GetComponent<ChariotMiniReset4>().damageBenchmark = track4.GetComponent<ChariotMiniReset4>().pushBack.TotalDamage;
				skyStarter = true;
			}
			transform.position = new Vector3(transform.position.x - moveSpeedLeft, transform.position.y, transform.position.z);
            if (transform.position.x <= -53)
            {
                moveToStart = true;
				readyToShoot = true;
            }
        }

		if (readyToShoot)
		{
			shotTimer -= Time.deltaTime;
			if (shotTimer <= 0 && shotCounter >= 0)
			{
				cannonSound.Play();
				shotCounter--;
				shotTimer = initShotTimer;
				Instantiate(BouncyBall, EmitLoc.position, Quaternion.identity); // Quaternion.Euler(0f, 0f, 90f));
				if (currentCannon == 1)
					currentCannon = 2;
				else if (currentCannon == 2)
					currentCannon = 3;
				else if (currentCannon == 3)
					currentCannon = 1;
				EmitLoc = this.gameObject.transform.GetChild(currentCannon);
			}

			if (shotCounter <= 0)
			{
				readyToShoot = false;
				shotCounter = reloadNumber;
				StartCoroutine(reload(reloadTime));
			}
		}
	}

    public void giveDamage(int damageToGive)
    {
        hurtSound.Play();
        enemyHealth -= damageToGive;
        StartCoroutine(flash());
    }

	IEnumerator reload(float reloadTime)
	{
		yield return new WaitForSeconds(reloadTime);
		readyToShoot = true;
	}

    IEnumerator flash()
    {
        sr.color = new Color(1, 1, 0.3f, 1);
        yield return new WaitForSeconds(0.03f);
        sr.color = new Color(1, 1, 1, 1);
    }
}