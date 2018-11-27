using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetBossManager : MonoBehaviour {

    //[SerializeField] 
    Phase1DollHealthManager LeftDoll;
    //[SerializeField] 
    Phase1DollHealthManager RightDoll;
    //[SerializeField] 
    Phase2HandsHealthManager LeftHand;
    //[SerializeField] 
    Phase2HandsHealthManager RightHand;
    EnemyHealthManager EHM;
    [SerializeField] int Phase4HP;
    float beginPhaseOne;
    public bool phaseOneActivated, phaseTwoActivated, phaseThreeActivated, phaseFourActivated;

    bool movedUpPhase3, movedDownPhase3;
    public bool hasMovedForPhase3;
    float beginPhaseThreeTimer;

    float beginPhaseFourTimer;
    public bool hasMovedForPhase4;
    bool movedDownPhase4, movedLeftPhase4;

    Transform StartingLoc;

    StartOrResetLevel SORL;
    SpriteRenderer sr;
    HurtPlayerOnContact HurtPlayer;

	// Use this for initialization
	void Start () {

        StartingLoc.position = transform.localPosition;

        phaseOneActivated = false;
        phaseTwoActivated = false;
        phaseThreeActivated = false;

        hasMovedForPhase3 = false;
        beginPhaseThreeTimer = 3f;
        movedUpPhase3 = false;
        movedDownPhase3 = false;

        hasMovedForPhase4 = false;
        movedDownPhase4 = false;
        movedLeftPhase4 = false;

        EHM = GetComponent<EnemyHealthManager>();

        SORL = FindObjectOfType<StartOrResetLevel>();

        sr = GetComponent<SpriteRenderer>();

        HurtPlayer = GetComponent<HurtPlayerOnContact>();
    }
	
	void FixedUpdate () {

        SORL = FindObjectOfType<StartOrResetLevel>();
        EHM = GetComponent<EnemyHealthManager>();
        sr = GetComponent<SpriteRenderer>();
        //Debug.Log(EHM.enemyHealth);
        if (SORL.StartFight)
        {
            phaseOneActivated = true;

            if(phaseOneActivated && !phaseThreeActivated && transform.position.y >= 2.385)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z);
            }
            

            if(phaseOneActivated && !phaseTwoActivated)
            {
                if (GameObject.Find("Left Doll") != null || GameObject.Find("Right Doll") != null)
                {
                    LeftDoll = GameObject.Find("Left Doll").GetComponent<Phase1DollHealthManager>();
                    RightDoll = GameObject.Find("Right Doll").GetComponent<Phase1DollHealthManager>();
                }
                else
                {
                    //Debug.Log("PHASE 1 NOT WORKING PHASE 1 NOT WORKING");
                }
            }
        }

        //Debug.Log(LeftDoll.LeftDollKilled);
        //Debug.Log(RightDoll.RightDollKilled);
        //Debug.Log(GameObject.Find("Left Doll") != null);
        if (LeftDoll.LeftDollKilled && RightDoll.RightDollKilled) //&& (GameObject.Find("Left Doll") != null || GameObject.Find("Right Doll") != null))
        {
            phaseOneActivated = false;
            phaseTwoActivated = true;
            if(phaseTwoActivated && !phaseThreeActivated)
            {
                if (GameObject.Find("Phase 2 Hand Left(Clone)") != null || GameObject.Find("Phase 2 Hand Right(Clone)") != null)
                {
                    LeftHand = GameObject.Find("Phase 2 Hand Left(Clone)").GetComponent<Phase2HandsHealthManager>();
                    RightHand = GameObject.Find("Phase 2 Hand Right(Clone)").GetComponent<Phase2HandsHealthManager>();                }
                else
                {
                    //Debug.Log("LOL NOT WORKING");
                }
            }
            //Debug.Log("Phase 1 over. Now entering phase 2...");
        }

        //Debug.Log(LeftHand.LeftHandKilled);
        //Debug.Log(RightHand.RightHandKilled);
        //Debug.Log(phaseThreeActivated);
        if (LeftHand.LeftHandKilled && RightHand.RightHandKilled)
        {
            phaseTwoActivated = false;
            phaseThreeActivated = true;
            //Debug.Log("Phase 2 over. Now entering phase 3...");
        }

        if (phaseThreeActivated)
        {
            beginPhaseThreeTimer -= Time.deltaTime;
        }
        if(phaseThreeActivated && !hasMovedForPhase3 && beginPhaseThreeTimer <= 0)
        {
            if(!movedUpPhase3)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                if (transform.position.y >= 8)
                {
                    movedUpPhase3 = true;
                }
            }
            else if(!movedDownPhase3)
            {
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
                if(transform.position.x >= 7.7)
                {
                    movedDownPhase3 = true;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.08f, transform.position.z);
                if(transform.position.y <= -1.5)
                {
                    hasMovedForPhase3 = true;
                }
            }
        } //end if moving to start phase3

        if(EHM.enemyHealth <= Phase4HP)
        {
            phaseThreeActivated = false;
            phaseFourActivated = true;
            HurtPlayer.enabled = false;
            Debug.Log("Phase 3 over. Now entering phase 4...");
        }

        if (phaseFourActivated)
        {
            beginPhaseFourTimer -= Time.deltaTime;
        }
        if (phaseFourActivated && !hasMovedForPhase4 && beginPhaseFourTimer <= 0)
        {
            if (!movedDownPhase4)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                if (transform.position.y <= -9)
                {
                    movedDownPhase4 = true;
                    if ((sr = GetComponent<SpriteRenderer>()) != null)
                    {
                        sr.color = new Color(1f, 1f, 1f, sr.color.a - 0.4f);
                    }
                    else
                    {
                        Debug.Log("Puppet Boss Manager SR not working");
                    }
                }
            }
            else if (!movedLeftPhase4)
            {
                transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y, transform.position.z);
                if (transform.position.x <= 0)
                {
                    movedLeftPhase4 = true;
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.06f, transform.position.z);
                if (transform.position.y >= -1.5)
                {
                    hasMovedForPhase4 = true;
                }
            }
        }

    }//end FixedUpdate()

    void Update()
    {
        if(SORL.ResetFight)
        {

            Destroy(gameObject);
            phaseOneActivated = false;
            phaseTwoActivated = false;
            phaseThreeActivated = false;
            phaseFourActivated = false;

            hasMovedForPhase3 = false;
            beginPhaseThreeTimer = 3f;
            movedUpPhase3 = false;
            movedDownPhase3 = false;

            hasMovedForPhase4 = false;
            movedDownPhase4 = false;
            movedLeftPhase4 = false;

            sr.color = new Color(1f, 1f, 1f, 1f);

            //LeftDoll = GameObject.Find("Left Doll").GetComponent<Phase1DollHealthManager>();
            //RightDoll = GameObject.Find("Right Doll").GetComponent<Phase1DollHealthManager>();
            //LeftHand = GameObject.Find("Phase 2 Hand Left(Clone)").GetComponent<Phase2HandsHealthManager>();
            //RightHand = GameObject.Find("Phase 2 Hand Right(Clone)").GetComponent<Phase2HandsHealthManager>();

            //LeftDoll.LeftDollKilled = false;
            //RightDoll.RightDollKilled = false;
            //LeftHand.LeftHandKilled = false;
            //RightHand.RightHandKilled = false;

            transform.position = StartingLoc.position;
        }
        if(!SORL.StartFight)
        {

            transform.position = StartingLoc.position;
            LeftDoll.LeftDollKilled = false;
            RightDoll.RightDollKilled = false;
            LeftHand.LeftHandKilled = false;
            RightHand.RightHandKilled = false;

            phaseOneActivated = false;
            phaseTwoActivated = false;
            phaseThreeActivated = false;
            phaseFourActivated = false;
        }
    }

    IEnumerator WaitForNextPhase(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
