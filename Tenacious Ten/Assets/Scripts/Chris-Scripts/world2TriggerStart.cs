using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world2TriggerStart : MonoBehaviour {

    public GameObject find;
    private Animator animator;
    public GameObject find2;
    private Animator animator2;
    private AudioSource audio1;
    private AudioSource audio2;

    // Start
    public void startWorld2()
    {
        find = GameObject.Find("checkpoint_world2");
        animator = find.GetComponent<Animator>();
        if (animator.GetBool("world2Available"))
        {
            animator.SetBool("startWorld2Level", true);
            audio1 = find.GetComponent<AudioSource>();
            audio1.Play();

            find2 = GameObject.Find("countshadow");
            animator2 = find2.GetComponent<Animator>();
            animator2.SetBool("runWorld2", true);
            audio2 = find2.GetComponent<AudioSource>();
            audio2.Play();
        }
    }
}
