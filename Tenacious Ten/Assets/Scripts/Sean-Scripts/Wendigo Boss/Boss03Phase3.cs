using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Phase3 : MonoBehaviour {

    EnemyHealthManager EHM;
    PushBackRoar PBR;
    Boss03Manager Wendigo;
    StartOrResetLevel SORL;

    int phaseThreeRandomizer;
    float phaseThreeCounter;
    float BeginPhaseThree;
    bool phaseThreeActivated;

    bool BoomerangIsOut;
    bool isRoaring;

    public Transform BoomerangSpawn;
    public GameObject MeatBoomerang;

    public float pushBackTimer;
    public bool isPushingBack;
    bool hasFlipped;
    int RoarCounter;

    [SerializeField]
    AudioSource roar;

    float boomerangTimer;

	// Use this for initialization
	void Start () {
        EHM = GetComponent<EnemyHealthManager>();
        PBR = GetComponent<PushBackRoar>();
        Wendigo = GetComponent<Boss03Manager>();
        SORL = FindObjectOfType<StartOrResetLevel>();

        phaseThreeCounter = 2.5f;
        BeginPhaseThree = 2f;

        phaseThreeActivated = false;
        isPushingBack = false;
        hasFlipped = false;
        isRoaring = false;
        BoomerangIsOut = false;

        RoarCounter = 0;
        boomerangTimer = 0.5f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        phaseThreeRandomizer = Random.Range(0, 2);

        if(EHM.enemyHealth <= Wendigo.EnterPhaseThreeHP)
        {
            BeginPhaseThree -= Time.deltaTime;
            if(BeginPhaseThree <= 0)
            {
                phaseThreeActivated = true;
            }
        }

        if (phaseThreeActivated == true)
        {
            phaseThreeCounter -= Time.deltaTime;
        }

        if(phaseThreeCounter <= 0 && !BoomerangIsOut)
        {
            if (transform.position.x < 0.65 && Wendigo.facingLeft && !hasFlipped)
            {
                Wendigo.Flip();
                Wendigo.facingLeft = !Wendigo.facingLeft;
                hasFlipped = true;
            }
            else if(transform.position.x > 0.65 && !Wendigo.facingLeft && !hasFlipped)
            {
                Wendigo.Flip();
                Wendigo.facingLeft = !Wendigo.facingLeft;
                hasFlipped = true;
            }

            if(Wendigo.facingLeft)
            {
                Wendigo.anim.SetInteger("State", 1);
                if(transform.position.x > 0.65)
                {
                    transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
                }
                
                if(transform.position.x <= 0.65)
                {
                    Wendigo.Flip();
                    Wendigo.facingLeft = !Wendigo.facingLeft;

                    Wendigo.anim.SetInteger("State", 6);
                    boomerangTimer -= Time.deltaTime;
                    if (boomerangTimer <= 0)
                    {
                        roar.Play(); // Sound
                        ThrowBoomerang();
                        BoomerangIsOut = true;
                        phaseThreeCounter = 3f;
                        StartCoroutine(waitRoarBloodAnim(1.6f));
                    }
                }
            }
            else if(!Wendigo.facingLeft)
            {
                Wendigo.anim.SetInteger("State", 1);
                if (transform.position.x < 0.65)
                {
                    transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
                }
                if (transform.position.x >= 0.65)
                {
                    Wendigo.anim.SetInteger("State", 6);
                    boomerangTimer -= Time.deltaTime;
                    if(boomerangTimer <= 0)
                    {
                        ThrowBoomerang();
                        BoomerangIsOut = true;
                        phaseThreeCounter = 3f;
                        StartCoroutine(waitRoarBloodAnim(1.6f));
                    }
                }
            }
        }
        else if(phaseThreeRandomizer == 0 && !Wendigo.isJumping && phaseThreeCounter <= 0 && RoarCounter < 2)
        {
            if (pushBackTimer > 0)
            {
                roar.Play(); // Sound
                Wendigo.anim.SetInteger("State", 6);
                isRoaring = true;
                isPushingBack = true;
                PBR.Roar();
                pushBackTimer -= Time.deltaTime;
            }
            else
            {
                Wendigo.anim.SetInteger("State", 0);
                RoarCounter++;
                PBR.playerIsIdle = true;
                isRoaring = false;
                pushBackTimer = 5.5f;
                isPushingBack = false;
                phaseThreeCounter = 2.5f;
            }
        }
        else if(phaseThreeRandomizer == 1 && !isRoaring && phaseThreeCounter <= 0)
        {
            Wendigo.isJumping = true;
            Wendigo.phaseOneCounter = 0;
            phaseThreeCounter = 3f;
            if(RoarCounter >= 2)
            {
                RoarCounter = 0;
            }
        }
	}

    void Update()
    {
        if (SORL.ResetFight == true)
        {
            ResetFight();
        }
    }

    IEnumerator waitRoarBloodAnim(float time)
    {
        yield return new WaitForSeconds(time);
        Wendigo.anim.SetInteger("State", 0);
    }

    void ThrowBoomerang()
    {
        Instantiate(MeatBoomerang, BoomerangSpawn.position, Quaternion.identity); 
    }

    void ResetFight()
    {
        phaseThreeCounter = 2.5f;
        BeginPhaseThree = 2f;

        phaseThreeActivated = false;
        isPushingBack = false;
        hasFlipped = false;
        isRoaring = false;
        BoomerangIsOut = false;
        pushBackTimer = 5.5f;
        boomerangTimer = 0.5f;

        RoarCounter = 0;
    }
}
