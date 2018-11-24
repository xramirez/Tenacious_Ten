using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;
    public float[] checkPointPos;
    public GameObject SceneSwitch1;
    public static bool SceneSwitch;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); //finds any object in the scene that has level manager in it
        SceneSwitch = false;
    }

    void Awake()
    {
        checkPointPos = new float[4];
        if (SceneSwitch)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            checkPointPos = SaveLoadCheckpoint.LoadLevelCheckPointData();
            Player.transform.position = new Vector3(checkPointPos[0], checkPointPos[1], checkPointPos[2]);
            SceneSwitch = false;
        }

    }

    // Update is called once per frame
    void Update () {
	}

    

    public void Save()
    {
        SaveLoadCheckpoint.SaveLevelCheckPointData(this);
    }
    public void Load()
    {
        checkPointPos = SaveLoadCheckpoint.LoadLevelCheckPointData();
    }


    public void LoadScene()
    {
        checkPointPos = SaveLoadCheckpoint.LoadLevelCheckPointData();
        int SceneNum = (int)checkPointPos[3];
        string SceneName = "Level_" + SceneNum + ".0";
        Scene Dest = SceneManager.GetSceneByName(SceneName);
        SceneSwitch = true;
        SceneManager.LoadScene(SceneName);
    }
    /*
    public void DeleteSave()
    {
        SaveLoadCheckpoint.DeleteLevelCheckPointData();
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.currentCheckpoint = gameObject;
            checkPointPos[0] = levelManager.currentCheckpoint.transform.position.x;
            checkPointPos[1] = levelManager.currentCheckpoint.transform.position.y;
            checkPointPos[2] = levelManager.currentCheckpoint.transform.position.z;
            string SceneNum = SceneManager.GetActiveScene().name.Remove(0, 6);
            SceneNum = SceneNum.Remove(1,2);
            Debug.Log("Current SceneNum is: " + SceneNum);
            checkPointPos[3] = float.Parse(SceneNum);
            Save();
            Debug.Log("Checkpoint Saved at pos(" + checkPointPos[0] + ", " + checkPointPos[1] + ", " + checkPointPos[0] + ").");
        }
    }
}
