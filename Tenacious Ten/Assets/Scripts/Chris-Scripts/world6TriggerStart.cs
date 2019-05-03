using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world6TriggerStart : MonoBehaviour
{
    public Canvas LevelCanvas;
    public GameObject find;
    private Animator animator;
    public GameObject find2;
    private Animator animator2;
    private AudioSource audio1;
    private AudioSource audio2;
    public GameObject Loader;

    // Start
    public void startWorld6()
    {
        find = GameObject.Find("checkpoint_world6");
        animator = find.GetComponent<Animator>();
        if (animator.GetBool("world6Available"))
        {
            animator.SetBool("startWorld6Level", true);
            audio1 = find.GetComponent<AudioSource>();
            audio1.Play();

            find2 = GameObject.Find("chariot");
            animator2 = find2.GetComponent<Animator>();
            animator2.SetBool("runWorld6", true);
            audio2 = find2.GetComponent<AudioSource>();
            audio2.Play();

            Loader.GetComponent<LoadingHandler>().LoadingALevel();
        }
    }
}
