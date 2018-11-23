using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile : MonoBehaviour
{
    public static int currentLevel;
    public GameObject animatorGameObject;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        currentLevel = 1;
        Debug.Log("currentLevel is: " + currentLevel + ".");

        animatorGameObject = GameObject.FindGameObjectWithTag("World1_checkpoint");
        animator = animatorGameObject.GetComponent<Animator>();
        animator.SetBool("world1Available", true);
        animatorGameObject = GameObject.FindGameObjectWithTag("World1_boss");
        animator = animatorGameObject.GetComponent<Animator>();
        animator.SetBool("world1Unlocked", true);
    }
    
    public void levelUp()
    {
        if(currentLevel < 5)
        {
            currentLevel++;
            Debug.Log("updated currentLevel is: " + currentLevel + ".");
            string currentWorld = "World" + currentLevel;
            string checkpoint = currentWorld + "_checkpoint";
            string boss = currentWorld + "_boss";
            string available = "world" + currentLevel + "Available";
            string unlocked = "world" + currentLevel + "Unlocked";

            animatorGameObject = GameObject.FindGameObjectWithTag(checkpoint);
            animator = animatorGameObject.GetComponent<Animator>();
            animator.SetBool(available, true);
            animatorGameObject = GameObject.FindGameObjectWithTag(boss);
            animator = animatorGameObject.GetComponent<Animator>();
            animator.SetBool(unlocked, true);
        }
    }
}
	
