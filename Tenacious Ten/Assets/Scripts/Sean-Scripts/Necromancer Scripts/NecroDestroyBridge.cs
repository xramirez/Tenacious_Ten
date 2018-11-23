using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroDestroyBridge : MonoBehaviour {

    NecromancerPhase1 P1;

	// Use this for initialization
	void Start () {
        P1 = FindObjectOfType<NecromancerPhase1>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if(P1.DestroyBridge)
        {
            Destroy(gameObject, 1f);
        }

	}
}
