using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooperDoorLvl2 : MonoBehaviour
{
	Vector2 take = new Vector2(0, 0);

	void Start()
	{
		take = GameObject.Find("StartDoor").transform.position;
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
