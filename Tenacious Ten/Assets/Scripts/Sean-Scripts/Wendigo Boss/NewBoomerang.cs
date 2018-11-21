using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoomerang : MonoBehaviour {

    Rigidbody2D rb;
    float alpha;
    public float RotationSpeed;
    public float MoveSpeed;
    public float Offset;

    StartOrResetLevel SORL;

     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SORL = FindObjectOfType<StartOrResetLevel>();
    }

    void FixedUpdate()
    {
        transform.position = new Vector2((10f * Mathf.Sin(Mathf.Deg2Rad * alpha)), Offset + (1.5f * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        alpha += MoveSpeed;//can be used as speed

        rb.angularVelocity = RotationSpeed;

        

        
    }
    void Update()
    {
        if (SORL.ResetFight == true)
        {
            Destroy(gameObject);
        }

    }
}