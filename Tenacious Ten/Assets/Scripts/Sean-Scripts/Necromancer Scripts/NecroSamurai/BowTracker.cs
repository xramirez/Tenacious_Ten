using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTracker : MonoBehaviour {

    //[SerializeField] Transform PlayerPos;

    Transform PlayerPos;

    // Use this for initialization
    void Start () {
       
        PlayerPos = GameObject.Find("Player").transform;

        transform.right = PlayerPos.position - transform.position;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(PlayerPos.position.x);
        Debug.Log("That's x ^ ^ ");
        Debug.Log(PlayerPos.position.y);
        Debug.Log("That's y ^ ^ ");
        transform.right = PlayerPos.position - transform.position;
    }
}
