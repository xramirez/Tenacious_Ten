using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerHealthManager.HurtPlayer(damage);
		}
	}
	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerHealthManager.HurtPlayer(damage);
		}
	}
}
