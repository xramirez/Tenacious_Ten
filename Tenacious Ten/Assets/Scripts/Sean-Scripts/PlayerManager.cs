using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour {

    public float speedX;
    public float jumpSpeedY;

    bool facingRight, Jumping, isGrounded, isWalking;
    float speed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    [SerializeField]
    private bool grounded;
    [SerializeField]
    bool landedFromJump;
    public UnityEvent OnLandEvent;
    Collider2D player_col;

    public GameObject leftProjectile, rightProjectile;

    Transform projectilePos;    //transform is position

    public float shotDelay;
    private float shotDelayCounter;

    Animator anim;
    Rigidbody2D rb;

    int prevAnimState; //used to hold a variable for the previous animation state and call back to it if needed

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player_col = GetComponent<Collider2D>();

        facingRight = true;

        isWalking = false;

        landedFromJump = false;
        Jumping = false;

        projectilePos = transform.Find("projectilePos");   //find child named projectilePos (i.e. its position)
	}

    private void FixedUpdate()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        //grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!grounded)
                    OnLandEvent.Invoke();
            }
        }
        //checks for if player is touching the ground
    }

    // Update is called once per frame
    void Update () {

        MovePlayer(speed);
        Flip();
        Die();

        if (Input.GetKey(KeyCode.RightArrow))
        { 
            speed = speedX;     //move right
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;         //not walking/idle
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed = -speedX;    //move left
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;          //not walking/idle
            isWalking = false;
        }



        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)    //jump
        {
            Jump();
            landedFromJump = false;
        }

        if(Input.GetKey(KeyCode.UpArrow))   //on jump key press, change animation to jump animation
        {
            anim.SetInteger("State", 3);
        }

        
        if (grounded && landedFromJump && !isWalking)   //if player is on the ground, not jumping, and is not walking set animation to idle. if walking, change to walking animation
        {
            anim.SetInteger("State", 0);
        }
        else if(grounded && landedFromJump && isWalking)
        {
            anim.SetInteger("State", 2);
        }



        if (Input.GetKeyDown(KeyCode.Space))    //shoot
        {
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
            if (grounded && isWalking)
            {
                anim.SetInteger("State", 2);
            }
            else if (grounded)
            {
                anim.SetInteger("State", 0);
            }
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

                temp2.x = temp2.x + 1;
            }
            if(!facingRight)
            {

                temp2.x = temp2.x - 1;
            }
            transform.localScale = temp;
            transform.localPosition = temp2;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Ground")
        {
            anim.SetInteger("State", 5);
            landedFromJump = true;
        }
    }

   void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            landedFromJump = false;
        }
    }

    void MovePlayer(float playerSpeed) //player movement
    {
        rb.velocity = new Vector3(playerSpeed, rb.velocity.y, 0);
    }

    void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));     //will add force, take in a parameter (vector [x,y])
        Jumping = true;
    }

    void Die()
    {
        if (player_col.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            //isAlive = false;
            print("died");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void Fire() //shoot projectile/fire projectile
    {
        if (facingRight == true)
        {
            Instantiate(rightProjectile, projectilePos.position, Quaternion.identity);    
            //Instantiate means create. So create a right projectile, at a specific position in world/space (projectilePos)
            //Quanternion is rotation, in this case do not rotate
        }
        if (facingRight == false)
        {
            Instantiate(leftProjectile, projectilePos.position, Quaternion.identity);
        }
    }
}
