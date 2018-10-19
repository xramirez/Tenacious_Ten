using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {


    Vector3 StartingPos;
    Vector3 StartingScale;

    float startingPosX;

    public bool facingLeft;
    public bool isIdle;
    bool HitOneDone;
    bool HitTwoDone;
    bool ComboFinished;

    public float dashSpeedX;

    public GameObject leftFireball;
    public GameObject rightFireball;
    public GameObject Explosion;

    public GameObject LeftPlatform;
    public GameObject RightPlatform;
    public GameObject MiddlePlatform;

    public StartOrResetLevel SORL;

    Rigidbody2D rb;
    public Animator anim;
    EnemyHealthManager EHM;

    Transform projectilePos;
    Transform explosionPos;
    float explosionBoundary;

    Vector3 explosionStart;

    //timers / delays for projectiles and such
    public float shotDelay;
    private float shotDelayCounter;
    public float dashDelay;
    private float dashDelayCounter;
    float HitTimer;
    float BlastTimer;

    float fadeTimer;
    float fadeAway;
    float heroicLandingTimer;

    public int PhaseOneHP;
    public int PhaseTwoHP;
    public int PhaseThreeHP;

    bool blasted, driftedBack, driftDir;
    public bool start3hit;
    bool isInvisible, hasTeleported, heroicLanding, hoverBack, stepOnePhase3, XmoveCorrect;

    public bool phaseOneComplete;
    public bool phaseTwoComplete;

    float scalingNum;
    float StartPhase3;

    private GameObject instantiatedObj;

    public int PhaseOneRandomizer;
    public int PhaseTwoRandomizer;

    public bool StartFight;
    
    public int ThreeHitCounter;

    public int FireBallCounter;
    public int DashAttackCounter;

    public float PhaseOneBegin;

    public float FireballDelay;

    // Use this for initialization
    void Start()
    {

        StartingPos = transform.localPosition;
        StartingScale = transform.localScale;
        startingPosX = transform.position.x;

        facingLeft = true;
        isIdle = true;
        HitOneDone = false;
        HitTwoDone = false;
        ComboFinished = false;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EHM = GetComponent<EnemyHealthManager>();
        SORL = FindObjectOfType<StartOrResetLevel>();

        HitTimer = 1f;
        BlastTimer = 0.7f;
        fadeTimer = 0.3f;
        fadeAway = 0.1f;

        FireballDelay = 2f;

        projectilePos = transform.Find("projectilePos");
        explosionPos = transform.Find("explosionPos");

        LeftPlatform = GameObject.Find("Left platform");
        RightPlatform = GameObject.Find("Right platform");
        MiddlePlatform = GameObject.Find("Middle platform");

        explosionBoundary = explosionPos.position.x;
        explosionStart = explosionPos.position;

        blasted = false;
        driftedBack = false;
        start3hit = false;

        isInvisible = false;
        hasTeleported = false;
        heroicLanding = false;
        hoverBack = false;
        stepOnePhase3 = false;

        scalingNum = 1.5f;
        heroicLandingTimer = 0.8f;

        phaseOneComplete = false;
        phaseTwoComplete = false;
        XmoveCorrect = false;

        StartPhase3 = 3f;

        StartFight = false;

        ThreeHitCounter = 0;

        PhaseOneBegin = 5f;

        FireBallCounter = 0;
        DashAttackCounter = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(SORL.StartFight)
        {
            StartFight = true;
            PhaseOneBegin -= Time.deltaTime;
        }
        if (SORL.ResetFight)
        {
            EHM.enemyHealth = EHM.maxHealth;
            StartFight = false;
            ResetFight();
        }
        if(!phaseOneComplete && StartFight && PhaseOneBegin <= 0)//if(EHM.enemyHealth >= PhaseOneHP)    //phase 1 - dash attack or fireball
        {
            
            //phaseOneComplete = false;
            if (isIdle)
            {
                anim.SetInteger("State", 0);
            }

            shotDelayCounter -= Time.deltaTime;

            PhaseOneRandomizer = Random.Range(0, 2);

            if (shotDelayCounter <= 0 && isIdle)
            {
                if(FireBallCounter >= 3)
                {
                    PhaseOneRandomizer = 1;
                    FireBallCounter = 0;
                }
                else if (DashAttackCounter >= 3)
                {
                    PhaseOneRandomizer = 0;
                    DashAttackCounter = 0;
                }
                if (PhaseOneRandomizer == 0 && FireBallCounter < 3)
                {
                    anim.SetInteger("State", 1);
                    CastFireball();
                    FireBallCounter++;
                }

                if (PhaseOneRandomizer == 1 && DashAttackCounter < 3)
                {
                    if (facingLeft)
                    {
                        DashAttackLeftFull();
                    }
                    else if (!facingLeft)
                    {
                        DashAttackRightFull();
                    }
                    DashAttackCounter++;
                }
                
                if(EHM.enemyHealth  < PhaseTwoHP)
                {
                    anim.SetInteger("State", 0);
                    phaseOneComplete = true;
                }
                shotDelayCounter = shotDelay;

                //if (SORL.ResetFight)
                //{
                //    ResetFight();
                //}
            }
            StopDash();
        }//end phase 1
        StopDash();
        

        if (!phaseTwoComplete && phaseOneComplete && StartFight) //(EHM.enemyHealth < PhaseTwoHP && EHM.enemyHealth > PhaseThreeHP && phaseOneComplete)    //phase2 - 3 hit combo or cast seals
        {
            //phaseTwoComplete = false;   


            //PhaseTwoRandomizer = Random.Range(0, 2);


            //PhaseTwoRandomizer = 1;
            //PhaseTwoRandomizer = 0;
            if(isIdle)
            {
                //anim.SetInteger("State", 0);
            }

            if(PhaseTwoRandomizer == 0 && isIdle && start3hit == false && ThreeHitCounter < 2)
            {
                //Debug.Log(PhaseTwoRandomizer);
                Debug.Log(ThreeHitCounter);
                ThreeHitCounter++;
                isIdle = false;
                start3hit = true;
                driftDir = DriftLeftOrRight();
            }

            if (start3hit)
            {
                HitTimer -= Time.deltaTime;
                if (HitTimer <= 0 && !ComboFinished) //initiate beginning of three hit combo
                {
                    if (!HitOneDone)
                    {
                        //isIdle = false;
                        anim.SetInteger("State", 3);
                        ThreeHitCombo();
                        HitOneDone = true;
                    }
                    else if (HitOneDone && !HitTwoDone)
                    {
                        anim.SetInteger("State", 4);
                        ThreeHitCombo();
                        HitTwoDone = true;
                    }
                    else if (HitTwoDone && HitOneDone)
                    {
                        anim.SetInteger("State", 5);
                        ThreeHitCombo();
                        ComboFinished = true;
                    }
                    HitTimer = 0.75f;
                }//end three hit combo

                if (blasted == false && ComboFinished) //explosions after 3 hit blast
                {
                     BlastTimer -= Time.deltaTime;
                     if (BlastTimer <= 0)
                     {
                        ThreeHitBlast(explosionPos.position); //explosionPos.position
                        BlastTimer = 0.15f;
                        if (explosionPos.transform.position.x <= -13 && facingLeft)  //10.6 is value of leftmost edge of camera     //MIGHT WANT TO CHANGE LATER ON SWITCHING EXPLOSIONS
                        {
                            blasted = true;
                            explosionPos.transform.position = new Vector3(explosionBoundary - 3f, explosionPos.transform.position.y, 0f);
                        }
                        if (explosionPos.transform.position.x >= 10 && !facingLeft)
                        {
                            blasted = true;
                            explosionPos.transform.position = new Vector3(-explosionBoundary + 3f, explosionPos.transform.position.y, 0f);
                        }
                     }

                }//end explosions

                if (blasted) //drift back to either left or right depending on which direction boss is facing, occurs after setting off explosions
                {
                    anim.SetInteger("State", 0);
                    if (!driftDir)//(facingLeft)
                    {
                        if (transform.position.x <= startingPosX)
                        {
                            transform.position = new Vector3(transform.position.x + 0.15f, transform.position.y, transform.position.z);
                        }
                        else
                        {
                            driftedBack = true;
                            isIdle = true;
                        }
                        if (!facingLeft)
                        {
                            Flip();
                        }
                    }
                    if (driftDir) //(!facingLeft)
                    {
                        if (transform.position.x >= -startingPosX)
                        {
                            transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y, transform.position.z);
                        }
                        else
                        {
                            isIdle = true;
                            driftedBack = true;
                        }
                        if (facingLeft)
                        {
                            Flip();
                        }
                    }

                    if(driftedBack || SORL.ResetFight)
                    {
                        HitOneDone = false;
                        HitTwoDone = false;
                        ComboFinished = false;
                        blasted = false;
                        driftedBack = false;
                        start3hit = false;
                        BlastTimer = 0.7f;
                        isIdle = true;
                        if (EHM.enemyHealth <= PhaseThreeHP)
                        {
                            phaseTwoComplete = true;
                        }
                    }
                    //blasted = !blasted;
                }
            } //end if randomizer is 0 i.e. do 3 hit combo

            //PROJECTILE/SEAL CASTS ARE IN SEPARATE CODE

            if (ThreeHitCounter >= 2)
            {
                //Debug.Log(ThreeHitCounter);
                PhaseTwoRandomizer = 1;
                ThreeHitCounter = 0;
            }
            PhaseTwoRandomizer = Random.Range(0, 2);

        }//end phase 2

        if(phaseTwoComplete && StartFight) //if (EHM.enemyHealth < PhaseThreeHP) //phase 3
        {
            StartPhase3 -= Time.deltaTime;
            if (transform.position.x <= 9.92 && !XmoveCorrect && StartPhase3 <= 0)
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
            if (transform.position.x >= 9.92 && transform.position.y <= -1.5 && !hasTeleported && !stepOnePhase3 && isIdle) //first part of entering phase, drift boss to platform level
            {
                if(!facingLeft)
                {
                    Flip();
                }
                facingLeft = true;
                XmoveCorrect = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
                explosionPos.transform.position = new Vector3(transform.position.x - 2f, explosionPos.transform.position.y, 0f);
            }


            fadeTimer -= Time.deltaTime;
            if(fadeTimer <= 0 && !isInvisible && !stepOnePhase3 && isIdle && XmoveCorrect)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - fadeAway);
                fadeAway += 0.1f;
                fadeTimer = 0.2f;
                if (fadeAway >= 1)
                {
                    isIdle = false;
                    stepOnePhase3 = true;
                    isInvisible = true;
                    fadeAway = 0.1f;
                }
            }

            if (isInvisible && !hasTeleported)
            {
                GameObject somePlatform = PickAPlatform();
                transform.position = new Vector3(somePlatform.transform.position.x + 0.8f, somePlatform.transform.position.y + 3.7f, somePlatform.transform.position.z);
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                hasTeleported = true;
                isInvisible = false;
            }
            //add in timer here before dropping
            if(hasTeleported)
            {
                heroicLandingTimer -= Time.deltaTime;
            }
            
            if (hasTeleported && !heroicLanding && heroicLandingTimer <= 0)
            {
                anim.SetInteger("State", 7);
                transform.position = new Vector3(transform.position.x, transform.position.y-0.3f, transform.position.z);
                if (transform.position.y <= -4.40f) 
                {
                    heroicLandingTimer = 0.8f;
                    heroicLanding = true;
                    hasTeleported = false;
                }
            }

            if (heroicLanding && !hoverBack)
            {
                BlastTimer -= Time.deltaTime;
                if (BlastTimer <= 0)
                {
                    ExplosionsBothWay(explosionPos.position, scalingNum);
                    //scalingNum += 3f;
                    if(facingLeft)
                    {
                        scalingNum += 3f;
                    }
                    else if(!facingLeft)
                    {
                        scalingNum -= 3f;
                    }
                    BlastTimer = 0.15f;
                    if (explosionPos.transform.position.x <= -25 && facingLeft)  //10.6 is value of leftmost edge of camera 
                    {
                        explosionPos.transform.position = new Vector3(transform.position.x - 2f, explosionPos.transform.position.y, 0f);//reset back to Boss
                        hoverBack = true;
                        heroicLanding = false;
                        scalingNum = 3f;
                    }
                    if (explosionPos.transform.position.x >= 20 && !facingLeft)
                    {
                        explosionPos.transform.position = new Vector3(transform.position.x + 2f, explosionPos.transform.position.y, 0f);//reset back to Boss
                        hoverBack = true;
                        heroicLanding = false;
                        scalingNum = 3f;
                    }
                }
            }


            if (hoverBack)
            {
                anim.SetInteger("State", 0);
                if (fadeTimer <= 0 && hoverBack)
                {
                    GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - fadeAway);
                    fadeAway += 0.16f;
                    fadeTimer = 0.2f;
                    if (fadeAway >= 1)
                    {
                        fadeAway = 0.1f;
                        transform.position = new Vector3(9.92f, -1.4f, 0f);
                        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                        hoverBack = false;
                        isInvisible = false;
                        hasTeleported = false;
                        heroicLanding = false;
                        stepOnePhase3 = false;
                        //XmoveCorrect = false;
                        isIdle = true;
                    }
                }

            }

        }//end phase 3

        if (SORL.ResetFight)
        {
            EHM.enemyHealth = EHM.maxHealth;
            StartFight = false;
            ResetFight();
        }

    }
    
    /////////////////////////////////


    void Flip() //flip boss depending on side
    {
        facingLeft = !facingLeft;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    void DashAttackLeftFull()   //dash attack facing left the length of the whole room (full)
    {
        anim.SetInteger("State", 2);
        rb.velocity = new Vector3(-dashSpeedX, rb.velocity.y, 0);
        isIdle = false;
    }

    void DashAttackRightFull()  //dash attack facing right the length of the whole room (full)
    {
        anim.SetInteger("State", 2);
        rb.velocity = new Vector3(dashSpeedX, rb.velocity.y, 0);
        isIdle = false;
    }

    void StopDash() //stops dash attack
    {
        if (transform.position.x >= startingPosX && !facingLeft || transform.position.x <= -startingPosX && facingLeft)
        {
            rb.velocity = new Vector3(0, 0, 0);
            isIdle = true;
            Flip();
        }
    }

    void CastFireball() //cast fireball / projectile
    {
        if(facingLeft)
        {
            Instantiate(leftFireball, projectilePos.position, Quaternion.identity);
        }
        else if (!facingLeft)
        {
            Instantiate(rightFireball, projectilePos.position, Quaternion.identity);
        }
        
    }

    void ThreeHitCombo() //moves boss per strike
    {
        if (facingLeft)
        {
            gameObject.transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z);
        }
        if (!facingLeft)
        {
            gameObject.transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z);
        }
    }

    void ThreeHitBlast(Vector3 location) //explosion on end of 3 hit strike
    {
        //Instantiate(Explosion, explosionPos.position, Quaternion.identity);
        //Instantiate(Explosion, location, Quaternion.identity);
        instantiatedObj = Instantiate(Explosion, location, Quaternion.identity);
        Destroy(instantiatedObj, 0.5f);
        if(facingLeft)
        {
            explosionPos.transform.position = new Vector3(explosionPos.transform.position.x - 1.5f, explosionPos.transform.position.y, 0f);
        }
        if(!facingLeft)
        {
            explosionPos.transform.position = new Vector3(explosionPos.transform.position.x + 1.5f, explosionPos.transform.position.y, 0f);
        }
    }

    void ExplosionsBothWay(Vector3 location, float increment)
    {
        instantiatedObj = Instantiate(Explosion, location, Quaternion.identity);
        Vector3 temp = location;

        temp.x += increment;
        location = temp;
        Destroy(instantiatedObj, 0.5f);

        instantiatedObj = Instantiate(Explosion, location, Quaternion.identity);
        temp.x -= increment;
        location = temp;

        Destroy(instantiatedObj, 0.5f);
        if (facingLeft)
        {
            explosionPos.transform.position = new Vector3(explosionPos.transform.position.x - 1.5f, explosionPos.transform.position.y, 0f);
        }
        if (!facingLeft)
        {
            explosionPos.transform.position = new Vector3(explosionPos.transform.position.x + 1.5f, explosionPos.transform.position.y, 0f);
        }
    }


    bool DriftLeftOrRight()
    {
        //bool MoveLeft = false;
        //bool MoveRight = false;
        PhaseTwoRandomizer = Random.Range(0, 2);
        if (PhaseTwoRandomizer == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    GameObject PickAPlatform()
    {
        int phase3randomizer = Random.Range(0, 3);
        if(phase3randomizer == 0)
        {
            return LeftPlatform;
        }
        else if (phase3randomizer == 1)
        {
            return RightPlatform;
        }
        else
        {
            return MiddlePlatform;
        }
    }

    void ResetFight()
    {
        anim.SetInteger("State", 0);
        transform.localPosition = StartingPos;
        transform.localScale = StartingScale;
        rb.velocity = new Vector3(0, 0, 0);

        phaseOneComplete = false;
        phaseTwoComplete = false;
        FireballDelay = 0.5f;

        facingLeft = true;
        isIdle = true;
        StartPhase3 = 3f;
        FireBallCounter = 0;
        DashAttackCounter = 0;

        //phase 2 reset stuff below
        HitOneDone = false;
        HitTwoDone = false;
        ComboFinished = false;
        blasted = false;
        BlastTimer = 0.7f;
        explosionPos.position = explosionStart;

        ThreeHitCounter = 0;

        //phase 3 reset stuff below
        XmoveCorrect = false;
        hoverBack = false;
        isInvisible = false;
        hasTeleported = false;
        heroicLanding = false;
        stepOnePhase3 = false;
        fadeAway = 0.1f;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - fadeAway);
    }
}
