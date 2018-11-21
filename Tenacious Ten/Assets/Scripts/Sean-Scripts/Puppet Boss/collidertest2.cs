using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidertest2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
	}
}
