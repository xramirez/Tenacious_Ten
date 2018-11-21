using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world5TriggerStart : MonoBehaviour {

    public GameObject find;
    private Animator animator;
    public GameObject find2;
    private Animator animator2;
    private AudioSource audio1;
    private AudioSource audio2;

    // Start
    public void startWorld5()
    {
        find = GameObject.Find("checkpoint_world5");
        animator = find.GetComponent<Animator>();
        if (animator.GetBool("world5Available"))
        {
            animator.SetBool("startWorld5Level", true);
            audio1 = find.GetComponent<AudioSource>();
            audio1.Play();

            find2 = GameObject.Find("knight");
            animator2 = find2.GetComponent<Animator>();
            animator2.SetBool("runWorld5", true);
            audio2 = find2.GetComponent<AudioSource>();
            audio2.Play();
        }
    }
}
