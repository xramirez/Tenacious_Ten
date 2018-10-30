using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTracker : MonoBehaviour {

	[SerializeField]
	float moveSpeed;
	
	Vector2 TrackerPosition = new Vector2(0, 0);
	[SerializeField]
	GameObject player;
	float distanceBetween;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		distanceBetween = player.transform.position.x - transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		TrackerPosition.y = 0;
		distanceBetween = player.transform.position.x - transform.position.x;
		TrackerPosition.x = distanceBetween;
		transform.Translate(TrackerPosition * moveSpeed * Time.deltaTime);
	}
}
