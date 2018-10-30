using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera1 : MonoBehaviour {

    public StartOrResetLevel SORL;

    public Vector3 StartingPos;

    public float transitionSpeed;

    // Use this for initialization
    void Start () {
        SORL = FindObjectOfType<StartOrResetLevel>();

        StartingPos =  transform.localPosition;
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if(SORL.StartFight)
        {
            if(transform.position.x != 0)
            {
                transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z);
            }
        }

        if(SORL.ResetFight)
        {
            transform.localPosition = StartingPos;
        }
		
	}
}
