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
    CapsuleCollider2D bodyCollider;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    bool isAlive = true;

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
    private void Start()
    {
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (!isAlive) { return; }
        Move_Player();
        Die();
    }

    void Die(){
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            //isAlive = false;
            //GetComponent<Rigidbody2D>().velocity = deathKick;
            print("died");
        }
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
