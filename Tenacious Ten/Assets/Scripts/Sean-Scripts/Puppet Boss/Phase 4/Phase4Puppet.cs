using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase4Puppet : MonoBehaviour {

    PuppetBossManager BM;
    [SerializeField] GameObject balls;
    [SerializeField] Transform BigBallSpawnerLeft;
    [SerializeField] Transform BigBallSpawnerRight;
    [SerializeField] GameObject BigBall;

    Transform BBspawnLeftStart;
    Transform BBspawnRightStart;

    bool ballsCasted;
    [SerializeField] float waitToSendBalls;
    private float waitBallTimer;
    int ballDirRandomizer;
    bool ballDirChosen, ellipseBallsOnTheMove;
    int RainCounter;
    [SerializeField] float RainTimer;
    float initialRainTimer;
    [SerializeField]float FirstRainBall;
    float initialFirstRainBall;

    bool CastingBalls, CastingPlatforms;

    [SerializeField] float P4AbilityBuffer;
    float initAbilityBuffer;

    [SerializeField] GameObject PlatformMoveLeft;
    [SerializeField] GameObject PlatformMoveRight;
    [SerializeField] Transform PlatformLocUpperL;
    [SerializeField] Transform PlatformLocUpperR;
    [SerializeField] Transform PlatformLocLowerR;
    [SerializeField] Transform PlatformLocLowerL;

    [SerializeField] float catSpawnTimer;
    [SerializeField] GameObject CatDeploy;
    [SerializeField] Transform CatSpawn;
    float initCatSpawnTimer;

    int platformRandomizer;
    bool platformLocsChosen, platformsOut;

    StartOrResetLevel SORL;

    bool fightOver;

    [SerializeField]
    AudioSource catVroom;

    [SerializeField]
    AudioSource downSpawn;

    [SerializeField]
    AudioSource ballSpawn;

    // Use this for initialization
    void Start () {

        initCatSpawnTimer = catSpawnTimer;

        ballsCasted = false;
        ballDirChosen = false;
        ellipseBallsOnTheMove = false;
        RainCounter = 0;
        waitBallTimer = waitToSendBalls;
        initialRainTimer = RainTimer;
        initialFirstRainBall = FirstRainBall;

        CastingBalls = false;
        CastingPlatforms = false;
        platformLocsChosen = false;
        platformsOut = false;

        initAbilityBuffer = P4AbilityBuffer;

        BBspawnLeftStart.localPosition = BigBallSpawnerLeft.localPosition;
        BBspawnRightStart.localPosition = BigBallSpawnerRight.localPosition;

        BM = GameObject.FindObjectOfType<PuppetBossManager>();

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();

        fightOver = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = GameObject.FindObjectOfType<PuppetBossManager>();
        }
        

		if(BM.hasMovedForPhase4 && !fightOver)
        {
            P4AbilityBuffer -= Time.deltaTime;

            catSpawnTimer -= Time.deltaTime;
            if(catSpawnTimer <= 0)
            {
                catVroom.Play(); // Sound
                Instantiate(CatDeploy, CatSpawn.position, Quaternion.identity);
                catSpawnTimer = initCatSpawnTimer;
            }

            if(!ballsCasted && P4AbilityBuffer <= 0 && !CastingPlatforms)
            {
                CastInitialBalls();
                CastingBalls = true;
            }
            else if(ballsCasted)
            {
                if(!ellipseBallsOnTheMove)
                {
                    waitToSendBalls -= Time.deltaTime;
                }
                if(waitToSendBalls <= 0 && !ellipseBallsOnTheMove)
                {
                    ellipticalBalls.moveBalls = true;
                    if(!ballDirChosen)
                    {
                        ballDirRandomizer = Random.Range(0, 2);
                        Debug.Log(ballDirRandomizer);
                        ballDirChosen = true;
                    }

                    if (ballDirRandomizer == 0 && !ellipseBallsOnTheMove) //go left
                    {
                        if(ellipticalBalls.moveSpeedX > 0)
                        {
                            ellipticalBalls.moveSpeedX = -ellipticalBalls.moveSpeedX;
                        }
                        ellipseBallsOnTheMove = true;
                        waitToSendBalls = waitBallTimer;
                    }
                    else //go right
                    {
                        if (ellipticalBalls.moveSpeedX < 0)
                        {
                            ellipticalBalls.moveSpeedX = -ellipticalBalls.moveSpeedX;
                        }
                        ellipseBallsOnTheMove = true;
                        waitToSendBalls = waitBallTimer;
                    }
                    //waitToSendBalls = waitBallTimer;
                }
                else if (ellipseBallsOnTheMove)
                {
                    FirstRainBall -= Time.deltaTime;
                    if(FirstRainBall <= 0)
                    {
                        RainTimer -= Time.deltaTime;
                    }

                    if (RainTimer <= 0)
                    {
                        RainTimer = initialRainTimer;
                        CastRainOfBalls();
                        if (RainCounter >= 8)
                        {
                            //P4AbilityBuffer = 100;
                            P4AbilityBuffer = initAbilityBuffer;
                            ballsCasted = false;
                            ballDirChosen = false;
                            ellipseBallsOnTheMove = false;
                            RainCounter = 0;
                            ellipticalBalls.moveBalls = false;
                            CastingPlatforms = true;
                            //BigBallSpawnerLeft.localPosition = BBspawnLeftStart.localPosition;
                            //BigBallSpawnerRight.localPosition = BBspawnRightStart.localPosition;
                            RainTimer = initialRainTimer;
                            FirstRainBall = initialFirstRainBall;
                        }
                    }

                }
            }

            if (P4AbilityBuffer <= 0 && CastingPlatforms)
            {
                CastPlatforms();
            }

        }
	}

    void Update()
    {
        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
        if (SORL.ResetFight)
        {
            CastingBalls = false;
            CastingPlatforms = false;
            platformLocsChosen = false;
            platformsOut = false;

            ballsCasted = false;
            ballDirChosen = false;
            ellipseBallsOnTheMove = false;

            P4AbilityBuffer = initAbilityBuffer;

            BigBallSpawnerLeft.localPosition = new Vector3(-10.95f, 8.07f, 0f);
            BigBallSpawnerRight.localPosition = new Vector3(10.95f, 8.07f, 0f);
        }

        if (BM.GetComponent<EnemyHealthManager>().enemyHealth <= 0)
        {
            fightOver = true;
        }

    }

    void CastPlatforms()
    {
        if(!platformLocsChosen)
        {
            platformRandomizer = Random.Range(0, 2);
            platformLocsChosen = true;
        }
        else if(platformLocsChosen && !platformsOut)
        {
            if(platformRandomizer == 0)
            {
                downSpawn.Play(); // Sound
                Instantiate(PlatformMoveLeft, PlatformLocUpperR.position, Quaternion.identity);
                Instantiate(PlatformMoveRight, PlatformLocLowerL.position, Quaternion.identity);
            }
            else if(platformRandomizer == 1)
            {
                downSpawn.Play(); // Sound
                Instantiate(PlatformMoveLeft, PlatformLocLowerR.position, Quaternion.identity);
                Instantiate(PlatformMoveRight, PlatformLocUpperL.position, Quaternion.identity);
            }

            platformsOut = true;
            StartCoroutine(waitForPlatforms(3f));
        }
        
    }

    IEnumerator waitForPlatforms(float time)
    {
        yield return new WaitForSeconds(time);
        CastingPlatforms = false;
        platformLocsChosen = false;
        platformsOut = false;
        P4AbilityBuffer = initAbilityBuffer;
    }

    void CastInitialBalls()
    {
        Instantiate(balls, transform.position, Quaternion.identity);  //doesn't matter where it is instantiates due to the nature of how the balls work
        ballsCasted = true;
    }

    void CastRainOfBalls()
    {
        Vector3 temp = BigBallSpawnerLeft.localPosition;
        Vector3 temp2 = BigBallSpawnerRight.localPosition;
       

        if (RainCounter < 8)
        {
            RainCounter++;
        }

        if(ballDirRandomizer == 0)
        {
            ballSpawn.Play(); // Sound
            Instantiate(BigBall, BigBallSpawnerLeft.position, Quaternion.identity);
            temp.x = temp.x +  2.5f;
        }

        else if(ballDirRandomizer == 1)
        {
            ballSpawn.Play(); // Sound
            Instantiate(BigBall, BigBallSpawnerRight.position, Quaternion.identity);
            temp2.x = temp2.x - 2.5f;
        }

        if(RainCounter == 8)
        {
            if (ballDirRandomizer == 0)
            {
                temp.x = temp.x - (8 * (2.5f));
            }
            if(ballDirRandomizer == 1)
            {
                temp2.x = temp2.x + (8 *(2.5f));
            }
            
        }
        BigBallSpawnerLeft.localPosition = temp;
        BigBallSpawnerRight.localPosition = temp2;
    }
}
