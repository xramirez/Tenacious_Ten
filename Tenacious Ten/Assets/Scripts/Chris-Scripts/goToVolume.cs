using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToVolume : MonoBehaviour
{
    // Use this for initialization
    public void loadVolumeScene()
    {
        SceneManager.LoadScene("OptionsVolume");
    }
}
