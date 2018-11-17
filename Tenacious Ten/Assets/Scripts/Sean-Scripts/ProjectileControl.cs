using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

    public Vector2 speed;
    public float delay;

    public float timeToDestroy = 1f;
    

    public int damage;

    public GameObject impactEffect;
    private GameObject instantiatedObj;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;

        Destroy(gameObject, delay); //destroy the projectile/bullet/lemon after a specified delay time (in unity)
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = speed;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damage);
        }
        else if (other.tag == "Doll Enemy")
        {
            other.GetComponent<Phase1DollHealthManager>().giveDamage(damage);
        }
        else if (other.tag == "Hand Enemy")
        {
            other.GetComponent<Phase2HandsHealthManager>().giveDamage(damage);
        }
        //
        //Instantiate(impactEffect, transform.position, Quaternion.identity);

        instantiatedObj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiatedObj, 1);

        Destroy(gameObject);
    }
}
