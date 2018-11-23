using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalTracker : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	Vector2 TrackerPosition = new Vector2(0, 0);
	[SerializeField]
	GameObject player;
	float distanceBetween;

	// Use this for initialization
	void Start()
	{
		//player = GameObject.FindGameObjectWithTag("Player");
		distanceBetween = player.transform.position.y - transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void FixedUpdate()
	{
		TrackerPosition.x = 0;
		distanceBetween = player.transform.position.y - transform.position.y;
		TrackerPosition.y = distanceBetween;
		transform.Translate(TrackerPosition * moveSpeed * Time.deltaTime);
	}
}
