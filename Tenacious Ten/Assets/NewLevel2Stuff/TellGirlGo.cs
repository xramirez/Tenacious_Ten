using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellGirlGo : MonoBehaviour
{
	public bool passed;
	GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
		passed = false;
		Player = GameObject.FindGameObjectWithTag("Player");
    }

	void FixedUpdate()
	{
		if (Player.transform.position.x > transform.position.x && Player.transform.position.y < transform.position.y)
		{
			passed = true;
		}
		else
			passed = false;
	}
}
