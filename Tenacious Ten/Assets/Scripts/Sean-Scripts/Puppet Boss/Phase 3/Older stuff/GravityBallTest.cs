using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBallTest : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float RangeValueLeft;
    [SerializeField] float RangeValueRight;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0f, Random.Range(RangeValueLeft, RangeValueRight), 0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
