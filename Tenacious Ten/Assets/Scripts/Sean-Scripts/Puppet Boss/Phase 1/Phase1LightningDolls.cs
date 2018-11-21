using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase1LightningDolls : MonoBehaviour {

    bool playerFound;
    float playerLocationX, playerLocationY;
    GameObject player;
    bool movingLeft, readyToMove, hitGround;
    float distanceFromPlayer, displacementY;
    float travelTimeX, travelTimeY;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
        movingLeft = false;
        readyToMove = false;
        hitGround = false;
        playerFound = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        Destroy(gameObject, 12f);

        if(!playerFound)
        {
            LocatePlayer();
            Debug.Log("Player Found");
        }
        else if(playerFound && !readyToMove)
        {
            if(transform.position.x >= playerLocationX)
            {
                movingLeft = true;
            }
            else if(transform.position.x <= playerLocationX && !readyToMove)
            {
                movingLeft = false;
            }
            readyToMove = true;
        }
        else if(readyToMove)
        {
            
            if(movingLeft)//this if/else controls x direction towards player
            {
                //if(transform.position.x > playerLocationX)
                {
                    transform.position = new Vector3(transform.position.x - travelTimeX, transform.position.y, transform.position.z);
                }
            }
            else if(!movingLeft)
            {
                //if (transform.position.x < playerLocationX)
                {
                    transform.position = new Vector3(transform.position.x + travelTimeX, transform.position.y, transform.position.z);
                }
            }
            
            if (transform.position.y > -5 && !hitGround) //controls y direction falling from ceiling
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - travelTimeY, transform.position.z);
            }
            else if(transform.position.y < -5)
            {
                hitGround = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + travelTimeY, transform.position.z);
            }
            else if (transform.position.y >= -5 && hitGround)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + travelTimeY, transform.position.z);
            }

        }
	}

    void LocatePlayer()
    {
        playerFound = true;
        playerLocationX = player.transform.position.x;
        playerLocationY = player.transform.position.y;
        distanceFromPlayer = Mathf.Abs(transform.position.x - playerLocationX);
        displacementY = Mathf.Abs(transform.position.y - playerLocationY);
        travelTimeX = distanceFromPlayer / 60;
        travelTimeY = displacementY / 60;
    }
}
