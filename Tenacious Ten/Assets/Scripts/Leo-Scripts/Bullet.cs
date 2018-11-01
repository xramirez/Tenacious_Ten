using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float Speed = 20f;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb.velocity = transform.right * Speed;
	}

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.name != "background")
        {
            //Debug.Log(hitInfo.name);
            Destroy(gameObject);
        }
    }
}
