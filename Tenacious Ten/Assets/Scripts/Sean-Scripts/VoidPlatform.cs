using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidPlatform : MonoBehaviour {

    [SerializeField] float speedFactor;
    [SerializeField] float distance;
    [SerializeField] bool StartMoveUp;
    [SerializeField] bool StartMoveDown;
    float initialYLoc;
    bool movingDown;
    bool movingUp;

    // Use this for initialization
    void Start () {
        initialYLoc = transform.position.y;
        if(StartMoveUp)
        {
            movingUp = true;
            movingDown = false;
        }
        if(StartMoveDown)
        {
            movingUp = false;
            movingDown = true;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
       if(movingUp)
       {
           transform.position = new Vector3(transform.position.x, transform.position.y + speedFactor, 0f);
       }
            
       if(movingDown)
       {
           transform.position = new Vector3(transform.position.x, transform.position.y - speedFactor, 0f);
       }
        
       if(StartMoveUp)
        {
            if (transform.position.y >= initialYLoc + distance && movingUp)
            {
                movingUp = false;
                movingDown = true;
            }

            if (transform.position.y <= initialYLoc && movingDown)
            {
                movingUp = true;
                movingDown = false;
            }
        }

       if(StartMoveDown)
        {
            if (transform.position.y >= initialYLoc && movingUp)
            {
                movingUp = false;
                movingDown = true;
            }

            if (transform.position.y <= initialYLoc - distance && movingDown)
            {
                movingUp = true;
                movingDown = false;
            }
        }
    }
}
