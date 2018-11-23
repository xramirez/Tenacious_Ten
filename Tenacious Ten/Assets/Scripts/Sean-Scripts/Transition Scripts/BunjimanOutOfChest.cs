using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunjimanOutOfChest : MonoBehaviour {

    [SerializeField] float GrowSpeed;
    Animator anim;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 700f));
        anim = GetComponent<Animator>();
        anim.SetInteger("State", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(transform.localScale.x <= 10f)
        {
            transform.localScale = new Vector3(transform.localScale.x + GrowSpeed, transform.localScale.y + GrowSpeed, 0f);
        }
	}
}
