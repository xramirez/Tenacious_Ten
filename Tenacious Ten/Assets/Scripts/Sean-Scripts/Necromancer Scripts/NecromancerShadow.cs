using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerShadow : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float speed;

    [SerializeField] bool WalkLeft;
    [SerializeField] bool WalkRight;

    [SerializeField] float AnimTimer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        AnimTimer -= Time.deltaTime;
        if(AnimTimer <= 0)
        {
            anim.SetInteger("State", 1);
            if (WalkLeft)
            {
                rb.velocity = new Vector3(-speed, 0f, 0f);
            }

            if (WalkRight)
            {
                rb.velocity = new Vector3(speed, 0f, 0f);
            }
        }
	}
}
