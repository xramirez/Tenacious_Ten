using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnOnSpace : MonoBehaviour {
    public GameObject HUD;

    void Awake()
    {
    }
	void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //if (SceneManager.GetActiveScene().name.Contains("Level"))
            //{
                HUD.GetComponent<Level1DeathMenu>().Respawn();
            //}
            //else if(SceneManager.GetActiveScene().name.Contains("Boss Fight"))
            //{
                //HUD.GetComponent<Level1DeathMenu>().Respawn();
            //}
        }
    }
}
