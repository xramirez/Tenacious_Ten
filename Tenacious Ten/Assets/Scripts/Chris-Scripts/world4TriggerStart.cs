using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class world4TriggerStart : MonoBehaviour {

    public GameObject find;
    private Animator animator;
    public GameObject find2;
    private Animator animator2;
    private AudioSource audio1;
    private AudioSource audio2;

    // Start
    public void startWorld4()
    {
        find = GameObject.Find("checkpoint_world4");
        animator = find.GetComponent<Animator>();
        if (animator.GetBool("world4Available"))
        {
            animator.SetBool("startWorld4Level", true);
            audio1 = find.GetComponent<AudioSource>();
            audio1.Play();

            find2 = GameObject.Find("puppeteer");
            animator2 = find2.GetComponent<Animator>();
            animator2.SetBool("runWorld4", true);
            audio2 = find2.GetComponent<AudioSource>();
            audio2.Play();
        }
    }
}
