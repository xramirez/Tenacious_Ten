using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalSkullMove : MonoBehaviour {
	[SerializeField]
	float moveSpeed = 2f;
	Vector2 startingPosition;
	[SerializeField]
	float radius = 3f;
	float angle = 0;
	Vector2 offset;
	[SerializeField]
	bool reversed = false;
    Vector3 temp;
    Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		startingPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
    }

	void FixedUpdate()
	{

        //rb.velocity = new Vector2(-4, rb.velocity.y);
        if (!reversed)
		{
			angle += moveSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
		}
		if (reversed)
		{
			angle -= moveSpeed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
		}
		offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
		//transform.position = startingPosition + offset;

        rb.velocity = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f) * radius;
        rb.velocity = new Vector3(-2f, rb.velocity.y, 0f) * radius;


        //transform.position = startingPosition + offset;
        //temp = transform.position;
        //temp.x -= 3f;//temp.x -= 0.03f;
        //transform.position = temp;
        //Debug.Log("HAUH");
        //Debug.Log(transform.position.x);
    }

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "EnemyBounceWall" && !reversed)
		{
			reversed = true;
		}
		else if(collision.gameObject.tag == "EnemyBounceWall" && reversed)
		{ 
			reversed = false;
		}

	}
}
