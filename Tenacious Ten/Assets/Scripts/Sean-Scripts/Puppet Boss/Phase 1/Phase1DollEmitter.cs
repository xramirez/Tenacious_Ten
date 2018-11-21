using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1DollEmitter : MonoBehaviour {

    EnemyHealthManager EHM;
    PuppetBossManager BossManager;

    GameObject player;
    float playerLocation;
    bool playerFound;

    public GameObject LightningDoll;
    public Transform CeilingSpawn;

    bool firstDollShot, secondDollShot, thirdDollShot;
    bool hasWaitedToShoot;

    float startShootingTimer;
    Vector3 startingSpawn;
    StartOrResetLevel SORL;

	// Use this for initialization
	void Start () {

        EHM = GetComponent<EnemyHealthManager>();
        SORL = FindObjectOfType<StartOrResetLevel>();
        BossManager = GameObject.FindObjectOfType<PuppetBossManager>();

        firstDollShot = false;
        secondDollShot = false;
        thirdDollShot = false;
        hasWaitedToShoot = false;
        startingSpawn = CeilingSpawn.localPosition;
        startShootingTimer = 3f;
    }
	
	
	void FixedUpdate () {

        if (FindObjectOfType<PuppetBossManager>() != null)
        {
            BossManager = GameObject.FindObjectOfType<PuppetBossManager>();
        }

        if (BossManager.phaseOneActivated)
        {
            startShootingTimer -= Time.deltaTime;
            if(startShootingTimer <= 0)
            {
                Shoot3Dolls();
                if (thirdDollShot && hasWaitedToShoot)
                {
                    hasWaitedToShoot = false;
                    firstDollShot = false;
                    secondDollShot = false;
                    thirdDollShot = false;
                }
            }
        }

	}

    void Update()
    {
        if(SORL.ResetFight)
        {
            firstDollShot = false;
            secondDollShot = false;
            thirdDollShot = false;
            hasWaitedToShoot = false;
            startShootingTimer = 3f;
        }
    }

    void Shoot3Dolls()
    {
        Vector3 temp = startingSpawn;
        temp.x = temp.x + Random.Range(-5f, 5f);
        CeilingSpawn.localPosition = temp;
        if (!firstDollShot)
        {
            Instantiate(LightningDoll, CeilingSpawn.position, Quaternion.identity);
            firstDollShot = true;
            StartCoroutine(WaitForXseconds(1.5f));
        }
        else if(!secondDollShot && hasWaitedToShoot)
        {
            Instantiate(LightningDoll, CeilingSpawn.position, Quaternion.identity);
            secondDollShot = true;
            hasWaitedToShoot = false;
            StartCoroutine(WaitForXseconds(1.5f));
        }
        else if (!thirdDollShot && hasWaitedToShoot)
        {
            Instantiate(LightningDoll, CeilingSpawn.position, Quaternion.identity);
            thirdDollShot = true;
            hasWaitedToShoot = false;
            StartCoroutine(WaitForXseconds(2f));
        }
    }

    IEnumerator WaitForXseconds(float time)
    {
        yield return new WaitForSeconds(time);
        hasWaitedToShoot = true;
        if(startShootingTimer == 3)
        {
            hasWaitedToShoot = false;
        }
    }
}
