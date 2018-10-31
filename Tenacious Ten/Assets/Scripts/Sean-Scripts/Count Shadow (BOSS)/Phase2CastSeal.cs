using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2CastSeal : MonoBehaviour {

    public GameObject SealHorizLeft;
    public GameObject SealHorizRight;
    public GameObject SealVertUp;
    public GameObject SealVertDown;

    Transform HorizProjTop;
    Transform HorizProjBottom;
    Transform VertProjLeft;
    Transform VertProjRight;

    EnemyHealthManager EHM;
    BossManager BM;
    StartOrResetLevel SORL;

    int projRandomizer;
    float projDelayCounter;
    public float projDelay;

    int count3shots;
    int count2shots;

    bool firstCast, BeginCastingAll;

    public int SealCounter;

    // Use this for initialization
    void Start () {
		
        HorizProjTop = transform.Find("Top");
        HorizProjBottom = transform.Find("Bottom");
        VertProjLeft = transform.Find("Left");
        VertProjRight = transform.Find("Right");

        EHM = FindObjectOfType<EnemyHealthManager>();
        BM = FindObjectOfType<BossManager>();
        SORL = FindObjectOfType<StartOrResetLevel>();
        
        BeginCastingAll = false;

        projDelayCounter = projDelay;

        count3shots = 0;
        count2shots = 0;

        firstCast = false;

        SealCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if(!BM.phaseTwoComplete && BM.phaseOneComplete && SORL.StartFight && !BM.start3hit && SealCounter < 2)//if(EHM.enemyHealth < BM.PhaseTwoHP && EHM.enemyHealth > BM.PhaseThreeHP)
        {
            if (BM.PhaseTwoRandomizer == 1 && BeginCastingAll == false && BM.isIdle)
            {
                SealCounter++;
                CastSealsAll();
                BM.isIdle = false;
                BM.anim.SetInteger("State", 6);
            }
        }

        if (BeginCastingAll && SORL.StartFight && !BM.phaseTwoComplete)//BM.PhaseTwoRandomizer == 1 && EHM.enemyHealth < BM.PhaseTwoHP) 
        {
            projDelayCounter -= Time.deltaTime;
            //BM.anim.SetInteger("State", 6);
            if (count3shots < 3 && projDelayCounter <= 0)
            {
                CastSealHorizRandom();
                projDelayCounter = projDelay;
                count3shots++;
            }
            else if (count3shots == 3 && count2shots < 2 && projDelayCounter <= 0)
            {
                CastSealsVert();
                projDelayCounter = projDelay;
                count2shots++;
                BM.anim.SetInteger("State", 0);
            }
            else if(count3shots == 3 && count2shots == 2)
            {
                count3shots = 0;
                count2shots = 0;
                BeginCastingAll = false;
                BM.isIdle = true;
                BM.anim.SetInteger("State", 0);
                if(EHM.enemyHealth <= BM.PhaseThreeHP)
                {
                    BM.phaseTwoComplete = true;
                    BM.anim.SetInteger("State", 0);
                }
                
            }
        }

        if (SealCounter >= 2)
        {
            SealCounter = 0;
            BM.PhaseTwoRandomizer = 0;
        }

        if (SORL.ResetFight)
        {
            BeginCastingAll = false;
            count3shots = 0;
            count2shots = 0;
            SealCounter = 0;
        }

	}

    void CastSealHorizTop()
    {
        Vector3 temp = HorizProjTop.position;

        if (BM.facingLeft == true)
        {
            if (HorizProjTop.position.x < 0)
            {
                temp.x *= -1;
                HorizProjTop.position = temp;
            }
            Instantiate(SealHorizLeft, HorizProjTop.position, Quaternion.identity);
        }
        else if (BM.facingLeft == false)
        {
            if (HorizProjTop.position.x > 0)
            {
                temp.x *= -1;
                HorizProjTop.position = temp;
            }
            Instantiate(SealHorizRight, HorizProjTop.position, Quaternion.identity);
        }
    }

    void CastSealHorizBottom()
    {
        Vector3 temp = HorizProjBottom.position;

        if (BM.facingLeft == true)
        {
            if(HorizProjBottom.position.x < 0)
            {
                temp.x *= -1;
                HorizProjBottom.position = temp;
            }
            Instantiate(SealHorizLeft, HorizProjBottom.position, Quaternion.identity);
        }
        else if (BM.facingLeft == false)
        {
            if (HorizProjBottom.position.x > 0)
            {
                temp.x *= -1;
                HorizProjBottom.position = temp;
            }
            Instantiate(SealHorizRight, HorizProjBottom.position, Quaternion.identity);
        }
    }

    void CastSealHorizRandom()
    {
        projRandomizer = Random.Range(0, 2);
        if(projRandomizer == 0)
        {
            CastSealHorizTop();
        }
        else if(projRandomizer == 1)
        {
            CastSealHorizBottom();
        }
    }

    void CastSealVertLeft()
    {
        Instantiate(SealVertDown, VertProjLeft.position, Quaternion.identity);
    }

    void CastSealVertRight()
    {
        Instantiate(SealVertUp, VertProjRight.position, Quaternion.identity);
    }

    void CastSealsVert()
    {
        if(BM.facingLeft)
        {
            if(!firstCast)
            {
                CastSealVertRight();
                firstCast = true;
            }
            else if(firstCast)
            {
                CastSealVertLeft();
                firstCast = false;
            }
        }
        else if(!BM.facingLeft)
        {
            if (!firstCast)
            {
                CastSealVertLeft();
                firstCast = true;
            }
            else if (firstCast)
            {
                CastSealVertRight();
                firstCast = false;
            }
        }
    }

    void CastSealsAll()
    {
        BeginCastingAll = true;
    }
}
