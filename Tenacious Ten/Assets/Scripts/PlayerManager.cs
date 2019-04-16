using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;

    bool facingRight, Jumping, isGrounded, isWalking;
    public float speed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    [SerializeField]
    AudioSource jumpSound;
    [SerializeField]
    AudioSource shootSound;
	
    public GameObject leftProjectile, rightProjectile;

    Transform projectilePos;    //transform is position

    public float shotDelay;
    private float shotDelayCounter;

    public Animator anim;
    Rigidbody2D rb;

    [SerializeField] float flipValue;

    bool justJumped;

    bool landedFromJump;

    int prevAnimState; //used to hold a variable for the previous animation state and call back to it if needed

    public PlayerHealthManager healthManager;

    Boss03Phase3 P3; //for wendigo boss

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        healthManager = FindObjectOfType<PlayerHealthManager>();

        facingRight = true;

        isWalking = false;

        projectilePos = transform.Find("projectilePos");   //find child named projectilePos (i.e. its position)

        flipValue = 1;

        justJumped = false;

        landedFromJump = false;

        if(FindObjectOfType<Boss03Phase3>() != null)
        {
            P3 = GameObject.FindObjectOfType<Boss03Phase3>();
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //checks for if player is touching the ground
    }

    // Update is called once per frame
    void Update () {
        if(!Boss01PauseMenu.GameIsPaused)
        {
            if(healthManager.isDead)
            {
                speed = 0;
                anim.SetInteger("State", 0);
            }
            if (!healthManager.isDead)
            {
                MovePlayer(speed);
                Flip();

                if(GameObject.FindObjectOfType<Boss03Phase3>()!= null)
                {
                    if(!P3.isPushingBack)
                    {
                        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                        {
                            speed = speedX; 
                            isWalking = true;
                        }
                        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
                        {
                            speed = 0; 
                            isWalking = false;
                        }

                        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                        {
                            speed = -speedX; 
                            isWalking = true;
                        }
                        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
                        {
                            speed = 0; 
                            isWalking = false;
                        }
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    {
                        //anim.SetInteger("State", 2);
                        speed = speedX;     //move right
                        isWalking = true;
                    }
                    if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
                    {
                        //anim.SetInteger("State", 0);
                        speed = 0;         //not walking/idle
                        isWalking = false;
                    }

                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    {
                        //anim.SetInteger("State", 2);
                        speed = -speedX;    //move left
                        isWalking = true;
                    }
                    if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
                    {
                        //anim.SetInteger("State", 0);
                        speed = 0;          //not walking/idle
                        isWalking = false;
                    }
                }
                


                //if (justJumped && grounded)
               // {
                //    anim.SetInteger("State", 5);
               //    justLanded(0.2f);
                //    justJumped = false;
               // }

                if ((Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)) && grounded)    //jump
                {
                    Jump();
                    landedFromJump = false;
                }
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    anim.SetInteger("State", 3);
                }


                if (grounded && landedFromJump && !isWalking)   //if player is on the ground, not jumping, and is not walking set animation to idle. if walking, change to walking animation
                {
                    anim.SetInteger("State", 0);
                }
                else if (grounded && landedFromJump && isWalking)
                {
                    anim.SetInteger("State", 2);
                }



                if (Input.GetKeyDown(KeyCode.Space))    //shoot
                {
                    //anim.SetInteger("State", 7);
                    Fire();
                    shotDelayCounter = shotDelay;
                }

                if (Input.GetKey(KeyCode.Space))    //shoot, but able to hold down space to shoot automatically
                {
                    anim.SetInteger("State", 9);
                    shotDelayCounter -= Time.deltaTime;

                    if (shotDelayCounter <= 0)
                    {
                        shotDelayCounter = shotDelay;
                        Fire();
                    }
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (!grounded) //if player is in the air, e.g. jumping
                    {
                        anim.SetInteger("State", 3);
                    }
                    if (grounded && isWalking) //(Input.GetKeyDown(KeyCode.LeftArrow)) || Input.GetKey(KeyCode.RightArrow)) //if player is on ground and walking
                    {
                        anim.SetInteger("State", 2);
                    }
                    else if (grounded)
                    {
                        anim.SetInteger("State", 0);
                    }
                }
            }
            
        }
        else if(Boss01PauseMenu.GameIsPaused)
        {
            speed = 0;
        }
        
    }

    void Flip()     //function to flip the image of the player. also flips the projectile as well 
    {
        if (speed > 0 && facingRight == false || speed < 0 && facingRight == true)
        {
            facingRight = !facingRight;

            Vector3 temp = transform.localScale;
            Vector3 temp2 = transform.localPosition;
            temp.x *= -1;
            if(facingRight)
            {

                temp2.x = temp2.x + flipValue;
            }
            if(!facingRight)
            {

                temp2.x = temp2.x - flipValue;
            }
            transform.localScale = temp;
            transform.localPosition = temp2;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Upper Ground")
        {
            anim.SetInteger("State", 5);
            landedFromJump = true;
        }

        if (other.gameObject.tag == "Moving Platform Boss 4")
        {
            anim.SetInteger("State", 5);
            landedFromJump = true;
            flipValue = -0.3f;
            transform.SetParent(other.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Upper Ground")
        {
            landedFromJump = false;
        }

        if (other.gameObject.tag == "Moving Platform Boss 4")
        {
            transform.SetParent(null);
            flipValue = 1;
        }
    }

    void MovePlayer(float playerSpeed) //player movement
    {
        rb.velocity = new Vector3(playerSpeed, rb.velocity.y, 0);
    }

    void Jump()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));  
        Jumping = true;
        jumpSound.Play();
    }

    void Fire() //shoot projectile/fire projectile
    {
        shootSound.Play();
        if (facingRight == true)
        {
            ScoreManager.Instance.ShotsFired++;
            Instantiate(rightProjectile, projectilePos.position, Quaternion.identity);    
            //Instantiate means create. So create a right projectile, at a specific position in world/space (projectilePos)
            //Quanternion is rotation, in this case do not rotate
        }
        if (facingRight == false)
        {
            ScoreManager.Instance.ShotsFired++;
            Instantiate(leftProjectile, projectilePos.position, Quaternion.identity);
        }
    }

    IEnumerator justLanded(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetInteger("State", 0);
    }
    
}
