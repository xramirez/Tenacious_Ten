using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {
    public GameSession currentSession;


	// Use this for initialization
	void Start () {
        currentSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D something)
    {
        if(something.name == "Knight"){
            currentSession.currentCheckpoint = gameObject;
        }
    }
}
