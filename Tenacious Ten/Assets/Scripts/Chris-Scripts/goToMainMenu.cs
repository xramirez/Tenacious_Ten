using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToMainMenu : MonoBehaviour
{
    // Use this for initialization
    public void loadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
