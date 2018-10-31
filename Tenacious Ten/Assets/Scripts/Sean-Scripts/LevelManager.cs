using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentCheckpoint;
    private PlayerManager player;
    private CameraControl camera;

    public PlayerHealthManager healthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerManager>();

        camera = FindObjectOfType<CameraControl>();

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
        player.transform.position = currentCheckpoint.transform.position;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
