using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshFallEmitter : MonoBehaviour {

    [SerializeField] float RandomTimeL;
    [SerializeField] float RandomTimeR;
    [SerializeField] GameObject AshFall;
    [SerializeField] public bool ashFlipInAir;
    bool emitTimeSet;
    float emitTimer;

	// Use this for initialization
	void Start () {
        emitTimeSet = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.position = new Vector3(Random.Range(-10.4f, 10.4f), transform.position.y, 0f);
        //transform.position = new Vector3(transform.position.x, Random.Range(-10.4f, 10.4f), 0f);

        if (!emitTimeSet)
        {
            emitTimeSet = true;
            emitTimer = Random.Range(RandomTimeL, RandomTimeR);
        }
        emitTimer -= Time.deltaTime;
        if(emitTimer <= 0)
        {
            emitTimeSet = false;
            Instantiate(AshFall, transform.position, Quaternion.identity);
        }

	}
}
