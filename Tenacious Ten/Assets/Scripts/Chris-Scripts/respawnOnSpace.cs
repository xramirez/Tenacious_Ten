using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnOnSpace : MonoBehaviour {
    public GameObject HUD;
    void Awake()
    {
        HUD = GameObject.FindGameObjectWithTag("HUD");
    }
	void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            HUD.GetComponent<Level1DeathMenu>().Respawn();
        }
    }
}
