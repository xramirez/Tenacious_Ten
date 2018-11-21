using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2HandsNew : MonoBehaviour
{

    public GameObject EdgeDoll;
    public Transform SpawnEdgeDollLocation;
    [SerializeField] bool isLeftHand, isRightHand;
    [SerializeField] float SpawnDollsTimer;
    [SerializeField] float hoverBackSpeed, handStrikeMoveSpeed, fadeAwayValue;
    //[SerializeField]
    PuppetBossManager BossManager;
    private float StartSpawnTimer, startingFadeValue;
    GameObject player;
    bool playerFound, movingLeft, hitGround, playerStruck, hoveredBack;
    bool readyToHoverBack, waitedToHover;
    float playerLocation, playerLocationY, distanceFromPlayer, distanceFromPlayerY, travelTime, travelTimeY;

    bool movingToSpawnDolls, SpawnedDolls, backFromSpawning, OGspotFound, pausedBeforeSecondSpawn, firstDollSpawned;

    public bool HandsAreIdle, DollsAreOut;
    [SerializeField] float LeftStrikeTimer, RightStrikeTimer;
    private float startLeftStrikeTimer, startRightStrikeTimer;
    bool LeftHandToStrike, RightHandToStrike;
    bool waitForRightStrike;

    Vector3 HoverBackLoc;
    SpriteRenderer sr;
    Vector3 StartingLoc;
    bool beginPhaseRelocate;

    Animator anim;

    Phase2HandsHealthManager LeftHand;
    P2HandsNew RightHand;

    [SerializeField] int numStrikesBeforeDollsOut;
    int currentStrikeCountLeft;
    int currentStrikeCountRight;

    // Use this for initialization
    void Start()
    {
        StartingLoc = transform.localPosition;
        StartSpawnTimer = SpawnDollsTimer;
        startingFadeValue = fadeAwayValue;
        player = GameObject.FindGameObjectWithTag("Player");
        playerFound = false;
        playerStruck = false;
        hoveredBack = false;
        readyToHoverBack = false;
        waitedToHover = false;

        movingToSpawnDolls = false;
        SpawnedDolls = false;
        backFromSpawning = false;
        OGspotFound = false;
        pausedBeforeSecondSpawn = false;
        firstDollSpawned = false;

        currentStrikeCountLeft = 0;
        currentStrikeCountRight = 0;

        HandsAreIdle = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        startLeftStrikeTimer = LeftStrikeTimer;
        startRightStrikeTimer = RightStrikeTimer; //was divided by 2? idk

        LeftHandToStrike = true;
        RightHandToStrike = false;

        beginPhaseRelocate = false;
        DollsAreOut = false;
        waitForRightStrike = false;

        SpawnEdgeDollLocation = transform;
        BossManager = FindObjectOfType<PuppetBossManager>();

        anim = GetComponent<Animator>();

        if (GameObject.Find("Phase 2 Hand Left(Clone)") != null)
        {
            LeftHand = GameObject.Find("Phase 2 Hand Left(Clone)").GetComponent<Phase2HandsHealthManager>();
        }

        if (GameObject.Find("Phase 2 Hand Right(Clone)") != null)
        {
            RightHand = GameObject.Find("Phase 2 Hand Right(Clone)").GetComponent<P2HandsNew>();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sr.color.a <= 1f)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.01f);
        }
        

        if (BossManager.phaseTwoActivated)
        {
            if (GameObject.Find("Phase 2 Hand Right(Clone)") != null)
            {
                if (isLeftHand && !RightHand.DollsAreOut)
                {
                    DollsAreOut = false;
                    LeftStrikeTimer = startLeftStrikeTimer;
                }
            }
                
            if (!DollsAreOut)
            {
                SpawnDollsTimer -= Time.deltaTime;
            }

            if(!DollsAreOut)//if (SpawnDollsTimer <= 0 && !DollsAreOut)
            {
                if (!OGspotFound)
                {
                    SetHoverBackLoc();
                    OGspotFound = true;
                }
                else
                {
                    SpawnEdgeDolls();
                    if (backFromSpawning)
                    {
                        movingToSpawnDolls = false;
                        SpawnedDolls = false;
                        firstDollSpawned = false;
                        pausedBeforeSecondSpawn = false;
                        backFromSpawning = false;
                        OGspotFound = false;
                        DollsAreOut = true;
                        //SpawnDollsTimer = StartSpawnTimer;
                        if (isLeftHand)
                        {
                            SpawnDollsTimer = startLeftStrikeTimer + 2f;
                        }
                        else if (isRightHand)
                        {
                            SpawnDollsTimer = 2f;
                        }
                    }
                }
            }
            if (isLeftHand && DollsAreOut)
            {
                LeftStrikeTimer -= Time.deltaTime;
            }

            if (isRightHand && DollsAreOut)
            {
                RightStrikeTimer -= Time.deltaTime;
            }

            if(LeftStrikeTimer <= 1f && isLeftHand)
            {
                anim.SetInteger("State", 1);
            }

            if (RightStrikeTimer <= 1f && isRightHand)
            {
                anim.SetInteger("State", 1);
            }

            if (LeftStrikeTimer <= 0 || RightStrikeTimer <= 0)
            {
                StrikePlayer();
                if (playerStruck && !readyToHoverBack)
                {
                    if (!waitedToHover)
                    {
                        waitedToHover = true;
                        StartCoroutine(WaitBeforeHoverBack(3f));
                    }
                }
                if (readyToHoverBack)
                {
                    HoverBack();
                    if (hoveredBack)
                    {
                        waitedToHover = false;
                        readyToHoverBack = false;

                        playerFound = false;
                        playerStruck = false;
                        hoveredBack = false;
                        movingLeft = false;

                        //LeftHandToStrike = !LeftHandToStrike;
                        //RightHandToStrike = !RightHandToStrike;

                        fadeAwayValue = startingFadeValue;
                        
                        if(LeftHand.LeftHandKilled)
                        {
                            RightStrikeTimer = startRightStrikeTimer/2;
                        }
                        else
                        {
                            RightStrikeTimer = startRightStrikeTimer;
                        }
                        if(isLeftHand)
                        {
                            currentStrikeCountLeft++;
                        }
                        else if(isRightHand)
                        {
                            currentStrikeCountRight++;
                        }
                        

                        if (isLeftHand && currentStrikeCountLeft < numStrikesBeforeDollsOut)
                        {
                            LeftStrikeTimer = startLeftStrikeTimer;
                        }
                        else if(isLeftHand && currentStrikeCountLeft >= numStrikesBeforeDollsOut)
                        {
                            currentStrikeCountLeft = 0;
                            LeftStrikeTimer = startLeftStrikeTimer * 2;
                            if (GameObject.Find("Phase 2 Hand Right(Clone)") == null)
                            {
                                DollsAreOut = false;
                            }
                        }

                        if (isRightHand && currentStrikeCountRight >= numStrikesBeforeDollsOut)
                        {
                            currentStrikeCountRight = 0;
                            DollsAreOut = false;
                        }
                    }
                }

            }



        }

    }

    void SpawnEdgeDolls()
    {
        if (!movingToSpawnDolls)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            if (transform.position.y <= 0)
            {
                movingToSpawnDolls = true;
            }
        }
        else if (movingToSpawnDolls && !SpawnedDolls)
        {
            if (!firstDollSpawned)
            {
                firstDollSpawned = true;
                Instantiate(EdgeDoll, SpawnEdgeDollLocation.position, Quaternion.identity);
                StartCoroutine(PauseBeforeSecondDoll(1.2f));
            }
            if (pausedBeforeSecondSpawn)
            {
                Instantiate(EdgeDoll, SpawnEdgeDollLocation.position, Quaternion.identity);
                SpawnedDolls = true;
            }
        }
        else if (SpawnedDolls)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z);

            if (transform.position.y >= HoverBackLoc.y)
            {
                backFromSpawning = true;
            }
        }
    }

    void SetHoverBackLoc()
    {
        HoverBackLoc = transform.position;
    }

    void LocatePlayer()
    {
        HoverBackLoc = transform.position;
        playerLocation = player.transform.position.x;
        playerLocationY = player.transform.position.y;
        playerFound = true;
        distanceFromPlayer = Mathf.Abs(transform.position.x - playerLocation);
        distanceFromPlayerY = Mathf.Abs(transform.position.y + 4.1f);
        travelTime = distanceFromPlayer / 50;
        travelTimeY = distanceFromPlayerY / 40;
        if (playerLocation >= transform.position.x)
        {
            movingLeft = false;
        }
        else if (playerLocation < transform.position.x)
        {
            movingLeft = true;
        }
    }

    void StrikePlayer()
    {
        if (!playerFound)
        {
            LocatePlayer();
        }
        else if (playerFound && !playerStruck)
        {
            if (movingLeft)//this if/else controls x direction towards player
            {
                //if(transform.position.x > playerLocationX)
                {
                    transform.position = new Vector3(transform.position.x - travelTime, transform.position.y, transform.position.z);
                }
            }
            else if (!movingLeft)
            {
                //if (transform.position.x < playerLocationX)
                {
                    transform.position = new Vector3(transform.position.x + travelTime, transform.position.y, transform.position.z);
                }
            }

            if (transform.position.y > -3.7) //controls y direction falling from ceiling
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - travelTimeY, transform.position.z);
                if (transform.position.y <= -3.7)
                {
                    playerStruck = true;
                }
            }

        }
    }

    void HoverBack()
    {
        sr.color = new Color(1, 1, 1, 1 - fadeAwayValue);
        fadeAwayValue += 0.01f;
        if (fadeAwayValue >= 1)
        {
            transform.position = HoverBackLoc;
            if(fadeAwayValue >= 1.1)
            {
                anim.SetInteger("State", 2);
            }
            if (fadeAwayValue >= 1.4)
            {
                hoveredBack = true;
                sr.color = new Color(1, 1, 1, 1);
            }
        }
    }

    IEnumerator WaitBeforeHoverBack(float time)
    {
        yield return new WaitForSeconds(time);
        readyToHoverBack = true;
    }

    IEnumerator PauseBeforeSecondDoll(float time)
    {
        yield return new WaitForSeconds(time);
        pausedBeforeSecondSpawn = true;
    }
}