using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardForPlayer : MonoBehaviour {

	public int damage;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerHealthManager.HurtPlayer(damage);
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			PlayerHealthManager.HurtPlayer(damage);
		}
	}
}
