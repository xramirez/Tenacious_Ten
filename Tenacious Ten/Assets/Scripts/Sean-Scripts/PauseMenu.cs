using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    LevelManager levelManager;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    BossCamera cameraB;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        Resume();
        cameraB = FindObjectOfType<BossCamera>();
    }

    // Update is called once per frame
    void Update()
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

    public void Resume()
    {
        Debug.Log("Resuming game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
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
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void Restart()
    {
        //SORL.StartFight = false;
        //SORL.ResetFight = true;
        levelManager.RespawnPlayer();
        //cameraB.transform.localPosition = cameraB.StartingPos;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
}
