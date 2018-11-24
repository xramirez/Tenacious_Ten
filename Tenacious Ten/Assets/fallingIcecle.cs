using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingIcecle : MonoBehaviour {
    Rigidbody2D body;
    public float delay;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(0, -5);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Knight") || collision.gameObject.name.Equals("Player 2.0"))
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
        if (collision.gameObject.name.Equals("Ground")||collision.gameObject.name.Equals("Ice"))
        {
            //Debug.Log("Hit the ground");
            Destroy(gameObject);
        }
    }

}
