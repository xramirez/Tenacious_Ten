using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveFile : MonoBehaviour
{
    public int currentLevel;
    public float currentVolume;
    public GameObject animatorGameObject;
    private Animator animator;
    public Slider slider;
    
    void Awake()
    {
        AudioListener.volume = SaveLoadManager.LoadVolumeData();
        currentLevel = SaveLoadManager.LoadLevelData();
        if(currentLevel != 1)
        {
            changeLevelConditions(currentLevel);
        }
    }
    public void VolumeLevelSave(int x)
    {
        currentLevel = x;
        //SaveLoadManager.SaveLevelData(this);
    }

    public void ChangeVolume(float changer)
    {
        currentVolume = changer;
        //SaveLoadManager.SaveLevelData(this);
    }

    public void Save()
    {
        SaveLoadManager.SaveLevelData(this);
    }
    public void Load()
    {
        currentLevel = SaveLoadManager.LoadLevelData();
        changeLevelConditions(currentLevel);
    }
    public void DeleteSave()
    {
        SaveLoadManager.DeleteLevelData();
    }

    private void changeLevelConditions(int cLevel)
    {
        for(int x = 1; x <= cLevel; x++)
        {
            Debug.Log("Updated currentLevel via SaveLoadManager is: " + x + ".");
            string currentWorld = "World" + x;
            string checkpoint = currentWorld + "_checkpoint";
            string boss = currentWorld + "_boss";
            string available = "world" + x + "Available";
            string unlocked = "world" + x + "Unlocked";
            try
            {
                animatorGameObject = GameObject.FindGameObjectWithTag(checkpoint);
                animator = animatorGameObject.GetComponent<Animator>();
                animator.SetBool(available, true);
                animatorGameObject = GameObject.FindGameObjectWithTag(boss);
                animator = animatorGameObject.GetComponent<Animator>();
                animator.SetBool(unlocked, true);
            }
            catch
            {
                Debug.Log("Cannot find GameObject or Animator from \"SaveFile.cs\" .");
            }
        }

    }

    public void levelUp()
    {
        if(currentLevel <= 4)
        {
            currentLevel++;
            changeLevelConditions(currentLevel);
        }
    }
}
	
