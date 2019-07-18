using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToControls : MonoBehaviour
{
    // Use this for initialization
    public void loadControlsScene()
    {
        SceneManager.LoadScene("OptionsControls");
    }
}