using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeChanger : MonoBehaviour {
    public GameObject find;
    private Slider sliderVolume;

    void Start()
    {
        find = GameObject.Find("VolumeSlider");
        sliderVolume = find.GetComponent<Slider>();
    }
    public void change()
    {
        AudioListener.volume = sliderVolume.value;
    }
}
