using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyDamageTeller : MonoBehaviour
{
	[SerializeField]
	GameObject backgroundObject;

	// Start is called before the first frame update
	void Start()
	{
		//backgroundObject.GetComponent<ChariotSky>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name == "rightProjectile(Clone)")
		{
			backgroundObject.GetComponent<ChariotSky>().TotalDamage += 1;
		}
	}
}
	
