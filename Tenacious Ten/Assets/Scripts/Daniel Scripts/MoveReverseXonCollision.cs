using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReverseXonCollision : MonoBehaviour {

	[SerializeField]
	Vector2 mover = new Vector2(-1f, 0);
	float moveSpeed = 5f;
	Vector2 startingPosition;

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
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "EnemyBounceWall")
			mover *= -1;
	}
}
