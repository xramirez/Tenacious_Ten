using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    /*Instructions on how to use:
     * There are three game Objects in Unity called Moving_Platform, Small Platform, and PositionB
     * Copy it and move the Small Platform Object towards the starting position
     * Move the PositionB object towards the desired destination of the moving platform. You will need to set the coordinates of the destination manually for the most accurate representation
     * Note: PositionB will be an invicible object
     * 
     */

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nexPos;


    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

	// Use this for initialization
	void Start () {

        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nexPos = posB;

	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);
        if(Vector3.Distance(childTransform.localPosition,nexPos) <= 0.1)
        {
            ChangeDestination();
        }


    }

    private void ChangeDestination()
    {
        nexPos = nexPos != posA ? posA : posB;

    }
}
