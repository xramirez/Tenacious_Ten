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
		print(other.name);
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damage);
            ScoreManager.Instance.ShotsLanded++;
        }
		//
		//Instantiate(impactEffect, transform.position, Quaternion.identity);
		//if (other.name != "background")
		//{
		//	instantiatedObj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		//	Destroy(instantiatedObj, 1);

		//	Destroy(gameObject);
		//}
        else if (other.tag == "Doll Enemy")
        {
            other.GetComponent<Phase1DollHealthManager>().giveDamage(damage);
            ScoreManager.Instance.ShotsLanded++;
        }
        else if (other.tag == "Hand Enemy")
        {
            other.GetComponent<Phase2HandsHealthManager>().giveDamage(damage);
            ScoreManager.Instance.ShotsLanded++;
        }
        else if (other.tag == "Level 6 Enemy")
        {
            if(other.GetComponent<L6BootsManager>()!=null)
            {
                other.GetComponent<L6BootsManager>().giveDamage(damage);
            }
            else if (other.GetComponent<L6HandsManager>() != null)
            {
                other.GetComponent<L6HandsManager>().giveDamage(damage);
            }
            else if (other.GetComponent<L6TorsoManager>() != null)
            {
                other.GetComponent<L6TorsoManager>().giveDamage(damage);
            }
            else if (other.GetComponent<L6HelmetManager>() != null)
            {
                other.GetComponent<L6HelmetManager>().giveDamage(damage);
            }
            ScoreManager.Instance.ShotsLanded++;
        }
        //
        //Instantiate(impactEffect, transform.position, Quaternion.identity);

        instantiatedObj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiatedObj, 1);

        Destroy(gameObject);
    }
}
