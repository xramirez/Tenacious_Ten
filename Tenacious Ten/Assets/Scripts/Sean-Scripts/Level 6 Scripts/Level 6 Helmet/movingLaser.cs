using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingLaser : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool isMovingLeft;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isMovingLeft)
        {
            speed = -speed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);
    }
}
