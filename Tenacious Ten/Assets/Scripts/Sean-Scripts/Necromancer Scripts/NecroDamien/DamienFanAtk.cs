using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamienFanAtk : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float speed;

    [SerializeField] bool pointTopPlatform;
    [SerializeField] bool pointMidPlatform;
    [SerializeField] bool pointBotPlatform;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (pointTopPlatform)
        {
            rb.velocity = new Vector3(-speed, 3f, 0f);
        }
        else if (pointMidPlatform)
        {
            rb.velocity = new Vector3(-speed, 0f, 0f);
        }
        else if (pointBotPlatform)
        {
            rb.velocity = new Vector3(-speed, -3f, 0f);
        }
    }
}
