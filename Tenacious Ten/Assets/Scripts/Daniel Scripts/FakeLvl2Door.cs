using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLvl2Door : MonoBehaviour
{

	Vector2 take = new Vector2(0, 0);
	bool open;
	FlipLever checker;
	GameObject doorSprite;

	void Start()
	{
		take = GameObject.Find("FalseDoorLetOut").transform.position;
		checker = GameObject.FindObjectOfType<FlipLever>();
		doorSprite = GameObject.Find("Level2Door_0");
	}

	void FixedUpdate()
	{
		if(!checker.flipped)
		{
			GetComponent<Animator>().enabled = false;
			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().sprite = doorSprite.GetComponent<SpriteRenderer>().sprite;
		}
		else
		{
			GetComponent<Animator>().enabled = true;
			GetComponent<Collider2D>().enabled = true;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.transform.position = take;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.transform.position = take;
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.transform.position = take;
		}
	}
}
