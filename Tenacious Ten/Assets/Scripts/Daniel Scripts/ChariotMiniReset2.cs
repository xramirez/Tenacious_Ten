using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotMiniReset2 : MonoBehaviour
{
	Vector3 StartingPos;
	Vector3 StartingScale;
	public bool facingLeft;
	public Animator anim;
	Rigidbody2D rb;
	bool isIdle;
	GameObject player;
	float distanceBetween;
	Vector2 TrackerPosition = new Vector2(0, 0);
	L6HandsManager EHM;
	StartOrResetLevel SORL;
	public ChariotSky pushBack;
	public float damageBenchmark;

	// Start is called before the first frame update
	void Start()
	{
		StartingPos = transform.localPosition;
		StartingScale = transform.localScale;
		//anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		isIdle = true;
		player = GameObject.FindGameObjectWithTag("Player");
		EHM = GetComponent<L6HandsManager>();
		SORL = FindObjectOfType<StartOrResetLevel>();
		pushBack = FindObjectOfType<ChariotSky>();
		damageBenchmark = pushBack.TotalDamage;
	}

	// Update is called once per frame
	void Update()
	{
		if (SORL.ResetFight == true && EHM.moveToStart == true)
		{
			ResetFight();
		}
	}

	void ResetFight()
	{
		transform.position = StartingPos;
		transform.localScale = StartingScale;

		isIdle = true;

		//anim.SetInteger("State", 0);
		pushBack.TotalDamage = damageBenchmark;
		EHM.enemyHealth = EHM.maxHealth;
		EHM.moveToStart = false;
		SORL.ResetFight = false;
	}
}
