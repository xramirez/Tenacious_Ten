using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4FightManager : MonoBehaviour {

    [SerializeField] GameObject Phase1HandsL;
    [SerializeField] GameObject Phase1HandsR;
    [SerializeField] Transform Phase1HandsSpawnL;
    [SerializeField] Transform Phase1HandsSpawnR;
    [SerializeField] GameObject Phase2HandsL;
    [SerializeField] GameObject Phase2HandsR;
    [SerializeField] GameObject Puppeteer;
    [SerializeField] Transform BossSpawn;
    //[SerializeField] Transform Phase2HandsSpawnLeft;

    PuppetBossManager BM;

    bool phase1HandsSpawned, phase2HandsSpawned;
    float phase2Timer;
    float phase1Timer;

    StartOrResetLevel SORL;

    //[SerializeField]
    // Use this for initialization
    void Start () {
        phase1HandsSpawned = false;
        phase2HandsSpawned = false;
        SORL = FindObjectOfType<StartOrResetLevel>();

        phase2Timer = 2f;
        phase1Timer = 2f;

        BM = FindObjectOfType<PuppetBossManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = FindObjectOfType<PuppetBossManager>();
        }

        if (SORL.StartFight)
        {
            //if (BM.phaseOneActivated)
            {
                phase1Timer -= Time.deltaTime;
                if (!phase1HandsSpawned && phase1Timer <= 0)
                {
                    Instantiate(Phase1HandsL, Phase1HandsSpawnL.position, Quaternion.identity);
                    Instantiate(Phase1HandsR, Phase1HandsSpawnR.position, Quaternion.identity);
                    Instantiate(Puppeteer, BossSpawn.position, Quaternion.identity);
                    phase1HandsSpawned = true;
                }
            }

            if (BM.phaseTwoActivated)
            {
                phase2Timer -= Time.deltaTime;
                if (!phase2HandsSpawned && phase2Timer <= 0)
                {
                    Instantiate(Phase2HandsL, Phase1HandsSpawnL.position, Quaternion.identity);
                    Instantiate(Phase2HandsR, Phase1HandsSpawnR.position, Quaternion.identity);
                    phase2HandsSpawned = true;
                }
            }
        }
        

	}

    void Update()
    {
        if(SORL.ResetFight)
        {
            phase2Timer = 4f;
            phase1Timer = 2.5f;
            phase1HandsSpawned = false;
            phase2HandsSpawned = false;
        }

    }
}
