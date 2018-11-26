using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {

    public LevelManager levelManager;
    public float[] checkPointPos;
    public GameObject SceneSwitch1;
    public static bool SceneSwitch;
    public static bool UpdateHealth = false;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>(); //finds any object in the scene that has level manager in it
        SceneSwitch = false;
    }

    void Awake()
    {
        checkPointPos = new float[5];
        if (SceneSwitch)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            checkPointPos = SaveLoadCheckpoint.LoadLevelCheckPointData();
            Player.transform.position = new Vector3(checkPointPos[0], checkPointPos[1]+3, checkPointPos[2]);
            SceneSwitch = false;
            UpdateHealth = true;
            Debug.Log("UpdateHealth is " + UpdateHealth);
        }

    }

    public static void ChangeUpdateHealth() { UpdateHealth = false; }
    
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
        int SceneNum;
        string SceneName;

        //Level 2.something cases
        if(checkPointPos[3] > 2 && checkPointPos[3] < 3){
            
            SceneName = "Level_" + checkPointPos[3].ToString();
            Debug.Log("Scenename = \"" + SceneName + "\"");
        }
        else{
            SceneNum = (int)checkPointPos[3];
            SceneName = "Level_" + SceneNum + ".0";
        }
        Scene Dest = SceneManager.GetSceneByName(SceneName);
        SceneSwitch = true;
        SceneManager.LoadScene(SceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.currentCheckpoint = gameObject;
            checkPointPos[0] = levelManager.currentCheckpoint.transform.position.x;
            checkPointPos[1] = levelManager.currentCheckpoint.transform.position.y;
            checkPointPos[2] = levelManager.currentCheckpoint.transform.position.z;
            string SceneNum = SceneManager.GetActiveScene().name.Remove(0, 6);
            if(float.Parse(SceneNum) > 2 && float.Parse(SceneNum) < 3){;} // Dont do nothin'
            else{SceneNum = SceneNum.Remove(1,2);} // Do sumtin'
            Debug.Log("Current SceneNum is: " + SceneNum);
            checkPointPos[3] = float.Parse(SceneNum);
            checkPointPos[4] = PlayerHealthManager.playerHealth;
            Debug.Log("Checkpoint Script: HP when at checkpoint " + PlayerHealthManager.playerHealth);
            Save();
            Debug.Log("Checkpoint Saved at pos(" + checkPointPos[0] + ", " + checkPointPos[1] + ", " + checkPointPos[0] + ").");
        }
    }
}
