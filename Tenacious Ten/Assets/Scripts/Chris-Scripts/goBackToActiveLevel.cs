using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class goBackToActiveLevel : MonoBehaviour {
    public GameObject Background;
    public GameObject OptionsText;
    public GameObject VolumeText;
    public Slider VolumeSlider;
    public Button Buttons;

    void Start()
    {
        Camera camera = GameObject.FindObjectOfType<Camera>();
        Background.transform.position = camera.transform.position + new Vector3(0,0,5);
        OptionsText.transform.position = camera.transform.position + new Vector3(0, 0, 5);
        VolumeText.transform.position = camera.transform.position + new Vector3(0, 0, 5);
        VolumeSlider.transform.position = camera.transform.position + new Vector3(0, 0, 5);
        Buttons.transform.position = camera.transform.position + new Vector3(0, 0, 5);
    }
    public void ReturnToGame()
    {
        goToOptionsInGame.Leave = true;
        SceneManager.UnloadScene(SceneManager.GetActiveScene());
    }
}
