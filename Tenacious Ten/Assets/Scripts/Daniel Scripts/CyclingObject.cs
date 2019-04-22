using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclingObject : MonoBehaviour
{
	[SerializeField]
	GameObject refreshStart;//the point it goes back to
	[SerializeField]
	GameObject refreshEnd;//the point that's the end of the cycle
	[SerializeField]
	float moveSpeed = 5f;
	Vector2 mover = new Vector2(-1f, 0);

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		transform.Translate(mover * moveSpeed * Time.deltaTime);
		if (transform.position.x <= refreshEnd.transform.position.x)
		{
			transform.position = new Vector2(refreshStart.transform.position.x,transform.position.y);
		}
	}
}
