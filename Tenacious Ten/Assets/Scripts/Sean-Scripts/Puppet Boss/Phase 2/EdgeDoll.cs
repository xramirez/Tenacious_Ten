using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDoll : MonoBehaviour {

    bool FirstGroundHit, hitWall, hitCeiling, FallDown;
    [SerializeField] bool FromLeftHand, FromRightHand;
    [SerializeField] float moveSpeed;
    GameObject player;
    private int circulationCounter;
    [SerializeField] int maxAmtCirculations;
    [SerializeField] GameObject deathEffect;
    PuppetBossManager BM;

	// Use this for initialization
	void Start () {
        FirstGroundHit = false;
        hitWall = false;
        hitCeiling = false;
        FallDown = false;
        player = GameObject.FindWithTag("Player");
        circulationCounter = 0;

        BM = GameObject.FindObjectOfType<PuppetBossManager>();
    }
	
	
	void FixedUpdate () {

        if(FindObjectOfType<PuppetBossManager>() != null)
        {
            BM = GameObject.FindObjectOfType<PuppetBossManager>();
        }

        if(BM.phaseThreeActivated)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (!FirstGroundHit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed , transform.position.z);
            if(transform.position.y <= -4.1)
            {
                FirstGroundHit = true;
            }
        }
        else if(FirstGroundHit && !hitWall)
        {
            if(FromLeftHand)
            {
                transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
                if(transform.position.x <= -9.6)
                {
                    hitWall = true;
                }
            }
            if(FromRightHand)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
                if (transform.position.x >= 9.6)
                {
                    hitWall = true;
                }
            }
        }
        else if(hitWall && !hitCeiling)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
            if(transform.position.y >= 4.1)
            {
                hitCeiling = true;
            }
        }
        else if(hitCeiling && !FallDown)
        {
            if (FromLeftHand)
            {
                transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
                if (transform.position.x >= player.transform.position.x)
                {
                    FallDown = true;
                }
            }
            if (FromRightHand)
            {
                transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, transform.position.z);
                if (transform.position.x <= player.transform.position.x)
                {
                    FallDown = true;
                }
            }
        }
        else if(FallDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
            if (transform.position.y <= -4.1)
            {
                circulationCounter++;
                FirstGroundHit = false;
                hitWall = false;
                hitCeiling = false;
                FallDown = false;
                if(circulationCounter == maxAmtCirculations)
                {
                    Instantiate(deathEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                //Destroy(gameObject);
            }
        }

	}
}
