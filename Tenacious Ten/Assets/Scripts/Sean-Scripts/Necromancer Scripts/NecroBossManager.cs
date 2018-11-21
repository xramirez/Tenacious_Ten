using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroBossManager : MonoBehaviour {

    public bool Phase1Activated, Phase2Activated, Phase5Activated, Phase6Activated;

    StartOrResetLevel SORL;

    [SerializeField] GameObject NecromancerPhase1;
    [SerializeField] Transform NecromancerPhase1Loc;

    [SerializeField] Transform FairySpawn;
    [SerializeField] Transform LiquidSpawn;
    [SerializeField] Transform DamienSpawn;

    [SerializeField] GameObject FairyObject;
    [SerializeField] GameObject LiquidBoiObject;
    [SerializeField] GameObject DamienObject;

    [SerializeField] GameObject NecromancerPhase5;
    [SerializeField] Transform NecromancerPhase5Loc;

    [SerializeField] GameObject NecromancerPhase6;

    FairyMove FairyMove;
    DamienAttack DamienAttack;
    Boss5Samurai Boss5Samurai;

    FairyMove FairyMove2;
    DamienAttack DamienAttack2;
    Boss5Samurai Boss5Samurai2;

    bool NecroP5Summoned;

    public bool LiquidBoiSpawned, FairySpawned, DamienSpawned, MiniBossChosen;
    int MiniBossChoice;

    bool DamienIsDead, FairyIsDead, LiquidBoiIsDead;
    bool NecroPhase1Spawned;
    bool NecroPhase6Spawned;

    int[] Randomizer = new int[9];

    // Use this for initialization
    void Start () {
        Phase1Activated = false;
        Phase2Activated = false;
        Phase5Activated = false;
        Phase6Activated = false;

        NecroP5Summoned = false;
        LiquidBoiSpawned = false;
        FairySpawned = false;
        DamienSpawned = false;
        MiniBossChosen = false;

        NecroPhase1Spawned = false;

        SORL = FindObjectOfType<StartOrResetLevel>();

        DamienIsDead = false;
        LiquidBoiIsDead = false;
        FairyIsDead = false;

        //FairyMove = FindObjectOfType<FairyMove>();
        //DamienAttack = FindObjectOfType<DamienAttack>();
        //Boss5Samurai = FindObjectOfType<Boss5Samurai>();


    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(SORL.StartFight)
        {
            Phase1Activated = true;
            if(!NecroPhase1Spawned)
            {
                //Instantiate(NecromancerPhase1, NecromancerPhase1Loc.position, Quaternion.identity);
                NecroPhase1Spawned = true;
            }
        }

        if (LiquidBoiIsDead && FairyIsDead && DamienIsDead)
        {
            Phase2Activated = false;
            Phase5Activated = true;
        }


        if (Phase2Activated)
        {
            
            if (!MiniBossChosen)
            {
                if (!LiquidBoiSpawned && !FairySpawned && !DamienSpawned)
                {
                    MiniBossChoice = Random.Range(0, 3);
                    MiniBossChosen = true;
                    if(MiniBossChoice == 0)
                    {
                        LiquidBoiSpawned = true;
                        StartCoroutine(SummonLiquidBoi(2f));//Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
                    }
                    else if(MiniBossChoice == 1)
                    {
                        FairySpawned = true;
                        StartCoroutine(SummonFairy(2f));//Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
                    }
                    else if(MiniBossChoice == 2)
                    {
                        DamienSpawned = true;
                        StartCoroutine(SummonDamien(2f)); //Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
                    }
                }
                else if(!LiquidBoiSpawned || !FairySpawned || !DamienSpawned)
                {
                    //MiniBossChoice = Random.Range(0, 3);
                    if (LiquidBoiSpawned)
                    {
                        if(!FairySpawned && !DamienSpawned)
                        {
                            MiniBossChoice = Random.Range(0, 2);
                            if (MiniBossChoice == 0)
                            {
                                FairySpawned = true;
                                StartCoroutine(SummonFairy(2f)); //Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
                            }
                            else if(MiniBossChoice == 1)
                            {
                                DamienSpawned = true;
                                StartCoroutine(SummonDamien(2f)); //Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
                            }
                        }
                        else if(!FairySpawned)
                        {
                            FairySpawned = true;
                            StartCoroutine(SummonFairy(2f)); //Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
                        }
                        else if (!DamienSpawned)
                        {
                            DamienSpawned = true;
                            StartCoroutine(SummonDamien(2f)); //Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
                        }
                        MiniBossChosen = true;
                    }
                    else if (FairySpawned)
                    {
                        if (!LiquidBoiSpawned && !DamienSpawned)
                        {
                            MiniBossChoice = Random.Range(0, 2);
                            if (MiniBossChoice == 0)
                            {
                                LiquidBoiSpawned = true;
                                StartCoroutine(SummonLiquidBoi(2f));//Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
                            }
                            else if (MiniBossChoice == 1)
                            {
                                DamienSpawned = true;
                                StartCoroutine(SummonDamien(2f)); //Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
                            }
                        }
                        else if (!LiquidBoiSpawned)
                        {
                            LiquidBoiSpawned = true;
                            StartCoroutine(SummonLiquidBoi(2f));//Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
                        }
                        else if (!DamienSpawned)
                        {
                            DamienSpawned = true;
                            StartCoroutine(SummonDamien(2f)); //Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
                        }
                        MiniBossChosen = true;
                    }
                    else if (DamienSpawned)
                    {
                        if (!LiquidBoiSpawned && !FairySpawned)
                        {
                            MiniBossChoice = Random.Range(0, 2);
                            if (MiniBossChoice == 0)
                            {
                                LiquidBoiSpawned = true;
                                StartCoroutine(SummonLiquidBoi(2f));//Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
                            }
                            else if (MiniBossChoice == 1)
                            {
                                FairySpawned = true;
                                StartCoroutine(SummonFairy(2f)); //Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
                            }
                        }
                        else if (!FairySpawned)
                        {
                            FairySpawned = true;
                            StartCoroutine(SummonFairy(2f)); //Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
                        }
                        else if (!LiquidBoiSpawned)
                        {
                            LiquidBoiSpawned = true;
                            StartCoroutine(SummonLiquidBoi(2f));//Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
                        }
                        MiniBossChosen = true;
                    }
                }

            }
            else if(MiniBossChosen)
            {
                if (FairySpawned && !FairyIsDead)// && !FairyMove.FirstDeath)
                {
                    //FairyMove = FairyObject.GetComponent<FairyMove>();
                    FairyMove = FindObjectOfType<FairyMove>();
                    //if(!FairyMove.FirstDeath)
                    {
                        if (FairyMove.FirstDeath)
                        {
                            FairyIsDead = true;
                            FairyMove.FirstDeath = false;
                            MiniBossChosen = false;
                        }
                    }
                }
                if(DamienSpawned && !DamienIsDead)// && !DamienAttack.FirstDeath)
                {
                    //DamienAttack = DamienObject.GetComponent<DamienAttack>();
                    DamienAttack = FindObjectOfType<DamienAttack>();
                    //if(!DamienAttack.FirstDeath)
                    {
                        if (DamienAttack.FirstDeath)
                        {
                            DamienIsDead = true;
                            DamienAttack.FirstDeath = false;
                            MiniBossChosen = false;
                        }
                    }
                }
                if(LiquidBoiSpawned && !LiquidBoiIsDead)// && !Boss5Samurai.FirstDeath)
                {
                    //Boss5Samurai = LiquidBoiObject.GetComponent<Boss5Samurai>();
                    Boss5Samurai = FindObjectOfType<Boss5Samurai>();
                    //if (!Boss5Samurai.FirstDeath)
                    {
                        if (Boss5Samurai.FirstDeath)
                        {
                            LiquidBoiIsDead = true;
                            Boss5Samurai.FirstDeath = false;
                            MiniBossChosen = false;
                        }
                    }
                }

            }
        }//end if phase 2 activated

        if(Phase5Activated)
        {
            if(!NecroP5Summoned)
            {
                Instantiate(NecromancerPhase5, NecromancerPhase5Loc.position, Quaternion.identity);
                NecroP5Summoned = true;
            }
            if(NecroP5Summoned)
            {
                if(FindObjectOfType<Boss5Samurai>() != null)
                {
                    Boss5Samurai2 = FindObjectOfType<Boss5Samurai>();
                }
                if(FindObjectOfType<DamienAttack>() != null)
                {
                    DamienAttack2 = FindObjectOfType<DamienAttack>();
                }
                if(FindObjectOfType<FairyMove>() != null)
                {
                    FairyMove2 = FindObjectOfType<FairyMove>();
                }
                if(Boss5Samurai2.FirstDeath && DamienAttack2.FirstDeath && FairyMove2.FirstDeath)
                {
                    if(!NecroPhase6Spawned)
                    {
                        StartCoroutine(SummonNecromancerPhase6(2f));
                        NecroPhase6Spawned = true;
                    }
                }
            }
        }

	}//end fixedUpdate

    void Update()
    {
        
        if(SORL.ResetFight)
        {
            Phase1Activated = false;
            Phase2Activated = false;
            Phase5Activated = false;
            Phase6Activated = false;

            NecroP5Summoned = false;
            NecroPhase6Spawned = false;

            LiquidBoiSpawned = false;
            FairySpawned = false;
            DamienSpawned = false;
            MiniBossChosen = false;

            DamienIsDead = false;
            LiquidBoiIsDead = false;
            FairyIsDead = false;
        }
    }

    IEnumerator SummonDamien(float time)
    {
        yield return new WaitForSeconds(time);
        if(Phase2Activated)
        {
            Instantiate(DamienObject, DamienSpawn.position, Quaternion.identity);
        }
    }

    IEnumerator SummonFairy(float time)
    {
        yield return new WaitForSeconds(time);
        if(Phase2Activated)
        {
            Instantiate(FairyObject, FairySpawn.position, Quaternion.identity);
        }
    }

    IEnumerator SummonLiquidBoi(float time)
    {
        yield return new WaitForSeconds(time);
        if(Phase2Activated)
        {
            Instantiate(LiquidBoiObject, LiquidSpawn.position, Quaternion.identity);
        }
    }

    IEnumerator SummonNecromancerPhase6(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(NecromancerPhase6, NecromancerPhase5Loc.position, Quaternion.identity);
    }
}
