using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2BloodProj : MonoBehaviour {

    public static float speedX;
    public static float speedY;
    Rigidbody2D rb;
    float timer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        speedX = 5;
        timer = 2f;
    }
	
	// Update is called once per frame
	void Update () {

        //rb.velocity = new Vector3(speedX, speedY, 0);
        //rb.AddForce(new Vector2(speedX, speedY));
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            //rb.AddForce(transform.up * 15);
            rb.velocity = (transform.up * 15);
            timer = 2f;
        }
        Object.Destroy(gameObject, 9f);
    }

    public static void changeBloodVelocity(float newSpeedX, float newSpeedY)
    {
        speedY = newSpeedY;
        speedX = newSpeedX;
    }
}
