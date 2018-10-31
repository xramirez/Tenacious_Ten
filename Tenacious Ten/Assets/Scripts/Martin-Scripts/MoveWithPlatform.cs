using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlatform : MonoBehaviour {

    [SerializeField]
    private Vector3 velocity;

    bool moving;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            moving = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if(moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
