using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseFloor : MonoBehaviour
{
	bool collapse;
	FlipLever checker;

	// Start is called before the first frame update
	void Start()
	{
		collapse = false;
		checker = GameObject.FindObjectOfType<FlipLever>();
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if(checker.flipped)
		{
			GetComponent<Renderer>().enabled = false;
			GetComponent<Collider2D>().enabled = false;
		}
		if(checker.flipped == false)
		{
			GetComponent<Renderer>().enabled = true;
			GetComponent<Collider2D>().enabled = true;
		}
    }
}
