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
            if(sceneLevel+1 > loadLevel)
            {
                Debug.Log("Saving current level to be to: " + loadLevel);
                SaveFile saveMe = new SaveFile();
                if(sceneLevel != 5)
                {
                    saveMe.currentLevel = loadLevel + 1;
                }
                else if(sceneLevel == 5)
                {
                    saveMe.currentLevel = 5;
                }
                saveMe.currentVolume = SaveLoadManager.LoadVolumeData();
                SaveLoadManager.SaveLevelData(saveMe);
            }

			//Go to Different if cases align
			//Go to Level Select Screen in 3 seconds
			if(sceneLevel == 3)
			{
				Debug.Log("Moving to level 3->4 transition...");
				Invoke("transition34", 3);
			}
			else if(sceneLevel == 4)
			{
				Debug.Log("Moving to level 4->5 transition..");
				Invoke("transition45", 3);
			}
            else if(sceneLevel == 5)
            {
                Debug.Log("Moving to level Credits transition..");
                Invoke("Credits", 3);
            }
			else
			{
				Debug.Log("Moving to level select screen...");
				Invoke("levelSelect", 3);
			}
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = -100;
        }
        if (GameObject.FindObjectOfType<EnemyHealthManager>() != null)
        {
            waiting = GameObject.FindObjectOfType<EnemyHealthManager>();
        }
        if(GameObject.FindObjectOfType<NecromancerPhase6>() != null)
        {
            waiting = GameObject.FindObjectOfType<NecromancerPhase6>().GetComponent<EnemyHealthManager>();
        }
        if (waiting.enemyHealth <= 0)
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
    void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    void transition34()
	{
		SceneManager.LoadScene("Lvl3 to 4 transition");
	}
	void transition45()
	{
		SceneManager.LoadScene("Lvl4 to 5 transition");
	}
}
