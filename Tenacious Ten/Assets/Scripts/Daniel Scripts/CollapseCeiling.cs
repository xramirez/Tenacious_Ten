using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseCeiling : MonoBehaviour
{
	bool collapse;
	FlipLever checker;
	Vector2 startPosition = new Vector2(0, 0);
	public float downSpeed = -0.05f;
	public float upSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
		collapse = false;
		checker = GameObject.FindObjectOfType<FlipLever>();
		startPosition = transform.position;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(checker.flipped == true)
		{
			transform.Translate(0, downSpeed, 0);
			if (startPosition.y - transform.position.y > 25)
				checker.flipped = false;
		}
		else
		{
			if (startPosition.y > transform.position.y)
			{
				transform.Translate(0, upSpeed, 0);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			checker.flipped = false;
			transform.position = startPosition;
		}
	}
}
