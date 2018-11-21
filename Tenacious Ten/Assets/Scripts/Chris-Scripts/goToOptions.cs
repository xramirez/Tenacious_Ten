using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToOptions : MonoBehaviour
{
    // Use this for initialization
    public void loadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }
}
