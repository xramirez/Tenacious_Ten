using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLevelSelect : MonoBehaviour
{
    // Use this for initialization
    public void loadLevelSelectScene()
    {
        SceneManager.LoadScene("Level_Select");
    }
}
