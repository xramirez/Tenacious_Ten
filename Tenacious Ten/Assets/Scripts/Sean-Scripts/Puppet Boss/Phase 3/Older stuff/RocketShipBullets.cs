using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShipBullets : MonoBehaviour {

    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float AngularSpeed;
    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.angularVelocity = AngularSpeed;
        rb.velocity = new Vector3(speedX, speedY, 0f);
	}
}
