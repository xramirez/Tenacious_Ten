using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3PlatformShots : MonoBehaviour {

    [SerializeField] bool isUpwardShot;
    [SerializeField] bool isDownwardShot;
    [SerializeField] bool isForPhase4MoveRight;
    Rigidbody2D rb;
    [SerializeField] float ShotSpeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(isUpwardShot)
        {
            rb.velocity = new Vector3(-ShotSpeed, ShotSpeed, 0f);
        }

        if(isDownwardShot)
        {
            rb.velocity = new Vector3(-ShotSpeed, -ShotSpeed, 0f);
        }
        if(isForPhase4MoveRight)
        {
            rb.velocity = new Vector3(ShotSpeed, -ShotSpeed, 0f);
        }
	}
}
