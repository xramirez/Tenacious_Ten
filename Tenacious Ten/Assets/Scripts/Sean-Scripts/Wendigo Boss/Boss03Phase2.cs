using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Phase2 : MonoBehaviour {

    int phaseTwoRandomizer;
    public GameObject boneProj;
    Transform boneProjPos;
    Transform bloodProjPos;
    public float phaseTwoCounter;
    float distanceToShoot;
    public GameObject BloodProj;
    float BloodChangeTimer;
    float BloodRotationAngle;
    GameObject[] projectiles;
    public Transform barrel;
    PlayerManager player;
    public bool isPushingBack;
    public float pushBackTimer;
    bool phaseTwoActivated;
    float BeginPhaseTwo;
    float RainBloodCounter;
    bool StartedRaining;
    bool isSpitting;
    bool isRoaring;
    int hasJumpedTwice;
    bool hasSpitOnce;
    bool hasWaited;
    bool rainRoarAnim;

    PushBackRoar PBR;
    RainingBlood rain;
    Boss03Manager Wendigo;
    Phase2BoneProj Phase2BoneProj;
    Phase2BloodProj HAUH;
    EnemyHealthManager EHM;
    StartOrResetLevel SORL;

    // Use this for initialization
    void Start () {
        boneProjPos = transform.Find("boneProjectilePos");
        bloodProjPos = transform.Find("bloodProjectilePos");
        phaseTwoCounter = 1.5f;
        Wendigo = GetComponent<Boss03Manager>();
        // = GetComponent<Phase2BoneProj>();
        BloodChangeTimer = 0;
        BloodRotationAngle = 0;
        isPushingBack = false;
        player = GetComponent<PlayerManager>();
        PBR = GetComponent<PushBackRoar>();
        rain = GetComponent<RainingBlood>();
        EHM = GetComponent<EnemyHealthManager>();
        SORL = FindObjectOfType<StartOrResetLevel>();
        pushBackTimer = 7f;
        phaseTwoActivated = false;
        BeginPhaseTwo = 3f;
        RainBloodCounter = 2f;
        StartedRaining = false;
        isSpitting = false;
        isRoaring = false;
        hasSpitOnce = false;
        hasJumpedTwice = 0;
        hasWaited = false;
        rainRoarAnim = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        phaseTwoRandomizer = Random.Range(0, 2);
        
        if(phaseTwoCounter <= 0)
        {
            if(!StartedRaining)
            {
                Wendigo.anim.SetInteger("State", 6);
                StartCoroutine(waitRoarBloodAnim(1f));
                phaseTwoCounter = 1.5f;
            }
            StartedRaining = true;
        }
        RainBloodCounter -= Time.deltaTime;
        

        if (StartedRaining && RainBloodCounter <= 0)
        {
            rain.RainOneLayerOfBlood();
            RainBloodCounter = 1.85f;       //ORIGINALLY 1.25F
        }

        if (phaseTwoActivated)
        {
            phaseTwoCounter -= Time.deltaTime;
        }

        if (phaseTwoRandomizer == 0 && phaseTwoCounter <= 0 && StartedRaining && !Wendigo.isJumping && hasJumpedTwice <= 0) // && isRoaring
        {
            isSpitting = true;
        }
        //else if (phaseTwoRandomizer == 1 && phaseTwoCounter <= 0 && !isSpitting && StartedRaining && !Wendigo.isJumping)
        //{
            //isRoaring = true;
        //}
        else if (phaseTwoRandomizer == 1 && phaseTwoCounter <= 0  && !isSpitting && StartedRaining)
        {
            Wendigo.isJumping = true;
            Wendigo.phaseOneCounter = 0;
            phaseTwoCounter = 2.8f;
        }
        if (Wendigo.jumpFinished)
        {
            hasJumpedTwice--;
            Debug.Log(hasJumpedTwice);
            Debug.Log("HAUH");
            if (hasJumpedTwice == 0)
            {
                hasSpitOnce = false;
            }
        }


        if (isSpitting) //if (phaseTwoCounter <= 0)// 
        {
            if(hasSpitOnce)
            {
                StartCoroutine(waitForBone(1.25f));
                if (hasWaited)
                {
                    ThrowUpBone();
                    phaseTwoCounter = 2.5f;
                    isSpitting = false;
                    hasJumpedTwice = 2;
                    hasSpitOnce = false;
                    hasWaited = false;
                    Wendigo.anim.SetInteger("State", 0);
                    Debug.Log(hasWaited);

                }
            }
            else if (!hasSpitOnce)
            {
                Wendigo.anim.SetInteger("State", 6);
                ThrowUpBone();
                hasSpitOnce = true;
            }
            //StartCoroutine(waitForBone(0.5f));
            //if(hasSpitOnce)
            //{
            //phaseTwoCounter = 2.5f;
            //isSpitting = false;
            //hasJumpedTwice = 2;
            //}
            //hasSpitOnce = true;
        }
        if (false)//if (isRoaring)
        {
            if(pushBackTimer > 0)
            {
                isPushingBack = true;
                PBR.Roar();
            }
            else
            {
                PBR.playerIsIdle = true;
                isRoaring = false;
                pushBackTimer = 10f;
                isPushingBack = false;
                phaseTwoCounter = 2f;
            }
        }
        
        //BeginPhaseTwo -= Time.deltaTime;
        //if(BeginPhaseTwo <= 0)
        //{
         //   if (EHM.enemyHealth <= Wendigo.EnterPhaseTwoHP && EHM.enemyHealth > Wendigo.EnterPhaseThreeHP)
          //  {
           //     phaseTwoActivated = true;
           // }
           // else if (EHM.enemyHealth <= Wendigo.EnterPhaseThreeHP)
           // {
           //     phaseTwoActivated = false;
           // }
        //}
        if (EHM.enemyHealth <= Wendigo.EnterPhaseTwoHP && EHM.enemyHealth > Wendigo.EnterPhaseThreeHP)
        {
             BeginPhaseTwo -= Time.deltaTime;
             if (BeginPhaseTwo <= 0)
             {
                 phaseTwoActivated = true;
             }
        }
        else if (EHM.enemyHealth <= Wendigo.EnterPhaseThreeHP)
        {
            phaseTwoActivated = false;
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

    void RoarOfBlood()
    {
        BloodChangeTimer++;
        //if(BloodChangeTimer < 60 && BloodChangeTimer%10 == 0)
        {
            if(BloodRotationAngle == 0)
            {
                //Phase2BloodProj.changeBloodVelocity(5,5);
            }
            else if (BloodRotationAngle == 90)
            {
                //Phase2BloodProj.changeBloodVelocity(-5, -5);
            }
            else if (BloodRotationAngle == 180)
            {
                //Phase2BloodProj.changeBloodVelocity(5, -5);
            }
            else if (BloodRotationAngle == 270)
            {
               // Phase2BloodProj.changeBloodVelocity(5, 5);
            }

            //bloodProjPos.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, BloodRotationAngle)); //Quaternion.AngleAxis(30, Vector3.up);// Quaternion.Euler(0, 0, 180);
            //Instantiate(BloodProj, barrel.position, Quaternion.Euler(0f, 0f, 0f)); //+ BloodRotationAngle));
            //BloodRotationAngle += 90;
            //Debug.Log(BloodRotationAngle);
            for(int i = 0; i < 4; i++)
            {
                //Vector3 dartPosition = new Vector3(transform.position.x, transform.position.y + Random.Range(-5, 5f), transform.position.z + Random.Range(-5f,5f));
                //bloodProjPos.rotation = Quaternion.Euler(transform.eulerAngles.x + Random.Range(-5, 5), transform.eulerAngles.y, transform.eulerAngles.z);
                //projectiles[i] = Instantiate(BloodProj, dartPosition, bloodProjPos.rotation) as GameObject;

                
               // Quaternion dartRotation = Quaternion.Euler(transform.eulerAngles.x + Random.Range(-5, 5), transform.eulerAngles.y +Random.Range(-5, 5), 90);
                //projectiles[i] = Instantiate(BloodProj, dartPosition, dartRotation) as GameObject;
            }
        }
        if(BloodChangeTimer >= 60)
        {
            BloodChangeTimer = 0;
        }
        if(BloodRotationAngle >=360)
        {
            BloodRotationAngle = 0;
        }
    }
    

    void ThrowUpBone()
    {
        if(Wendigo.facingLeft && Wendigo.transform.position.x < -5 || !Wendigo.facingLeft && Wendigo.transform.position.x > 5)
        {
            Wendigo.Flip();
            Wendigo.facingLeft = !Wendigo.facingLeft;
        }

        if (Wendigo.facingLeft && Wendigo.transform.position.x > 0)
        {
            distanceToShoot = -(Wendigo.transform.position.x + 9f);
        }
        else if (Wendigo.facingLeft && Wendigo.transform.position.x <= 0)
        {
            distanceToShoot = Mathf.Abs(Wendigo.transform.position.x) - 9f;
        }
        else if (!Wendigo.facingLeft && Wendigo.transform.position.x > 0)
        {
            distanceToShoot = 9f - Wendigo.transform.position.x;
        }
        else if (!Wendigo.facingLeft && Wendigo.transform.position.x < 0)
        {
            distanceToShoot = Mathf.Abs(Wendigo.transform.position.x) + 9f;
        }

        ////////////////
        ///////////////
        if(Mathf.Abs(distanceToShoot) < 5)
        {
            if (distanceToShoot < 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot + 0.5f));
            }
            else if (distanceToShoot > 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot - 0.5f));
            }
        }
        else if (Mathf.Abs(distanceToShoot) < 9 && Mathf.Abs(distanceToShoot) > 5)
        {
           Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot));
        }
        else if (Mathf.Abs(distanceToShoot) > 9 && Mathf.Abs(distanceToShoot) < 15)
        {
            if(distanceToShoot < 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot + 1.25f));
            }
            else if(distanceToShoot > 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot - 1.25f));
            }
        }
        else if (Mathf.Abs(distanceToShoot) > 15 && Mathf.Abs(distanceToShoot) < 25)
        {
            if (distanceToShoot < 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot + 2.5f));
            }
            else if (distanceToShoot > 0)
            {
                Phase2BoneProj.changeSpeedX(Random.Range(0f, distanceToShoot - 2.5f));
            }
        }
        Instantiate(boneProj, boneProjPos.position, Quaternion.identity);
    }

    void ResetFight()
    {
        phaseTwoCounter = 1.5f;
        BloodChangeTimer = 0;
        BloodRotationAngle = 0;
        isPushingBack = false;
        pushBackTimer = 7f;
        phaseTwoActivated = false;
        BeginPhaseTwo = 3f;
        RainBloodCounter = 2f;
        StartedRaining = false;
        isSpitting = false;
        isRoaring = false;
        Wendigo.isJumping = false;
        hasJumpedTwice = 0;
        hasSpitOnce = false;
        hasWaited = false;
    }

    IEnumerator waitForBone(float time)
    {
        yield return new WaitForSeconds(time);
        hasWaited = true;
        yield return new WaitForSeconds(time);
        hasWaited = false;
    }
}
