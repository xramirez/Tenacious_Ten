using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseCeiling : MonoBehaviour
{
	bool collapse;
	FlipLever checker;
	public Vector2 startPosition = new Vector2(0, 0);
	public float downSpeed = -0.05f;
	public float upSpeed = 0.5f;
	CollapseCeiling consistence;
	AudioSource scraping;
	bool playScrape = true;

    // Start is called before the first frame update
    void Start()
    {
		collapse = false;
		checker = GameObject.FindObjectOfType<FlipLever>();
		startPosition = transform.position;
		scraping = GetComponent<AudioSource>();
		if(name == "Hazards")
		{
			consistence = GameObject.FindObjectOfType<CollapseCeiling>();
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(checker.flipped == true)
		{
			if (playScrape == true)
			{
				scraping.Play();
				playScrape = false;
			}
			transform.Translate(0, downSpeed, 0);
			if (startPosition.y - transform.position.y > 25)
				checker.flipped = false;
		}
		else
		{
			if (startPosition.y > transform.position.y)
			{
				transform.Translate(0, upSpeed, 0);
				playScrape = false;
				scraping.Stop();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			checker.flipped = false;
			transform.position = startPosition;
			scraping.Stop();
			playScrape = true;
			if (name == "Hazards")
			{
				consistence.transform.position = consistence.startPosition;
				consistence.GetComponent<AudioSource>().Stop();
			}
		}
	}
}
