using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public int currentLevel;
    public GameObject animatorGameObject;
    private Animator animator;
    
    void Awake()
    {
        currentLevel = SaveLoadManager.LoadLevelData();
        if(currentLevel != 1)
        {
            changeLevelConditions(currentLevel);
        }
        else
        {
            currentLevel = 1;
        }
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
        if(currentLevel <= 5)
        {
            currentLevel++;
            changeLevelConditions(currentLevel);
        }
    }
}
	
