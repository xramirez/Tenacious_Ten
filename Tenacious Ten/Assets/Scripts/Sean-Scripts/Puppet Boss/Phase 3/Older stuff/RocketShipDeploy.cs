using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShipDeploy : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float VerticalDropSpeed;
    GameObject player;
    bool movingInXdir;
    bool facingLeft;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0f, -VerticalDropSpeed, 0f);
        player = GameObject.FindGameObjectWithTag("Player");

        movingInXdir = false;
        facingLeft = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(transform.position.y <= -3.95 && !movingInXdir)
        {
            movingInXdir = !movingInXdir;
            if(transform.position.x >= player.transform.position.x)
            {
                rb.velocity = new Vector3(-moveSpeed, 0f, 0f);
            }
            else
            {
                rb.velocity = new Vector3(moveSpeed, 0f, 0f);
                Vector3 temp = transform.localScale;
                temp.x *= -1;
                transform.localScale = temp;
            }
        }

	}
}
