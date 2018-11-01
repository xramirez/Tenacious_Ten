using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour {

    float speed = 7f;
    Rigidbody2D body;

    Knight_Move target;
    Vector2 moveDirection;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Knight_Move>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        body.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("Knight")){
            Debug.Log("Player Hit");
            Destroy(gameObject);
        }
        if(collision.name != "background" || collision.IsTouchingLayers(LayerMask.GetMask("ground"))){
            Debug.Log(collision.name);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
