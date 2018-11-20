using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

	bool needToFlipRight = false;
	bool needToFlipLeft = true;
	[SerializeField]
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate()
	{
		if(player.transform.position.x > transform.position.x && needToFlipLeft)
		{
			needToFlipRight = true;
			needToFlipLeft = false;
			Flip();
		}
		else if (player.transform.position.x < transform.position.x && needToFlipRight)
		{
			needToFlipRight = false;
			needToFlipLeft = true;
			Flip();
		}
	}

	void Flip()
	{
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}
}
