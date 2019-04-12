using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingLevel : MonoBehaviour
{
	[SerializeField]
	GameObject refreshStart;//the point it goes back to
	[SerializeField]
	GameObject refreshEnd;//the point that's the end of the cycle
	[SerializeField]
	float moveSpeed = 5f;
	Vector2 mover = new Vector2(-1f, 0);
	Vector2 startingPosition;
	float distanceBetween;


	// Start is called before the first frame update
	void Start()
    {
		startingPosition = transform.position;
		distanceBetween = refreshEnd.transform.position.x - refreshStart.transform.position.x;
		if (distanceBetween < 0)
			distanceBetween *= -1;
	}

    // Update is called once per frame
    void FixedUpdate()
    {

		transform.Translate(mover * moveSpeed * Time.deltaTime);
		if(transform.position.x <= startingPosition.x - distanceBetween)
		{
			transform.position = startingPosition;
		}
	}
}
