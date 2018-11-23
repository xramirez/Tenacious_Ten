using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroSword : MonoBehaviour {

    [SerializeField] bool Phase1Sword;
    [SerializeField] float speed;
    public bool readyToGo;

    Rigidbody2D rb;

    SpriteRenderer sr;

    float PhaseInIncrement;

	// Use this for initialization
	void Start () {
        readyToGo = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        PhaseInIncrement = 0f;

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        
        sr.color = new Color(1f, 1f, 1f, PhaseInIncrement);
        PhaseInIncrement = PhaseInIncrement + 0.03f;
        if(PhaseInIncrement >= 1)
        {
            readyToGo = true;
        }

        if (Phase1Sword && readyToGo)
        {
            rb.velocity = transform.right * (-speed);
        }
    }
}
