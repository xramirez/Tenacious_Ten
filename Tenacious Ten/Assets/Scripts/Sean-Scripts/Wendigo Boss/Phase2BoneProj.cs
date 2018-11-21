using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2BoneProj : MonoBehaviour {

    public static float speedX;
    public float speedY;

    public float rotationSpeed;
    public Rigidbody2D rb;
    StartOrResetLevel SORL;

    public GameObject spike;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(rb.velocity.x, speedY));
        SORL = FindObjectOfType<StartOrResetLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(speedX, rb.velocity.y, 0);
        

        rb.angularVelocity = rotationSpeed;

        Object.Destroy(gameObject, 5f);
        if(SORL.ResetFight == true)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
            Instantiate(spike, transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        int randomizer = Random.Range(0, 2);
        if(randomizer == 1)
        {
            if (other.tag == "Upper Ground")
            {
                Destroy(gameObject);
                Instantiate(spike, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }
        }
    }

    public static void changeSpeedX(float newSpeedX)
    {
        //rb.velocity = new Vector3(newSpeedX, rb.velocity.y, 0);
        speedX = newSpeedX;
    }


}
