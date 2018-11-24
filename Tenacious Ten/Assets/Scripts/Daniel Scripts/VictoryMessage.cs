using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

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

    public int Load_FromSaveLoad()
    {
        return SaveLoadManager.LoadLevelData();
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (showmessage)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 100;
            sound.Play();
            this.GetComponent<VictoryMessage>().enabled = false;

            //
            //  Chris' Script to make Save Files and Level Select transitions work
            //

            //Get currentScene name to find which level we're at
            Scene currentScene = SceneManager.GetActiveScene();
            int sceneLevel = Int32.Parse(currentScene.name.Remove(0,11));
            Debug.Log("Found that scene Level is: " + sceneLevel);
            int loadLevel = Load_FromSaveLoad();

            //Compare sceneLevel to loadLevel
            if(sceneLevel+1 > loadLevel && loadLevel != 5)
            {
                Debug.Log("Saving current level to be to: " + loadLevel);
                SaveFile saveMe = new SaveFile();
                saveMe.currentLevel = loadLevel + 1;
                SaveLoadManager.SaveLevelData(saveMe);
            }

            //Go to Level Select Screen in 3 seconds
            Debug.Log("Moving to level select screen...");
            Invoke("levelSelect",3);
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
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
    void levelSelect()
    {
        SceneManager.LoadScene("Level_Select");
    }
}
