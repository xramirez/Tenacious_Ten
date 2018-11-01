using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyMove : MonoBehaviour {

	Vector2 mover = new Vector2(-1f, 0);
	[SerializeField]
	float moveSpeed = 5f;
	Vector2 startingPosition;
	[SerializeField]
	float distance;
	[SerializeField]
	bool needToFlip = false;

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
		if (transform.position.x <= startingPosition.x -distance)
		{
			mover *= -1;
			if (needToFlip)
				Flip();
		}
		if (transform.position.x >= startingPosition.x)
		{
			mover *= -1;
			if (needToFlip)
				Flip();
		}
	}

	void Flip()
	{
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
}
