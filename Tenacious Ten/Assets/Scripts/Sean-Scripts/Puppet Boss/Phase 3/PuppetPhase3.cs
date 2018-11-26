using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetPhase3 : MonoBehaviour {

    PuppetBossManager BM;
    [SerializeField] float PlatformSpawnTimer;
    private float PSTimer;
    [SerializeField] GameObject UpPlatform;
    [SerializeField] GameObject DownPlatform;
    [SerializeField] Transform PlatformSpawnLoc;
    GameObject PlatformToSpawn;
    int UpPlatformCounter, DownPlatformCounter;
    int ChoosePlatform;
    bool PlatformChosen;
    int PlatformCount;

    [SerializeField] GameObject Airplane;
    [SerializeField] GameObject Rocketship;
    [SerializeField] Transform AerialSpawner;

    [SerializeField] GameObject BouncyBallCannon;
    [SerializeField] Transform CannonSpawnLoc;
    bool CannonIsOut;

    [SerializeField]  float P3AbilityBuffer;
    private float P3Buffer;

    int P3abilitySwitch; //if 0, platforms, if 1, plane + rocketship, either P --> R, or vice versa
    int AerialSpawnRandomizer;
    bool AerialSpawnChosen;

    bool AerialsFlying, PlatformsOut;
    bool AbilityFinished, OneAerialOut;
    [SerializeField] float TimeBetweenAerials;
    private float StartTimeBetweenAerials;

    StartOrResetLevel SORL;

    [SerializeField]
    AudioSource ballBounce;

    [SerializeField]
    AudioSource planeSpawn;

    [SerializeField]
    AudioSource ballSpawn;

    [SerializeField]
    AudioSource rocketSpawn;

    [SerializeField]
    AudioSource upSpawn;

    [SerializeField]
    AudioSource downSpawn;


    // Use this for initialization
    void Start () {
        PSTimer = PlatformSpawnTimer;
        UpPlatformCounter = 0;
        DownPlatformCounter = 0;
        PlatformCount = 0;
        PlatformChosen = false;

        AerialSpawnChosen = false;
        AerialsFlying = false;
        PlatformsOut = false;
        AbilityFinished = false;
        OneAerialOut = false;
        CannonIsOut = false;
        P3Buffer = P3AbilityBuffer;

        P3abilitySwitch = Random.Range(0,2);

        StartTimeBetweenAerials = TimeBetweenAerials;

        BM = GameObject.FindObjectOfType<PuppetBossManager>();

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
    }

    void Update()
    {
        if(SORL.ResetFight)
        {
            P3AbilityBuffer = P3Buffer;
            CannonIsOut = false;
            AbilityFinished = false;
            PlatformChosen = false;
            PlatformCount = 0;
            PlatformSpawnTimer = PSTimer;
            UpPlatformCounter = 0;
            DownPlatformCounter = 0;
        }
    }

   
    void FixedUpdate () {

        if (FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = GameObject.FindObjectOfType<PuppetBossManager>();
        }

        if (BM.phaseThreeActivated && BM.hasMovedForPhase3)
        {
            if(AbilityFinished)
            {
                P3AbilityBuffer -= Time.deltaTime;
                if(P3AbilityBuffer <= 0)
                {
                    AbilityFinished = false;
                    P3AbilityBuffer = P3Buffer;
                }
            }



            if(P3abilitySwitch == 0 && !AbilityFinished)
            {
                SpawnPlatforms();
                if(PlatformCount > 13)
                {
                    PlatformCount = 0;
                    AbilityFinished = true;
                    P3abilitySwitch = 1;
                }
            }
            else if(P3abilitySwitch == 1 && !AbilityFinished)
            {
                if(!CannonIsOut)
                {
                    CannonIsOut = true;
                    Instantiate(BouncyBallCannon, CannonSpawnLoc.position, Quaternion.Euler(0f,0f,180f));
                    StartCoroutine(waitBouncyBalls(12f));
                }
            }
            else if(false)//(P3abilitySwitch == 1 && !AbilityFinished)
            {
                if(!AerialSpawnChosen)
                {
                    AerialSpawnRandomizer = Random.Range(0, 2);
                    AerialSpawnChosen = true;
                }

                if(AerialSpawnRandomizer == 0)
                {
                    if(!OneAerialOut)
                    {
                        SpawnAirplane();
                        OneAerialOut = true;
                    }
                    else
                    {
                        TimeBetweenAerials -= Time.deltaTime;
                        if(TimeBetweenAerials <= 0)
                        {
                            AerialSpawnChosen = false;
                            TimeBetweenAerials = StartTimeBetweenAerials;
                            SpawnRocketship();
                            OneAerialOut = false;
                            AbilityFinished = true;
                            P3abilitySwitch = 0;
                        }

                    }
                }
                else
                {
                    if (!OneAerialOut)
                    {
                        SpawnRocketship();
                        OneAerialOut = true;
                        TimeBetweenAerials = TimeBetweenAerials / 2;
                    }
                    else
                    {
                        TimeBetweenAerials -= Time.deltaTime;
                        if (TimeBetweenAerials <= 0)
                        {
                            AerialSpawnChosen = false;
                            TimeBetweenAerials = StartTimeBetweenAerials;
                            SpawnAirplane();
                            OneAerialOut = false;
                            AbilityFinished = true;
                            P3abilitySwitch = 0;
                            P3AbilityBuffer = P3AbilityBuffer + 4.5f;
                        }

                    }
                }


            }


        }
        

	}

    void SpawnPlatforms()
    {
        PlatformSpawnTimer -= Time.deltaTime;
        if (PlatformSpawnTimer <= 0)
        {
            Debug.Log("HHHHHHHHHHHHAAAAAAAAAAUUUUUUUUUUHHHHHHHHHH ");
            if(!PlatformChosen)
            {
                ChoosePlatform = Random.Range(0, 2);
            }
            
            if (ChoosePlatform == 0 && UpPlatformCounter < 2)
            {
                DownPlatformCounter = 0;
                upSpawn.Play(); // Sound
                Instantiate(UpPlatform, PlatformSpawnLoc.position, Quaternion.identity);
                PlatformChosen = false;
                //PlatformToSpawn = UpPlatform;
                UpPlatformCounter++;
                Debug.Log(UpPlatformCounter);
                Debug.Log("HAUH");
            }

            if (ChoosePlatform == 1 && DownPlatformCounter < 2)
            {
                UpPlatformCounter = 0;
                downSpawn.Play(); // Sound
                Instantiate(DownPlatform, PlatformSpawnLoc.position, Quaternion.identity);
                //PlatformToSpawn = DownPlatform;
                DownPlatformCounter++;
                PlatformChosen = false;
                Debug.Log(DownPlatformCounter);
                Debug.Log("OK OK ");
            }

            if(UpPlatformCounter == 2)
            {
                PlatformChosen = true;
                ChoosePlatform = 1;
            }
            else if (DownPlatformCounter == 2)
            {
                PlatformChosen = true;
                ChoosePlatform = 0;
            }
            //Instantiate(PlatformToSpawn, PlatformSpawnLoc.position, Quaternion.identity);
            PlatformSpawnTimer = PSTimer;
            PlatformCount++;
        }
    }

    void SpawnAirplane()
    {
        planeSpawn.Play(); // Sound
        Instantiate(Airplane, AerialSpawner.position, Quaternion.identity);
    }

    void SpawnRocketship()
    {
        rocketSpawn.Play(); // Sound
        Instantiate(Rocketship, AerialSpawner.position, Quaternion.identity);
    }

    IEnumerator waitBouncyBalls(float time)
    {
        ballSpawn.Play(); // Sound
        yield return new WaitForSeconds(time);
        AbilityFinished = true;
        P3abilitySwitch = 0;
        CannonIsOut = false;
    }
}
