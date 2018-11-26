using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float xSpeedL;
    [SerializeField] float xSpeedR;
    [SerializeField] float yForceL;
    [SerializeField] float yForceR;

    bool xSpeedChosen;
    float randomXspeed;
    float randomYforce;

    [SerializeField]
    AudioSource bounceSound;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        xSpeedChosen = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(!xSpeedChosen)
        {
            randomXspeed = Random.Range(xSpeedL, xSpeedR);
            xSpeedChosen = true;
        }
        rb.velocity = new Vector3(randomXspeed, rb.velocity.y, 0f);
        
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            bounceSound.Play();
            randomYforce = Random.Range(yForceL, yForceR);
            rb.AddForce(new Vector2(0f, randomYforce));
        }
    }
}
