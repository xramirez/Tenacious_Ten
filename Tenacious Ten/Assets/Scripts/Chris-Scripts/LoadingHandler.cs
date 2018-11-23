using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour {
    public GameObject LoadingImage;
    public Text LoadingText;
    public GameObject World1;
    public GameObject World2;
    public GameObject World3;
    public GameObject World4;
    public GameObject World5;

    // Use this for initialization
    void Start () {
        LoadingImage.SetActive(false);
    }
	
    public void LoadingALevel()
    {
        LoadingImage.SetActive(true);
        World1.SetActive(false);
        World2.SetActive(false);
        World3.SetActive(false);
        World4.SetActive(false);
        World5.SetActive(false);
    }
}
