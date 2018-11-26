using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBallCannon : MonoBehaviour {

    [SerializeField] GameObject BouncyBall;
    Transform EmitLoc;
    bool readyToShoot;
    [SerializeField] float shotTimer;
    float initShotTimer;
    [SerializeField] int shotCounter;

    [SerializeField]
    AudioSource cannonSound;

	// Use this for initialization
	void Start () {
		EmitLoc = this.gameObject.transform.GetChild(0);
        readyToShoot = false;
        initShotTimer = shotTimer;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(!readyToShoot)
        {
            transform.position = new Vector3(transform.position.x - 0.04f, transform.position.y, 0f);
            if(transform.position.x <= 10)
            {
                readyToShoot = true;
            }
        }

        if(readyToShoot)
        {

            shotTimer -= Time.deltaTime;
            if(shotTimer <= 0 && shotCounter >= 0)
            {
                cannonSound.Play();
                shotCounter--;
                shotTimer = initShotTimer;
                Instantiate(BouncyBall, EmitLoc.position, Quaternion.identity); // Quaternion.Euler(0f, 0f, 90f));
            }

            if(shotCounter < 0)
            {
                transform.position = new Vector3(transform.position.x + 0.04f, transform.position.y, 0f);
                if (transform.position.x >= 12)
                {
                    Destroy(gameObject);
                }
            }

        }

	}
}
