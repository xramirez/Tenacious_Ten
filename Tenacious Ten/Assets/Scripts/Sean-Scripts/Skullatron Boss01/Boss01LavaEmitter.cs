using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01LavaEmitter : MonoBehaviour {

    [SerializeField] float EmitTime;
    [SerializeField] GameObject Lava;
    float initEmitTime;

	// Use this for initialization
	void Start () {
        initEmitTime = EmitTime;
	}
	
	// Update is called once per frame
	void Update () {

        EmitTime -= Time.deltaTime;
        if(EmitTime <= 0)
        {
            EmitTime = initEmitTime;
            Instantiate(Lava, transform.position, Quaternion.Euler(0f,0f,90f));
        }

	}
}
