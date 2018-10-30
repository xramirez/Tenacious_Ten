using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalEnemyMove : MonoBehaviour {

	Vector2 mover = new Vector2(0, 1f);
	float moveSpeed = 5f;
	Vector2 startingPosition;
	[SerializeField]
	float distance;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		transform.Translate(mover * moveSpeed * Time.deltaTime);
		if(transform.position.y >= startingPosition.y+distance)
		{
			mover *= -1;
		}
		if (transform.position.y <= startingPosition.y)
		{
			mover *= -1;
		}
	}
}
