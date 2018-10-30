using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyMove : MonoBehaviour {

	Vector2 mover = new Vector2(-1f, 0);
	public float moveSpeed = 5f;
	Vector2 startingPosition;
	[SerializeField]
	float distance;
    bool facing_right = true;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
        Flip();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		transform.Translate(mover * moveSpeed * Time.deltaTime);
        if (transform.position.x >= startingPosition.x + distance)
        {
            mover *= -1;
        }
        if (transform.position.x <= startingPosition.x -distance)
		{
			mover *= -1;
        }
        if (transform.position.x > startingPosition.x + distance  - .05)
        {
            Flip();
        }
        else
        if (transform.position.x < startingPosition.x - distance + .05)
        {
            Flip();
        }
    }
    void Flip(){
        transform.Rotate(0f, 180f, 0f);
    }

}
