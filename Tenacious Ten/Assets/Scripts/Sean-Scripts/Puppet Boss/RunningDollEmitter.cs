using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningDollEmitter : MonoBehaviour {

    public float SpawnDollAfterXSeconds;
    private float spawnTime;
    public GameObject RunningDoll;
    public Transform RunningDollEmitLocation;
    PuppetBossManager BM;

    StartOrResetLevel SORL;

	// Use this for initialization
	void Start () {
        spawnTime = SpawnDollAfterXSeconds;

        BM = GameObject.FindObjectOfType<PuppetBossManager>();

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = GameObject.FindObjectOfType<PuppetBossManager>();
        }

        if (BM.phaseOneActivated || BM.phaseTwoActivated)
        {
            SpawnDollAfterXSeconds -= Time.deltaTime;
            if (SpawnDollAfterXSeconds <= 0)
            {
                SpawnDollAfterXSeconds = spawnTime;
                Instantiate(RunningDoll, RunningDollEmitLocation.position, Quaternion.identity);
            }
        }

	}

    private void Update()
    {
        if(SORL.ResetFight)
        {
             SpawnDollAfterXSeconds = spawnTime;
        }
    }
}
