using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravBallEmitterTest : MonoBehaviour {

    [SerializeField] float PublicTimer;
    [SerializeField] Transform spawn;
    [SerializeField] GameObject Ball;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = PublicTimer; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        PublicTimer -= Time.deltaTime;
        if(PublicTimer <= 0)
        {
            PublicTimer = timer;
            Instantiate(Ball, spawn.position, Quaternion.identity);
        }

	}
}
