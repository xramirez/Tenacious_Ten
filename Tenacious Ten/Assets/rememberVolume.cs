using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rememberVolume : MonoBehaviour {
    public GameObject find;
    private Slider sliderVolume;
    public GameObject saveGameObject;
    public SaveFile save;

    // Use this for initialization
    void Start () {
        find = GameObject.Find("VolumeSlider");
        sliderVolume = find.GetComponent<Slider>();
        sliderVolume.value = SaveLoadManager.LoadVolumeData();
        AudioListener.volume = sliderVolume.value;

        save.VolumeLevelSave(SaveLoadManager.LoadLevelData());
        save.ChangeVolume(sliderVolume.value);
        save.Save();
        sliderVolume.onValueChanged.AddListener(ListenerMethod);
    }
 
    public void ListenerMethod(float value)
    {
        AudioListener.volume = sliderVolume.value;
        save.ChangeVolume(sliderVolume.value);
        save.Save();
    }
}
