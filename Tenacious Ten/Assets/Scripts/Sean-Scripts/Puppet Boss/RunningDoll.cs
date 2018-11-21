using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningDoll : MonoBehaviour {

    Rigidbody2D rb;
    public float runSpeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        rb.velocity = new Vector3(runSpeed, 0f, 0f);

	}
}
