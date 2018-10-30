using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInACircle : MonoBehaviour {
	[SerializeField]
	float moveSpeed = 2f;
	Vector2 startingPosition;
	[SerializeField]
	float radius = 3f;
	float angle = 0;
	Vector2 offset;
	[SerializeField]
	bool reversed = false;

	// Use this for initialization
	void Start()
	{
		startingPosition = transform.position;
	}

	void FixedUpdate()
	{
		if (!reversed)
		{
			angle += moveSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
		}
		if (reversed)
		{
			angle -= moveSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
		}
		offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
		transform.position = startingPosition + offset;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "EnemyBounceWall" && !reversed)
		{
			reversed = true;
		}
		else if(collision.gameObject.tag == "EnemyBounceWall" && reversed)
		{ 
			reversed = false;
		}

	}
}
