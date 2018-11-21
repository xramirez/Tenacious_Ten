using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTest : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Moving Platform")
        {
            transform.SetParent(other.transform);
            Debug.Log("Setting Parent to platform!");
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Moving Platform")
        {
            transform.SetParent(null);
            Debug.Log("No parent!");
        }
    }
}
