using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMatcher : MonoBehaviour
{

	Vector2 TrackerPosition = new Vector2(0, 0);
	[SerializeField]
	GameObject player;
	float zed;

	// Use this for initialization
	void Start()
	{
		zed = transform.position.z;
	}

	void FixedUpdate()
	{
		TrackerPosition.x = player.transform.position.x;
		TrackerPosition.y = player.transform.position.y;
		transform.position = new Vector3(TrackerPosition.x,TrackerPosition.y,zed);
	}
}

