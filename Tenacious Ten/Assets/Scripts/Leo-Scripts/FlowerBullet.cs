using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour {

    float speed = 7f;
    Rigidbody2D body;

	public float delay;

	//Knight_Move target;
	PlayerManager target;
    Vector2 moveDirection;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        //target = FindObjectOfType<Knight_Move>();
        target = FindObjectOfType<PlayerManager>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        body.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, delay);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Knight")|| collision.gameObject.name.Equals("Player 2.0"))
        {
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
        if(collision.gameObject.name.Equals("ground")){
            //Debug.Log("Hit the ground");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
