using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBullet : MonoBehaviour {

	float speed = 7f;
	public float bulletSpeed;
    Rigidbody2D body;

	public float delay;

	Vector2 moveDirection;
	PlayerManager target;

	Vector2 adjustment = new Vector2(0, -0.1f);

	// Use this for initialization
	void Start () {
		transform.Translate(adjustment);
		body = GetComponent<Rigidbody2D>();
		//target = FindObjectOfType<Knight_Move>();
		target = FindObjectOfType<PlayerManager>();
		moveDirection = (target.transform.position - transform.position).normalized * speed;
		if(moveDirection.x < 0)
		{
			bulletSpeed *= -1;
		}
		body.velocity = new Vector2(bulletSpeed, 0);
		Destroy(gameObject, delay);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name.Equals("Knight") || collision.gameObject.name.Equals("Player 2.0"))
		{
			Debug.Log("Player Hit");
			Destroy(gameObject);
		}
		if (collision.gameObject.name.Equals("ground"))
		{
			//Debug.Log("Hit the ground");
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
