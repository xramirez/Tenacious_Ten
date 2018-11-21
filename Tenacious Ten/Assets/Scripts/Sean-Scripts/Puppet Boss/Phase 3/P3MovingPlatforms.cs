using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P3MovingPlatforms : MonoBehaviour {

    [SerializeField] bool isShootingUp;
    [SerializeField] bool isShootingDown;
    [SerializeField] float InitialWaitTimer;
    [SerializeField] float ShootTimer;
    private float timer;

    [SerializeField] Transform SpawnOfShot;
    [SerializeField] GameObject UpwardShot;
    [SerializeField] GameObject DownwardShot;
    [SerializeField] float speed;
    Rigidbody2D rb;

    [SerializeField] bool isPhase4Platform;
    [SerializeField] float fadeInValue;

    SpriteRenderer sr;

    bool readyToMove;

    // Use this for initialization
    void Start () {
        timer = ShootTimer;
        sr = GetComponent<SpriteRenderer>();
        if(isPhase4Platform)
        {
            readyToMove = false;
            sr.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            readyToMove = true;
        }
        //rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(isPhase4Platform)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + fadeInValue);
            if(sr.color.a >= 1f)
            {
                readyToMove = true;
            }
        }

        //rb.velocity = new Vector3(speed, 0f, 0f);
        if(readyToMove)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            InitialWaitTimer -= Time.deltaTime;
            if (InitialWaitTimer <= 0)
            {
                ShootTimer -= Time.deltaTime;
                if ((isShootingUp) && ShootTimer <= 0)
                {
                    ShootTimer = timer;
                    Instantiate(UpwardShot, SpawnOfShot.transform.position, Quaternion.identity);
                }

                if ((isShootingDown) && ShootTimer <= 0)
                {
                    ShootTimer = timer;
                    Instantiate(DownwardShot, SpawnOfShot.transform.position, Quaternion.identity);
                }
            }
        }
        
	}
}
