using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public static bool GameIsPaused = false;
    public Canvas MyCanvas;

    public GameObject PauseMenuUI;

    int counter = 0;
	// Update is called once per frame
	void Update () {
        if(counter == 0){
            MyCanvas.enabled = false;
            counter++;
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}
    public void DisableCanvas()
    {
        MyCanvas.enabled = false;
    }
    public void EnableCanvas()
    {
        MyCanvas.enabled = true;
    }
    public void Resume(){
        DisableCanvas();
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause(){
        EnableCanvas();
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu(){
        Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame(){
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
