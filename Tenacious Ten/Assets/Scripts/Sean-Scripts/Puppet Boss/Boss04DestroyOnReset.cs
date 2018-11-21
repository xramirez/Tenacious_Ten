using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss04DestroyOnReset : MonoBehaviour {

    StartOrResetLevel SORL;

	// Use this for initialization
	void Start () {
        SORL = FindObjectOfType<StartOrResetLevel>();
	}
	
	// Update is called once per frame
	void Update () {

		if(SORL.ResetFight)
        {
            Destroy(gameObject);
        }
	}
}
