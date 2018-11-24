using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if(Load_FromSaveLoad() < 1)
            {
                SaveFile saveMe = new SaveFile();
                saveMe.currentLevel = 2;
                SaveLoadManager.SaveLevelData(saveMe);
            }
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
