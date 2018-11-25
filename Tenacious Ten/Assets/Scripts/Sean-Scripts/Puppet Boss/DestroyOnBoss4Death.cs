using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnBoss4Death : MonoBehaviour {

    PuppetBossManager BM;
    bool fightComplete;

	// Use this for initialization
	void Start () {
        BM = GameObject.FindObjectOfType<PuppetBossManager>();
        fightComplete = false;
	}
	
	// Update is called once per frame
	void Update() {

        if (GameObject.FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = GameObject.FindObjectOfType<PuppetBossManager>();
        }
        
        

        if(BM.GetComponent<EnemyHealthManager>().enemyHealth <= 0)
        { 
            Destroy(gameObject);
        }

	}
}
