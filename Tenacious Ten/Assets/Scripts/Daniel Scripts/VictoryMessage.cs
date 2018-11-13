using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMessage : MonoBehaviour {

	public bool showmessage = false;
	EnemyHealthManager waiting;

	AudioSource sound;

	// Use this for initialization
	void Start () {
		showmessage = false;
		waiting = FindObjectOfType<EnemyHealthManager>();
		sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (showmessage)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            sound.Play();
            this.GetComponent<VictoryMessage>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        if (waiting.enemyHealth == 0)
        {
            showmessage = true;
        }
        else
        {
            showmessage = false;
        }
    }
}
