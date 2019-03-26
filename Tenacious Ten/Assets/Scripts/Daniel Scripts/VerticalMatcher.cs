using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMatcher : MonoBehaviour
{

	Vector2 TrackerPosition = new Vector2(0, 0);
	[SerializeField]
	GameObject player;
	float distanceBetween;

	// Use this for initialization
	void Start()
	{
		//player = GameObject.FindGameObjectWithTag("Player");
		//distanceBetween = player.transform.position.y - transform.position.y;
	}

	void FixedUpdate()
	{
		TrackerPosition.x = transform.position.x;
		TrackerPosition.y = player.transform.position.y;
		transform.position = TrackerPosition;
	}
}
