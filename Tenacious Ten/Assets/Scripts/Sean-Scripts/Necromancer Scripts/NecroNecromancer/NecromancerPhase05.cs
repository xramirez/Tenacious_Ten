using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerPhase05 : MonoBehaviour {

    SpriteRenderer sr;
    [SerializeField] GameObject Fairy;
    [SerializeField] GameObject Damien;
    [SerializeField] GameObject Samurai;
    [SerializeField] GameObject Lightning;
    [SerializeField] GameObject NecroLightning;

    [SerializeField] Transform LightningSamurai;
    [SerializeField] Transform LightningFairy;
    [SerializeField] Transform LightningDamien;

    [SerializeField] Transform FairySpawn;
    [SerializeField] Transform DamienSpawn;
    [SerializeField] Transform SamuraiSpawn;

    [SerializeField] float ReviveTimer;
    float timer;
    bool LightningCasted, NecroLightningCasted, SamSummoned, FairySummoned, DamienSummoned;
    int summonCounter;

    public bool StartPhase5;

    StartOrResetLevel SORL;
    Animator anim;

    // Use this for initialization
    void Start () {

        sr = GetComponent<SpriteRenderer>();

        sr.color = new Color(1f, 1f, 1f, 0f);
        timer = ReviveTimer;
        LightningCasted = false;
        summonCounter = 0;

        SamSummoned = false;
        FairySummoned = false;
        DamienSummoned = false;
        StartPhase5 = false;
        NecroLightningCasted = false;

        SORL = FindObjectOfType<StartOrResetLevel>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(sr.color.a < 2f && (!SamSummoned || !FairySummoned || !DamienSummoned))
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.02f);
        }

        if (SamSummoned && FairySummoned && DamienSummoned)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a - 0.02f);
        }

        if (sr.color.a >= 2f && (!SamSummoned || !FairySummoned || !DamienSummoned))
        {

            if(!SamSummoned || !FairySummoned || !DamienSummoned)
            {
                ReviveTimer -= Time.deltaTime;
                StartPhase5 = true;
            }


            if(ReviveTimer <= 1.2f)
            {
                anim.SetInteger("State", 1);
            }
            if(ReviveTimer <= 0.3f)
            {
                if (!NecroLightningCasted)
                {
                    NecroLightningCasted = true;
                    Instantiate(NecroLightning, transform.GetChild(0).position, Quaternion.identity);
                }
            }
            if(ReviveTimer <= -0.4f)
            {
                if(!LightningCasted)
                {
                    if(summonCounter == 0)
                    {
                        Instantiate(Lightning, LightningSamurai.position, Quaternion.identity);
                        LightningCasted = true;
                        summonCounter++;
                    }
                    else if (summonCounter == 1)
                    {
                        Instantiate(Lightning, LightningFairy.position, Quaternion.identity);
                        LightningCasted = true;
                        summonCounter++;
                    }
                    else if (summonCounter == 2)
                    {
                        Instantiate(Lightning, LightningDamien.position, Quaternion.identity);
                        LightningCasted = true;
                        summonCounter++;
                    }
                }
                if (ReviveTimer <= -0.65f && !SamSummoned)
                {
                    LightningCasted = false;
                    NecroLightningCasted = false;
                    SamSummoned = true;
                    ReviveTimer = timer;
                    Instantiate(Samurai, SamuraiSpawn.position, Quaternion.identity);
                }
                else if (ReviveTimer <= -0.65f && !FairySummoned)
                {
                    LightningCasted = false;
                    NecroLightningCasted = false;
                    FairySummoned = true;
                    ReviveTimer = timer;
                    Instantiate(Fairy, FairySpawn.position, Quaternion.identity);
                }
                else if (ReviveTimer <= -0.65f && !DamienSummoned)
                {
                    LightningCasted = false;
                    NecroLightningCasted = false;
                    DamienSummoned = true;
                    ReviveTimer = timer;
                    Instantiate(Damien, DamienSpawn.position, Quaternion.identity);
                }
            }
        }
    }

    void Update()   //idk if i need this...
    {
        if(SORL.ResetFight)
        {
            sr.color = new Color(1f, 1f, 1f, 0f);
            timer = ReviveTimer;
            LightningCasted = false;
            summonCounter = 0;
            SamSummoned = false;
            FairySummoned = false;
            DamienSummoned = false;
            Destroy(gameObject);
        }

    }
}
