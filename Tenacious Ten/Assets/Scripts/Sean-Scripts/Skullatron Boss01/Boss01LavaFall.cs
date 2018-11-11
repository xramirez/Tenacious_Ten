using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01LavaFall : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float lavaSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector3(0f, -lavaSpeed, 0f);
	}
}
