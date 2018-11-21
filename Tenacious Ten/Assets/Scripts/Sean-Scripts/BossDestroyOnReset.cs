using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDestroyOnReset : MonoBehaviour {

    StartOrResetLevel SORL;

	// Use this for initialization
	void Start () {

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
	}
	
	// Update is called once per frame
	void Update () {
        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();

        if(SORL.ResetFight)
        {
            Destroy(gameObject);
        }
    }
}
