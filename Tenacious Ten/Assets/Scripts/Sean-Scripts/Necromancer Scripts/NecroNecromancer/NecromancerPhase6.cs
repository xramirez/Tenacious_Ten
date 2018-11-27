using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerPhase6 : MonoBehaviour {

    [SerializeField] GameObject CastedLightning;
    [SerializeField] GameObject RotationalLightning;
    [SerializeField] GameObject TrackingSwordEmitter;
    [SerializeField] GameObject SweepAttackTL;
    [SerializeField] GameObject SweepAttackTR;
    [SerializeField] GameObject SweepAttackML;
    [SerializeField] GameObject SweepAttackMR;
    [SerializeField] GameObject SweepAttackBL;
    [SerializeField] GameObject SweepAttackBR;

    bool RotateLightningCasted, HasSweepAttacked, HasCastedSwords;
    bool readyToCastLightning, readyToCastSwords;

    [SerializeField] float AbilityTimer;
    [SerializeField] int numOfSwordsToCast;
    [SerializeField] float swordCastTimer;
    float initSwordCastTimer;
    int numSwordsCasted;
    float initAbilityTimer;

    bool NecroSummoned;

    int abilityChoice;
    bool abilityChosen;

    bool hasTeleported;
    int sweepLocation, oldSweepLocation, sweepCounter;
    bool sweepLocChosen, isFaded, FullOpacity, SweepCreated;

    Animator anim;
    SpriteRenderer sr;
    BoxCollider2D BC;

    StartOrResetLevel SORL;

    [SerializeField] AudioSource ThunderSound;
    [SerializeField] AudioSource SweepAttackSound;

    // Use this for initialization
    void Start () {

        RotateLightningCasted = false;
        HasSweepAttacked = false;
        HasCastedSwords = false;
        abilityChosen = false;
        hasTeleported = false;
        sweepLocChosen = false;
        isFaded = false;
        FullOpacity = false;
        NecroSummoned = false;
        readyToCastSwords = false;
        readyToCastLightning = false;
        SweepCreated = false;

        initSwordCastTimer = swordCastTimer;
        initAbilityTimer = AbilityTimer;

        oldSweepLocation = 0;
        numSwordsCasted = 0;
        sweepCounter = 0;


        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        BC = GetComponent<BoxCollider2D>();
        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(sr.color.a <= 1.6f && !NecroSummoned)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.02f);
        }
        else if(sr.color.a > 1.6f)
        {
            NecroSummoned = true;
        }

        if(NecroSummoned)
        {
            if(!abilityChosen)
            {
                abilityChosen = true;
                abilityChoice = Random.Range(0, 3);
                if (RotateLightningCasted && HasSweepAttacked && HasCastedSwords)
                {
                    RotateLightningCasted = false;
                    HasSweepAttacked = false;
                    HasCastedSwords = false;
                }
            }
            else if(abilityChosen)
            {
                AbilityTimer -= Time.deltaTime;
                if(AbilityTimer <= 0)
                {

                    if (RotateLightningCasted && HasSweepAttacked && HasCastedSwords)
                    {
                        abilityChosen = false;
                    }
                    else if (abilityChoice == 0 && RotateLightningCasted)
                    {
                        abilityChoice = Random.Range(1, 3);
                    }
                    else if (abilityChoice == 1 && HasSweepAttacked)
                    {
                        while (abilityChoice == 1)
                        {
                            abilityChoice = Random.Range(0, 3);
                        }
                    }
                    else if (abilityChoice == 2 && HasCastedSwords)
                    {
                        abilityChoice = Random.Range(0, 2);
                    }
                    else if (abilityChoice == 0) //rotate lightning
                    {
                        CastRotationLightning();
                        Debug.Log("Only rotate lightning should be out...");
                    }
                    else if (abilityChoice == 1) //sweep attack
                    {
                        if (sweepCounter < 3)
                        {
                            CastArgusSweep();
                        }
                        else if(sweepCounter >= 3)
                        {
                            abilityChosen = true;
                            AbilityTimer = initAbilityTimer;
                            sweepCounter = 0;
                            HasSweepAttacked = true;
                        }
                    }
                    else if (abilityChoice == 2)  //tracking swords
                    {
                        Debug.Log("Only swords should be out...");
                        anim.SetInteger("State", 1);
                        CastTrackingSwords();
                    }
                }
                
            }
            
        }
        

	}//end fixedUpdate

    void Update()
    {
        if(SORL.ResetFight)
        {
            Destroy(gameObject);
        }
    }

    void CastRotationLightning()
    {
        if(sr.color.a >= 0)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.02f);
        }
        transform.position = new Vector3(0f, -3.58f, 0f);
        if(!readyToCastLightning && sr.color.a >= 1f)
        {
            //BC.enabled = false;
            anim.SetInteger("State", 1);
            readyToCastLightning = true;
            //Instantiate(RotationalLightning, transform.GetChild(1).position, Quaternion.identity);
            StartCoroutine(waitSpawnLightning(1.5f));
            StartCoroutine(waitForHurtbox(2f));
            StartCoroutine(waitForLightning(12f));
        }
    }

    void CastArgusSweep()
    {
        //if(!hasTeleported)
        //{
        //    hasTeleported = true;
        //}

        PickLocationForSweep();
        if(!isFaded)
        {
            FadeAway();
            Debug.Log("Fading...");
        }
        else if(isFaded && !FullOpacity)
        {
            if(sweepLocation == 0)  //BR
            {
                transform.position = new Vector3(8.67f, -4.09f, 0f);
            }
            else if (sweepLocation == 1) //MR
            {
                transform.position = new Vector3(8.67f, 0f, 0f);
            }
            else if (sweepLocation == 2) //TR
            {
                transform.position = new Vector3(8.67f, 1.69f, 0f);
            }
            else if (sweepLocation == 3)
            {
                transform.position = new Vector3(-8.67f, 1.69f, 0f);
            }
            else if (sweepLocation == 4)
            {
                transform.position = new Vector3(-8.67f, 0f, 0f);
            }
            else if (sweepLocation == 5)
            {
                transform.position = new Vector3(-8.67f, -4.09f, 0f);
            }

            if(sweepLocation == 1 || sweepLocation == 2 || sweepLocation == 0)
            {
                if(transform.localScale.x < 0f)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (sweepLocation == 3 || sweepLocation == 4 || sweepLocation == 5)
            {
                if (transform.localScale.x > 0f)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
                FadeBack();
            Debug.Log("Should've TPed and is now fading back to full opacity...");
        }
        else if(FullOpacity && !SweepCreated)
        {
            Debug.Log("Created sweep...");
            anim.SetInteger("State", 4);
            if (sweepLocation == 0)
            {
                Instantiate(SweepAttackBR, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (sweepLocation == 1)
            {
                Instantiate(SweepAttackMR, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (sweepLocation == 2)
            {
                Instantiate(SweepAttackTR, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (sweepLocation == 3)
            {
                Instantiate(SweepAttackTL, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (sweepLocation == 4)
            {
                Instantiate(SweepAttackML, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (sweepLocation == 5)
            {
                Instantiate(SweepAttackBL, transform.GetChild(0).position, Quaternion.identity);
            }
            SweepCreated = true;
            //HasSweepAttacked = true;
            StartCoroutine(waitSwingAnim(1.2f));
        }
    }

    void FadeAway()
    {
        if(sr.color.a >= 0)
        {
            Debug.Log("Should be fading...");
            sr.color = new Color(1f, 1f, 1f, sr.color.a - 0.2f);    //changed to 0.2f from 0.02 because sr.color.a was 10???
            Debug.Log(sr.color.a);
        }
        else if(sr.color.a < 0)
        {
            isFaded = true;
        }
    }

    void FadeBack()
    {
        if (sr.color.a <= 1f)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.02f);
            FullOpacity = false;
        }
        else if (sr.color.a > 1f)
        {
            FullOpacity = true;
        }
    }

    void PickLocationForSweep()
    {
        if(!sweepLocChosen)
        {
            sweepLocChosen = true;
            //sweepLocation = Random.Range(0, 6);
            while(oldSweepLocation == sweepLocation)
            {
                sweepLocation = Random.Range(0, 6);
            }
            oldSweepLocation = sweepLocation;
        }
        
    }

    void CastTrackingSwords()
    {
        //swordCastTimer -= Time.deltaTime;
        //if(numOfSwordsToCast > numSwordsCasted && swordCastTimer <= 0)
        //{
        //    swordCastTimer = initSwordCastTimer;
        //    numSwordsCasted++;
        //    Instantiate(TrackingSword, transform.position, Quaternion.identity);
        //}
        transform.position = new Vector3(0f, -3.58f, 0f);
        if (!readyToCastSwords)
        {
            //BC.enabled = false;
            Instantiate(TrackingSwordEmitter, transform.position, Quaternion.identity);
            readyToCastSwords = true;
            StartCoroutine(waitForHurtbox(2f));
            StartCoroutine(waitForTrackingSwords(10f));
        }
        
    }

    IEnumerator waitSwingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        SweepAttackSound.PlayDelayed(0.5f);
        anim.SetInteger("State", 5);
        StartCoroutine(waitTP(1.5f));
    }

    IEnumerator waitTP(float time)
    {
        yield return new WaitForSeconds(time);
        SweepCreated = false;
        FullOpacity = false;
        sweepLocChosen = false;
        isFaded = false;
        sweepCounter++;
        Debug.Log("SWEEP COUNTER EQUALS: ");
        Debug.Log(sweepCounter);
    }


    IEnumerator waitForTrackingSwords(float time)
    {
        yield return new WaitForSeconds(time);
        readyToCastSwords = false;
        anim.SetInteger("State", 0);
        abilityChosen = false;
        AbilityTimer = initAbilityTimer;
        HasCastedSwords = true;
    }

    IEnumerator waitForHurtbox(float time)
    {
        yield return new WaitForSeconds(time);
        //BC.enabled = true;
    }

    IEnumerator waitSpawnLightning(float time)
    {
        yield return new WaitForSeconds(time);
        ThunderSound.Play();
        Instantiate(RotationalLightning, transform.GetChild(1).position, Quaternion.identity);
    }

    IEnumerator waitForLightning(float time)
    {
        yield return new WaitForSeconds(time);
        readyToCastLightning = false;
        anim.SetInteger("State", 0);
        abilityChosen = false;
        AbilityTimer = initAbilityTimer;
        RotateLightningCasted = true;
    }
}
