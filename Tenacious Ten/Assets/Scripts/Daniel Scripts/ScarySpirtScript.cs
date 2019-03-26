using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySpirtScript : MonoBehaviour
{
	Vector2 startPosition = new Vector2(0, 0);
	TellGirlGo signal;
	GameObject EvilSprite, GoodSprite, Player;
	[SerializeField]
	float HoriMoveSpeed;

	// Start is called before the first frame update
	void Start()
    {
		startPosition = transform.position;
		signal = GameObject.FindObjectOfType<TellGirlGo>();
		EvilSprite = GameObject.Find("ScarySpirit_Reference");
		GoodSprite = GameObject.Find("spirit_0");
		Player = GameObject.FindGameObjectWithTag("Player");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (signal.passed)
		{
			GetComponent<VerticalEnemyMove>().enabled = false;
			GetComponent<SpriteRenderer>().sprite = EvilSprite.GetComponent<SpriteRenderer>().sprite;
			GetComponent<Collider2D>().enabled = true;
			GetComponent<VerticalTracker>().enabled = true;
			if (Player.transform.position.x >= transform.position.x)
				transform.Translate(HoriMoveSpeed, 0, 0);
			else
				transform.Translate(-HoriMoveSpeed, 0, 0);
		}
		else
			resetHer();
    }

	void Chasescene()
	{
		if (!signal.passed)
		{
			resetHer();
		}
	}

	void resetHer()
	{
		GetComponent<SpriteRenderer>().sprite = GoodSprite.GetComponent<SpriteRenderer>().sprite;
		GetComponent<VerticalEnemyMove>().enabled = true;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<VerticalTracker>().enabled = false;
		transform.position = startPosition;
	}
}
