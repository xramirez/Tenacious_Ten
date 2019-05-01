using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private GameObject player;

    public PlayerHealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");

        healthManager = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        healthManager.FullHealth();
        healthManager.isDead = false;
        player.transform.position = currentCheckpoint.transform.position + new Vector3(0,1,0);
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
