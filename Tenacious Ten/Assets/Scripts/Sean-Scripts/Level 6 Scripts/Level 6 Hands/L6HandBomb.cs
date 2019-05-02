using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6HandBomb : MonoBehaviour
{
    [SerializeField] float moveUpSpeed;
    bool hasMovedUp;
    bool hasMovedToPlayerX;

    Rigidbody2D rb;

    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        hasMovedUp = false;
        hasMovedToPlayerX = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y <= 10.5 && !hasMovedUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveUpSpeed, transform.position.z);
        }
        else
        {
            hasMovedUp = true;
        }

        if(hasMovedUp)
        {
            rb.isKinematic = false;
            if(!hasMovedToPlayerX)
            {
                hasMovedToPlayerX = true;
                transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y, transform.position.z);
            }
        }
    
        if(transform.position.y <= -6.9)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
