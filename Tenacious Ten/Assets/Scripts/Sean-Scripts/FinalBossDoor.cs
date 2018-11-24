using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDoor : MonoBehaviour {

    StartOrResetLevel SORL;
    Animator anim;
    BoxCollider2D bc;


	// Use this for initialization
	void Start () {
        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
        if(SORL.StartFight)
        {
            anim.enabled = true;
            StartCoroutine(waitForTrigger(1.75f));
        }
	}

    IEnumerator waitForTrigger(float time)
    {
        yield return new WaitForSeconds(time);
        bc.enabled = true;
    }
}
