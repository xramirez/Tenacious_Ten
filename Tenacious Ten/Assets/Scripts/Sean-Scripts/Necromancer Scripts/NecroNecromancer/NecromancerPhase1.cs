using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerPhase1 : MonoBehaviour {

     bool StartFight, StartPhase1;
    [SerializeField] float StartFightTimer;
    [SerializeField] float p1AbilityTimer;
    float initAbilityTimer;

    [SerializeField] GameObject SideLightning;
    [SerializeField] Transform LeftSide;
    [SerializeField] Transform RightSide;
    bool SideLightningsOn;

    [SerializeField] GameObject ShadowGuyLeft;
    [SerializeField] GameObject ShadowGuyRight;
    [SerializeField] float LightningBufferTimer;
    float initBufferTimer;
    [SerializeField] GameObject WarningLightning;
    [SerializeField] GameObject CastedLightning;
    Transform CastedLightningPos;
    bool WarningLightningOut;
    bool PlayerFound;
    float playerLocX;
    float playerLocY;
    [SerializeField] Transform playerLocOnCastRandomInit;
    [SerializeField] Transform CastedLightningPosRandomInit;
    Transform playerLocOnCast;
    PlayerManager player;

    [SerializeField] float SwordTimerL;
    [SerializeField] float SwordTimerR;
    [SerializeField] float SwordTimer;
    [SerializeField] GameObject FanOfSwords;
    [SerializeField] Transform FanSpawnLoc;
    bool SwordTimerSet;

    SpriteRenderer sr;
    float opacityIncrement;

    [SerializeField] float TPTimerL;
    [SerializeField] float TPTimerR;
    [SerializeField] float TPTimer;
    bool TPTimerSet, hasTeleported;

    NecroBossManager BM;
    EnemyHealthManager Health;

    public int Phase1HP;
    public bool DestroyBridge;

    StartOrResetLevel SORL;
    public Transform StartingLoc;
    Animator anim;
    [SerializeField] GameObject NecroLightning;
    bool NecroLightningOut;

    BoxCollider2D BC;

    // Use this for initialization
    void Start () {
        StartFight = false;
        StartPhase1 = false;
        SideLightningsOn = false;
        PlayerFound = false;
        WarningLightningOut = false;
        TPTimerSet = false;
        hasTeleported = false;
        DestroyBridge = false;
        NecroLightningOut = false;
        player = FindObjectOfType<PlayerManager>();
        
        //StartingLoc.position = transform.position;

        CastedLightningPos = CastedLightningPosRandomInit;
        playerLocOnCast = playerLocOnCastRandomInit;

        CastedLightningPos.position = new Vector3(0f, 15.57f, 0f);

        SwordTimerSet = false;

        BC = GetComponent<BoxCollider2D>();

        initBufferTimer = LightningBufferTimer;
        initAbilityTimer = p1AbilityTimer;

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        opacityIncrement = 0f;

        Health = GetComponent<EnemyHealthManager>();
        BM = FindObjectOfType<NecroBossManager>();

        if((GameObject.Find("Start trigger")) != null)
        {
            SORL = GameObject.Find("Start trigger").GetComponent<StartOrResetLevel>();
        }
        else
        {
            Debug.Log("Cannot find start trigger...");
        }
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(BM != null)
        {
            if (!StartFight && !StartPhase1 && BM.Phase1Activated)
            {
                opacityIncrement = opacityIncrement + 0.06f;
                sr.color = new Color(1f, 1f, 1f, opacityIncrement);
                if (opacityIncrement >= 1.3f)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, 0f);
                    if (transform.position.y <= 10.82)
                    {
                        opacityIncrement = 0f;
                        StartFight = true;
                        StartPhase1 = true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("BM equals NULL");
        }
        

        if(StartFight)
        {
            StartFightTimer -= Time.deltaTime;
            if(StartFightTimer <= 0)
            {
                if(!SideLightningsOn) //Turn on lightning on the sides of room
                {
                    Instantiate(SideLightning, LeftSide.position, Quaternion.identity);
                    Instantiate(SideLightning, RightSide.position, Quaternion.identity);
                    SideLightningsOn = true;
                }

                if(SideLightningsOn)
                {
                    p1AbilityTimer -= Time.deltaTime;
                    if(p1AbilityTimer <= 1.3f)
                    {
                        anim.SetInteger("State", 1);
                    }
                    if(p1AbilityTimer <= 0.4f)
                    {
                        if (!NecroLightningOut)
                        {
                            NecroLightningOut = true;
                            Instantiate(NecroLightning, transform.GetChild(0).position, Quaternion.identity);
                        }
                    }
                    if(p1AbilityTimer <= 0)
                    {
                        CastLightning();
                    }

                    if (!SwordTimerSet)
                    {
                        SwordTimer = Random.Range(SwordTimerL, SwordTimerR);
                        SwordTimerSet = true;
                    }
                    else if(SwordTimerSet)
                    {
                        SwordTimer -= Time.deltaTime;
                        if(SwordTimer <= 0)
                        {
                            FindRandomLocForFan();
                            SwordTimerSet = false;
                            Instantiate(FanOfSwords, FanSpawnLoc.position, Quaternion.identity);
                        }
                    }

                    if(false)if(!TPTimerSet)
                    {
                        TPTimer = Random.Range(TPTimerL, TPTimerR);
                        TPTimerSet = true;
                    }
                    else if(TPTimerSet)
                    {
                        TPTimer -= Time.deltaTime;
                        if(TPTimer <= 0)
                        {
                            Teleport();
                        }
                    }


                }



                
            }

        }//end if startFight

        if(Health != null)
        {
            if (Health.enemyHealth <= Phase1HP)
            {
                BC.enabled = false;
                StartFight = false;
                if(StartingLoc.position.y >= transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, 0f);
                }
                sr.color = new Color(1f, 1f, 1f, sr.color.a - 0.02f);
                if (sr.color.a <= -0.4)
                {
                    DestroyBridge = true;
                }
            }
        }
        

    }//end FixedUpdate

    void Update()
    {
        if(SORL != null)//if (SORL.ResetFight)
        {
            if (SORL.ResetFight)
            {
                BC.enabled = true;
                anim.SetInteger("State", 0);
                StartFight = false;
                StartPhase1 = false;
                SideLightningsOn = false;
                PlayerFound = false;
                WarningLightningOut = false;
                TPTimerSet = false;
                hasTeleported = false;
                DestroyBridge = false;
                NecroLightningOut = false;
                SwordTimerSet = false;
                LightningBufferTimer = initBufferTimer;
                Debug.Log(StartingLoc.position);
                p1AbilityTimer = initAbilityTimer;
                transform.position = StartingLoc.position;
                sr.color = new Color(1f, 1f, 1f, 0f);
                opacityIncrement = 0f;
                Health.enemyHealth = Health.maxHealth;
            }
            
        }

    }

    void CastLightning()
    {
        if(!PlayerFound)
        {
            playerLocX = player.transform.position.x;
            playerLocY = 10.87f;
            playerLocOnCast.position = new Vector3(playerLocX, playerLocY, 0f);
            CastedLightningPos.position = new Vector3(playerLocX, CastedLightningPos.position.y, 0f);
            PlayerFound = true;
        }
        else if(PlayerFound)
        {
            if(!WarningLightningOut)
            {
                Instantiate(WarningLightning, CastedLightningPos.position, Quaternion.identity);
                WarningLightningOut = true;
            }

            if(WarningLightningOut)
            {
                LightningBufferTimer -= Time.deltaTime;
                if(LightningBufferTimer <= 0.25f)
                {
                    anim.SetInteger("State", 0);
                }
                if(LightningBufferTimer <= 0)
                {
                    LightningBufferTimer = initBufferTimer;
                    Instantiate(CastedLightning, CastedLightningPos.position, Quaternion.identity);
                    Instantiate(ShadowGuyLeft, playerLocOnCast.position, Quaternion.identity);
                    Instantiate(ShadowGuyRight, playerLocOnCast.position, Quaternion.identity);

                    WarningLightningOut = false;
                    PlayerFound = false;
                    p1AbilityTimer = initAbilityTimer;

                    NecroLightningOut = false;
                }
            }
        }
    }//end void castLightning

    void FindRandomLocForFan()
    {
        float rangeX = 0;
        if(player.transform.position.x < 0)
        {
            rangeX = Random.Range(-2.8f, 0f);
        }
        else
        {
            rangeX = Random.Range(0f, 3.8f);
        }

        FanSpawnLoc.position = new Vector3(rangeX, FanSpawnLoc.position.y, 0f);
    }

    void Teleport()
    {
        if(!hasTeleported)
        {
            opacityIncrement = opacityIncrement + 0.03f;
            sr.color = new Color(1f, 1f, 1f, 1f - opacityIncrement);
        }
        if(opacityIncrement >= 1.1 && !hasTeleported)
        {
            opacityIncrement = 0;
            hasTeleported = true;
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0f);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 0f);
        }
        else if(hasTeleported)
        {
            opacityIncrement = opacityIncrement + 0.03f;
            sr.color = new Color(1f, 1f, 1f, opacityIncrement);
            if (opacityIncrement >= 1)
            {
                opacityIncrement = 0f;
                hasTeleported = false;
                TPTimerSet = false;
            }
        }
    }


}
