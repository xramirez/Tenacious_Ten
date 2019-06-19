using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMatcher : MonoBehaviour
{

	Vector2 TrackerPosition = new Vector2(0, 0);
	[SerializeField]
	GameObject player;
	float zed;
	[SerializeField]
	bool matchHorizontal = true;
	[SerializeField]
	bool matchVertical = true;
	float distanceY;
	float distanceX;
	[SerializeField]
	float moveSpeed;
	float mover;
	[SerializeField]
	float divider;

	// Use this for initialization
	void Start()
	{
		zed = transform.position.z;
	}

	void FixedUpdate()
	{
		if (matchHorizontal && matchVertical)
		{
			TrackerPosition.x = player.transform.position.x;
			TrackerPosition.y = player.transform.position.y;
			transform.position = new Vector3(TrackerPosition.x, TrackerPosition.y, zed);
		}
		else if(matchVertical)//not matching horizontal
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			mover = distanceX * moveSpeed / divider;
			transform.position += new Vector3(mover, distanceY, 0);
		}
		else if(matchHorizontal)//not matching vertical
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			mover = distanceX * moveSpeed / divider;
			transform.position += new Vector3(distanceX, mover, 0);
		}
	}
}

