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
    public Animator checkp_1;
    public Animator anim_1;
    public Animator checkp_2;
    public Animator anim_2;
    public Animator checkp_3;
    public Animator anim_3;
    public Animator checkp_4;
    public Animator anim_4;
    public Animator checkp_5;
    public Animator anim_5;

    // Use this for initialization
    void Start () {
        LoadingImage.SetActive(false);

        checkp_1.SetBool("world1Available", true);
        anim_1.SetBool("world1Unlocked", true);
        checkp_2.SetBool("world2Available", false);
        anim_2.SetBool("world2Unlocked", false);
        checkp_3.SetBool("world3Available", false);
        anim_3.SetBool("world3Unlocked", false);
        checkp_4.SetBool("world4Available", false);
        anim_4.SetBool("world4Unlocked", false);
        checkp_5.SetBool("world5Available", false);
        anim_5.SetBool("world5Unlocked", false);
        if (SaveLoadManager.LoadLevelData() == 1)
        {
            checkp_1.SetBool("world1Available", true);
            anim_1.SetBool("world1Unlocked", true);
            Debug.Log("World 1 Available");

        }
        if (SaveLoadManager.LoadLevelData() >= 2)
        {
            checkp_2.SetBool("world2Available", true);
            anim_2.SetBool("world2Unlocked", true);
            Debug.Log("World 2 Available");

        }
        if (SaveLoadManager.LoadLevelData() >= 3)
        {
            checkp_3.SetBool("world3Available", true);
            anim_3.SetBool("world3Unlocked", true);
            Debug.Log("World 3 Available");

        }
        if (SaveLoadManager.LoadLevelData() >= 4)
        {
            checkp_4.SetBool("world4Available", true);
            anim_4.SetBool("world4Unlocked", true);
            Debug.Log("World 4 Available");

        }
        if (SaveLoadManager.LoadLevelData() == 5)
        {
            checkp_5.SetBool("world5Available", true);
            anim_5.SetBool("world5Unlocked", true);
            Debug.Log("World 5 Available");
        }
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
