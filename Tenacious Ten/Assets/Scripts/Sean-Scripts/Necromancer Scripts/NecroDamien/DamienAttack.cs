using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamienAttack : MonoBehaviour {

    Animator anim;
    [SerializeField] GameObject RotatingSkull;
    [SerializeField] Transform skullSpawnLoc;
    bool RotatingSkullIsOut;

    [SerializeField] float RandomEmitTimeLeft;
    [SerializeField] float RandomEmitTimeRight;
    [SerializeField] float SkullEmitTimer;
    bool EmitTimerSet;

    [SerializeField] public float skullExploTimer;
    float initExploTimer;

    [SerializeField] GameObject FanAttack;
    [SerializeField] Transform FanAtkLoc;
    bool fanAttackSent;

    [SerializeField] bool FirstTimeAlive;
    bool readyToAttack;
    bool readyToMove;

    EnemyHealthManager Health;
    [SerializeField] int destroyAtThisHP;
    bool isBeingDestroyed;
    public bool FirstDeath;

    SpriteRenderer sr;
    float fadeInc;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        RotatingSkullIsOut = false;

        EmitTimerSet = false;

        isBeingDestroyed = false;

        initExploTimer = skullExploTimer;

        fanAttackSent = false;
        Health = GetComponent<EnemyHealthManager>();
        FirstDeath = false;
        fadeInc = 0f;

        sr = GetComponent<SpriteRenderer>();

        if (FirstTimeAlive)
        {
            readyToAttack = false;
            readyToMove = false;
            anim.SetInteger("State", 0);
        }
        else
        {
            readyToAttack = true;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(!readyToAttack)
        {
            if(!readyToMove)
            {
                StartCoroutine(waitForAnim(1.5f));
            }

            if(readyToMove)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, 0f);
                if (transform.position.y <= -4.58f)
                {
                    readyToAttack = true;
                }
            }
        }

        if(Health.enemyHealth <= destroyAtThisHP)
        {
            isBeingDestroyed = true;
            FirstDeath = true;
            anim.SetInteger("State", 3);
            sr.color = new Color(1f, 1f, 1f, sr.color.a - fadeInc);
            if(sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(waitForDeath(1.8f));
        }

        if(!EmitTimerSet)
        {
            EmitTimerSet = true;
            SkullEmitTimer = Random.Range(RandomEmitTimeLeft, RandomEmitTimeRight);
        }

        if(EmitTimerSet && readyToAttack && !isBeingDestroyed)
        {
            SkullEmitTimer -= Time.deltaTime;
            if(SkullEmitTimer <= .5f && SkullEmitTimer > 0f)
            {
                anim.SetInteger("State", 1);
            }
            else if(SkullEmitTimer <= 0)
            {
                if(!RotatingSkullIsOut)
                {
                    Instantiate(RotatingSkull, skullSpawnLoc.position, Quaternion.identity);
                    RotatingSkullIsOut = true;
                    anim.SetInteger("State", 0);
                }
                else if(RotatingSkullIsOut)
                {
                    skullExploTimer -= Time.deltaTime;
                    if(skullExploTimer <= 1.5f)
                    {
                        anim.SetInteger("State", 2);
                        if(skullExploTimer <= 0f)
                        {
                            if(!fanAttackSent)
                            {
                                Instantiate(FanAttack, FanAtkLoc.position, Quaternion.identity);
                                fanAttackSent = true;
                            }
                            if(skullExploTimer <= -0.8f)
                            {
                                fanAttackSent = false;
                                RotatingSkullIsOut = false;
                                EmitTimerSet = false;
                                anim.SetInteger("State", 0);
                                skullExploTimer = initExploTimer;
                            }
                        }

                    }
                }
                
            }

        }

	}//end FixedUpdate

    IEnumerator waitForAnim(float time)
    {
        yield return new WaitForSeconds(time);
        readyToMove = true;
    }

    IEnumerator waitForDeath(float time)
    {
        yield return new WaitForSeconds(time);
        fadeInc = 0.02f;
    }
}
