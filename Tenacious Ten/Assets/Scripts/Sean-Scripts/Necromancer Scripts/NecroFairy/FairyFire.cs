using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFire : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] bool NorthFire;
    [SerializeField] bool SouthFire;
    [SerializeField] bool WestFire;
    [SerializeField] bool EastFire;

    [SerializeField] float speed;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(NorthFire)
        {
            rb.velocity = new Vector3(0f, speed, 0f);
        }
        else if(WestFire)
        {
            rb.velocity = new Vector3(-speed, 0f, 0f);
        }
        else if(SouthFire)
        {
            rb.velocity = new Vector3(0f, -speed, 0f);
        }
        else if(EastFire)
        {
            rb.velocity = new Vector3(speed, 0f, 0f);
        }


	}
}
