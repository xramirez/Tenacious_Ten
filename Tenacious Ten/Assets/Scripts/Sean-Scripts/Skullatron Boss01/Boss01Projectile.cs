using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Projectile : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float delay;

    public float rotationSpeed;

    public int damage;

    Rigidbody2D rb;

    StartOrResetLevel SORL;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speedX, speedY, 0);
        SORL = FindObjectOfType<StartOrResetLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speedX, speedY, 0);

        rb.angularVelocity = rotationSpeed;

        Object.Destroy(gameObject, 5f);

        if(SORL.ResetFight)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
       {
           PlayerHealthManager.HurtPlayer(damage);
       }
        
        //instantiatedObj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(instantiatedObj, 1);
    }
    
}
