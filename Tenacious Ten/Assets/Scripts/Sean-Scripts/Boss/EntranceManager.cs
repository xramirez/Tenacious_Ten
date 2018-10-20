using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour {


    public StartOrResetLevel SORL;

    Vector3 StartingPos;

    float change;

    // Use this for initialization
    void Start () {
        SORL = FindObjectOfType<StartOrResetLevel>();

        StartingPos = transform.localPosition;
        change = 0.04f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(SORL.StartFight)
        {
            if (transform.position.y >= 0.66f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - change, transform.position.z);
            }
        }
    }

    void Update()
    {
        if (SORL.ResetFight)
        {
            transform.localPosition = StartingPos;
        }
    }
}
