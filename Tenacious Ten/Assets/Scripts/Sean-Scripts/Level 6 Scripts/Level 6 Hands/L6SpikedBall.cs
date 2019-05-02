using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6SpikedBall : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            Debug.Log("HAUH");
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (col.gameObject.tag != "Ground")
        {
            Debug.Log(col.gameObject.tag);
        }
    }
}
