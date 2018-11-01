using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMove : MonoBehaviour {

	Vector2 mover = new Vector2(-1f, -1f);
	[SerializeField]
	float moveSpeed = 5f;
	Vector2 startingPosition;
	[SerializeField]
	float ydistance;
	[SerializeField]
	float xdistance;

	// Use this for initialization
	void Start()
	{
		startingPosition = transform.position;
	}

	void FixedUpdate()
	{
		transform.Translate(mover * moveSpeed * Time.deltaTime);
		if (transform.position.y >= startingPosition.y + ydistance)
		{
			mover.y *= -1;
		}
		if (transform.position.y <= startingPosition.y - ydistance)
		{
			mover.y *= -1;
		}
		if (transform.position.x >= startingPosition.x + xdistance)
		{
			mover.x *= -1;
		}
		if (transform.position.x <= startingPosition.x - xdistance)
		{
			mover.x *= -1;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "EnemyBounceWall")
		{
			moveSpeed *= -1;
		}
	}
}
