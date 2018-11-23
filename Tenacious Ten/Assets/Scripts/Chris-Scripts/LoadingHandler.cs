using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour {
    public GameObject LoadingImage;
    public Text LoadingText;

    // Use this for initialization
    void Start () {
        LoadingImage.SetActive(false);
    }
	
    public void LoadingALevel()
    {
        LoadingImage.SetActive(true);
    }
}
