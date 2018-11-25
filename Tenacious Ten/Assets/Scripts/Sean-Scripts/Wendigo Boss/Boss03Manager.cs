using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Manager : MonoBehaviour {

    //starting variables
    Vector3 StartingPos;
    Vector3 StartingScale;
    public bool facingLeft;
    public Animator anim;
    Rigidbody2D rb;
    bool isIdle;
    GameObject player;
    float distanceBetween;
    Vector2 TrackerPosition = new Vector2(0, 0);
    EnemyHealthManager EHM;
    Boss03Phase2 P2;
    StartOrResetLevel SORL;

    //phase1 variables
    public float JumpSpeedY;
    public float speedX;
    bool atPlayerLoc, swingFinished, hasJumped, isSwinging;
    public bool playerFound;
    public bool jumpFinished;
    public bool isJumping;
    float playerLocation;
    float swingCountdown;
    float count;
    float changeDist;
    int phaseOneRandomizer;
    int jumpCounter;
    public float phaseOneCounter;
    public int HAUH;

    float SquatTimer;

    bool phaseOneActivated;
    float BeginFightTimer;

    float BatTimer;
    public Transform BatSpawn;
    public GameObject FlyingBat;

    //General Boss variables
    public int EnterPhaseTwoHP;
    public int EnterPhaseThreeHP;

    [SerializeField]
    AudioSource whooshSound;

    [SerializeField]
    AudioSource ghostSound;

    [SerializeField]
    AudioSource clawSound;

    // Use this for initialization
    void Start () {
        //starting variables
        StartingPos = transform.localPosition;
        StartingScale = transform.localScale;
        facingLeft = true;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isIdle = true;
        player = GameObject.FindGameObjectWithTag("Player");
        distanceBetween = player.transform.position.x - transform.position.x;
        EHM = GetComponent<EnemyHealthManager>();
        P2 = GetComponent<Boss03Phase2>();
        SORL = FindObjectOfType<StartOrResetLevel>();

        //phase 1 variables
        playerFound = false;
        atPlayerLoc = false;
        swingCountdown = 0;
        swingFinished = false;
        jumpFinished = false;
        count = 0.8f;
        hasJumped = false;
        isJumping = false;
        isSwinging = false;
        jumpCounter = 0;
        phaseOneCounter = 1.5f;
        phaseOneActivated = false;
        BeginFightTimer = 3f;
        BatTimer = 0f;

        SquatTimer = 0.5f;

    }//end Start()
	
	// Update is called once per frame
	void FixedUpdate () {

        //PHASE 1
        phaseOneRandomizer = Random.Range(0, 2);

        if(phaseOneRandomizer == 0 && isIdle && phaseOneActivated)
        {
            isJumping = true;
            isIdle = false;
        }
        else if (phaseOneRandomizer == 1 && isIdle && phaseOneActivated)
        {
            isSwinging = true;
            isIdle = false;
        }
        
        if(phaseOneActivated)
        {
            phaseOneCounter -= Time.deltaTime;
        }

        if (isJumping && !isSwinging && phaseOneCounter <= 0)
        {
            if(!playerFound)
            {
                anim.SetInteger("State", 4);
                Debug.Log("Found player...");
                LocatePlayer();
            }
            else if(playerFound && !jumpFinished)
            {
                SquatTimer -= Time.deltaTime;
                if(SquatTimer <= 0)
                {
                    Debug.Log("Jumping...");
                    Jump(playerLocation);
                }
            }
            else if(jumpFinished)
            {
                anim.SetInteger("State", 0);
                Debug.Log("Jump complete...");
                isJumping = false;
                isIdle = true;
                jumpFinished = false;
                hasJumped = false;
                playerFound = false;
                phaseOneCounter = 1.5f;
                SquatTimer = 0.5f;
                if (EHM.enemyHealth <= EnterPhaseTwoHP)
                {
                    phaseOneActivated = false;
                }
            }
        }//end Jump
        if(isSwinging && !isJumping && phaseOneCounter <= 0)
        {
            //Debug.Log("swinging...");
            if (!playerFound)
            {
                LocatePlayer();
            }
            else if(playerFound && !atPlayerLoc)
            {
                anim.SetInteger("State", 1);
                MoveToPlayer();
            }
            else if(atPlayerLoc && !swingFinished && count <= 0)
            {
                clawSound.Play();
                SwingAttack();
            }
            else if(swingFinished)
            {
                anim.SetInteger("State", 0);
                isSwinging = false;
                swingFinished = false;
                playerFound = false;
                atPlayerLoc = false;
                count = 0.8f;
                isIdle = true;
                phaseOneCounter = 1.5f;
                P2.phaseTwoCounter = 2.5f;
                if (EHM.enemyHealth <= EnterPhaseTwoHP)
                {
                    phaseOneActivated = false;
                }
            }

            if (atPlayerLoc)
            {
                count -= Time.deltaTime;
            }
        }//end Swing

        if(SORL.StartFight)
        {
            BeginFightTimer -= Time.deltaTime;
        }

        if (BeginFightTimer <= 0)
        {
            BeginFightTimer = 0;
            if (EHM.enemyHealth > EnterPhaseTwoHP)
            {
                phaseOneActivated = true;
            }
            BatTimer -= Time.deltaTime;
            if(BatTimer <= 0)
            {
                ghostSound.Play();
                Instantiate(FlyingBat, BatSpawn.position, Quaternion.identity);
                BatTimer = 3f;
            }
        }

        


    }//end FixedUpdate()

    void Update()
    {
        if (SORL.ResetFight == true)
        {
            ResetFight();
        }
    }

    public void Flip() //flip boss depending on side
    {
         //facingLeft = !facingLeft;
         Vector3 temp = transform.localScale;
         temp.x *= -1;
         transform.localScale = temp;
    }

    void Jump(float location)
    {
        if(!hasJumped)
        {
            anim.SetInteger("State", 5);
            rb.AddForce(new Vector2(rb.velocity.x, JumpSpeedY));
            hasJumped = true;
        }
        if(facingLeft)//if(distanceBetween < 0) //i.e. facing left
        {
            changeDist = -(playerLocation - transform.position.x) / 30;
            transform.position = new Vector3(transform.position.x - changeDist, transform.position.y, transform.position.z);
        }
        else if(!facingLeft)
        {
            changeDist = (playerLocation - transform.position.x) / 30;
            transform.position = new Vector3(transform.position.x + changeDist, transform.position.y, transform.position.z);
        }
        jumpCounter++;
        if (jumpCounter > 60)
        {
            jumpCounter = 0;
            jumpFinished = true;
        }
    }
    
    void LocatePlayer()
    {
        playerFound = true;
        playerLocation = player.transform.position.x;
        if (playerLocation < transform.position.x)
        {
            if(!facingLeft)
            {
                Flip();
            }
            facingLeft = true;
        }
        else if (playerLocation > transform.position.x)
        {
            if (facingLeft)
            {
                Flip();
            }
            facingLeft = false;
        }
    }

    void MoveToPlayer()
    {
        if(facingLeft && transform.position.x > playerLocation)
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        else if(!facingLeft && transform.position.x < playerLocation)
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }

        if (facingLeft && transform.position.x <= playerLocation)
        {
            atPlayerLoc = true;
            //anim.SetInteger("State", 1);
        }
        else if(!facingLeft && transform.position.x >= playerLocation)
        {
            atPlayerLoc = true;
            //anim.SetInteger("State", 1);
        }
    }

    void SwingAttack()
    {
        anim.SetInteger("State", 2);
        swingCountdown++;
        if (swingCountdown == 29)
        {
            if (facingLeft)
            {
                transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);

            }
            else if (!facingLeft)
            {
                transform.position = new Vector3(transform.position.x + 2.6f, transform.position.y, transform.position.z);
            }
            
        }

        if(swingCountdown == HAUH)
        {
            if (facingLeft)
            {
                transform.position = new Vector3(transform.position.x + 1.32f, transform.position.y, transform.position.z);
            }
            else if (!facingLeft)
            {
                transform.position = new Vector3(transform.position.x - 1.32f, transform.position.y, transform.position.z);
            }
            swingCountdown = 0;
            swingFinished = true;
        }

        //anim.SetInteger("State", 1);
    }

    void ResetFight()
    {
        transform.position = StartingPos;
        transform.localScale = StartingScale;

        facingLeft = true;
        isIdle = true;

        anim.SetInteger("State", 0);

        EHM.enemyHealth = EHM.maxHealth;

        playerFound = false;
        atPlayerLoc = false;
        swingCountdown = 0;
        swingFinished = false;
        jumpFinished = false;
        count = 0.8f;
        hasJumped = false;
        isJumping = false;
        isSwinging = false;
        jumpCounter = 0;
        phaseOneCounter = 1.5f;
        phaseOneActivated = false;
        BeginFightTimer = 3f;
        BatTimer = 0f;

        SquatTimer = 0.5f;
    }
}
