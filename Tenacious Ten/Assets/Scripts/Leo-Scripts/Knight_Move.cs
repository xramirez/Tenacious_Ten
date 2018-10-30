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
    [SerializeField] Vector2 damageKick = new Vector2(75f, 0f);
    [SerializeField] Vector2 damageKick2 = new Vector2(-75f, 0f);
    //public int Health = 3;
    bool isAlive = true;
    bool facingRight;

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
        facingRight = controller.m_FacingRight;
        if (!isAlive) { return; }
        Move_Player();
        Damaged();
        Die();
    }

    void Die(){
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard"))){
            //isAlive = false;
            print("died");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    void Damaged(){
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))){
            GetComponent<Rigidbody2D>().velocity = !facingRight ? damageKick : damageKick2;
            //Health -= 1;
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
