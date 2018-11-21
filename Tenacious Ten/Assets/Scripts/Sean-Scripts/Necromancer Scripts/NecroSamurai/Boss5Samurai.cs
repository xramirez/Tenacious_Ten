using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss5Samurai : MonoBehaviour {

    [SerializeField] GameObject StraightArrow;
    [SerializeField] Transform Bow;
    [SerializeField] GameObject AngledArrow;
    [SerializeField] Transform AngledBow;

    Animator anim;
    Rigidbody2D rb;

    [SerializeField] float ShotWarningTimer;
    float initialWarningTimer;

    [SerializeField] float RandomShotTimerLeft;
    [SerializeField] float RandomShotTimerRight;
    bool ShotTimerSet, WillGroundShot;
    [SerializeField] float ShotTimer;

    bool InAir, justJumped, shotOneAngled, shotTwoAngled;
    [SerializeField] float jumpSpeedY;
    [SerializeField] float SecondShotTimer;
    float initialSecondTimer;

    [SerializeField] float jumpTimer;
    float initialJumpTimer;

    [SerializeField] bool FirstTimeAlive;
    bool HitTheGroundFirstTime;
    [SerializeField] bool SecondTimeAlive;

    EnemyHealthManager Health;
    [SerializeField] int destroyAtThisHP;
    public bool FirstDeath;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ShotTimerSet = false;
        InAir = false;
        justJumped = false;
        shotOneAngled = false;
        shotTwoAngled = false;
        WillGroundShot = false;

        initialSecondTimer = SecondShotTimer;
        initialJumpTimer = jumpTimer;

        Health = GetComponent<EnemyHealthManager>();

        FirstDeath = false;

        if (FirstTimeAlive)
        {
            HitTheGroundFirstTime = false;
            anim.SetInteger("State", 3);
        }
        else
        {
            HitTheGroundFirstTime = true;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(!InAir && Health.enemyHealth <= destroyAtThisHP)
        {
            FirstDeath = true;
            anim.SetInteger("State", 10);
            Destroy(gameObject, 1f);
        }

        //Debug.Log(rb.velocity.y);
        if (!ShotTimerSet)
        {
            ShotTimer = Random.Range(RandomShotTimerLeft, RandomShotTimerRight);
            ShotTimerSet = true;
        }

        if(!InAir && HitTheGroundFirstTime)
        {
            ShotTimer -= Time.deltaTime;
        }

        if(ShotTimer <= ShotWarningTimer && Health.enemyHealth > destroyAtThisHP)   //send signal through bow to indicate a shot is coming
        {
            WillGroundShot = true;
            anim.SetInteger("State", 1);
            if(ShotTimer <= 0)
            {
                WillGroundShot = false;
                anim.SetInteger("State", 2);
                Instantiate(StraightArrow, Bow.position, Quaternion.identity);
                //ShotTimer = initialWarningTimer;
                ShotTimerSet = false;
            }
        }
        
        if(!WillGroundShot && HitTheGroundFirstTime)
        {
            jumpTimer -= Time.deltaTime;
        }
        if(jumpTimer <= 0 && ShotTimer >= ShotWarningTimer)
        {
            InAir = true;

            //if(transform.position.y < 0 && justJumped && !shotOneAngled) //&& Mathf.Abs(rb.velocity.y) >= 3.5
            //{
            //   anim.SetInteger("State", 20);
            //}

            if (!justJumped)
            {
                anim.SetInteger("State", 3);
                StartCoroutine(waitNextAnim(0.33f));
                rb.AddForce(new Vector2(0f, jumpSpeedY));
                justJumped = true;
            }

            if (Mathf.Abs(rb.velocity.y) <= 2.5 && transform.position.y >= 0 && !shotTwoAngled && !shotOneAngled)
            {
                SecondShotTimer -= Time.deltaTime * 2;
                anim.SetInteger("State", 21);

                if (!shotOneAngled && !shotTwoAngled && SecondShotTimer <= 0)
                {
                    SecondShotTimer = initialSecondTimer;
                    anim.SetInteger("State", 22);
                    Instantiate(AngledArrow, AngledBow.position, AngledBow.rotation);
                    shotOneAngled = true;
                }
                
            }
            else if(shotOneAngled && !shotTwoAngled)
            {
                anim.SetInteger("State", 21);
                SecondShotTimer -= Time.deltaTime;
                
                if (SecondShotTimer <= 0 && !shotTwoAngled)
                {
                    anim.SetInteger("State", 22);
                    Instantiate(AngledArrow, AngledBow.position, AngledBow.rotation);
                    SecondShotTimer = initialSecondTimer;
                    shotTwoAngled = true;
                }
            }
            //else if(transform.position.y >= 0 && rb.velocity.y < 0)
            //{
             //   anim.SetInteger("State", 3);
            //}
            else if(transform.position.y <= 0 && rb.velocity.y < 0)
            {
                justJumped = false;
                anim.SetInteger("State", 3);
                jumpTimer = initialJumpTimer;
                shotOneAngled = false;
                shotTwoAngled = false;
            }
            
        }

		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            HitTheGroundFirstTime = true;
            anim.SetInteger("State", 2);
            InAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetInteger("State", 3);
        }
    }


    IEnumerator waitNextAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetInteger("State", 20);
    }

}
