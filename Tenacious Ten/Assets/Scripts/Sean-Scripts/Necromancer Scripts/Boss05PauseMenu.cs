using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss05PauseMenu : MonoBehaviour
{
    LevelManager levelManager;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    Boss05Camera cameraB;
    StartOrResetLevel SORL;

    PlayerHealthManager PHM;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Resume();
        cameraB = FindObjectOfType<Boss05Camera>();
        SORL = FindObjectOfType<StartOrResetLevel>();

        PHM = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PHM.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        if (Checkpoint.UpdateHealth)
        {
            PlayerHealthManager.SetHP((int)SaveLoadCheckpoint.LoadLevelCheckPointData()[4]);
            Debug.Log("Continue Button Exception: Start with " + (int)SaveLoadCheckpoint.LoadLevelCheckPointData()[4] + " lives");
            //Change Update Health back to false
            Checkpoint.ChangeUpdateHealth();
            Debug.Log("Continue Button Exception: UpdateHealth is " + Checkpoint.UpdateHealth);
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitting game");
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        //SORL.StartFight = false;
        //SORL.ResetFight = true;
        levelManager.RespawnPlayer();
        cameraB.transform.localPosition = cameraB.StartingPos;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
}
