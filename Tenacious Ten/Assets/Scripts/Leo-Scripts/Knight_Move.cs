using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knight_Move : MonoBehaviour {
    public Animator animator;
    public CharacterController2D controller;
    bool jumping = false;
    public float playerSpeed = 40f;
    float horizontalMove = 0f;
    public AudioSource jumpSound;

    void Move_Player()
    {

        horizontalMove = playerSpeed * Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            //jump();
            jumpSound.Play();
            jumping = true;
            animator.SetBool("IsJumping", true);
        }
    }

    void Update()
    {
        Move_Player();
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jumping);
        jumping = false;
    }
    public void OnLanding(){
        animator.SetBool("IsJumping", false);
    }

}
