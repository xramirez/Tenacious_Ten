using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Manager : MonoBehaviour {

    public float speedX;
    public float speedY;

    Rigidbody2D rb;

    public GameObject bone;
    public GameObject angledBone;
    public GameObject EyeShot;

    public bool hasShot, startFight;

    [SerializeField]
    AudioSource boneSound;

    [SerializeField]
    AudioSource laserSound;

    Transform projectilePos;
    Transform projectilePos2;

    public float shotDelay;
    private float shotDelayCounter;
    public float FirstShot;

    EnemyHealthManager EHM;

    Animator anim;

    public StartOrResetLevel SORL;

    Vector3 StartingPos;
    Vector3 StartingScale;

    float startingSpeedX, startingSpeedY;

    [SerializeField] float ChangeAnimTimer;
    float initChangeAnimTimer;
    float AnimTimeIncrement;
    bool AnimTransition;

    // Use this for initialization
    void Start()
    {
        StartingPos = transform.localPosition;
        StartingScale = transform.localScale;

        startingSpeedX = speedX;
        startingSpeedY = speedY;

        hasShot = false;
        startFight = false;
        EHM = GetComponent<EnemyHealthManager>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //BossMove(speedX, speedY);

        SORL = FindObjectOfType<StartOrResetLevel>();

        initChangeAnimTimer = ChangeAnimTimer;

        projectilePos = transform.Find("projectilePos");
        projectilePos2 = transform.Find("projectilePos2");

        FirstShot = 2f;

        AnimTransition = false;

        AnimTimeIncrement = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SORL.StartFight)
        {
            FirstShot -= Time.deltaTime;
            if(FirstShot <= 0)
            {
                FirstShot = 0;
                if (EHM.enemyHealth > 52)    //phase 1
                {
                    anim.SetInteger("State", 0);
                    shotDelayCounter -= Time.deltaTime;

                    if (shotDelayCounter <= 0 && !hasShot)
                    {
                        //shotDelayCounter = shotDelay;
                        shootBone();
                    }
                    if (shotDelayCounter <= -0.67)
                    {
                        //shootAngledBone();
                        shootEyeProj();
                        shotDelayCounter = shotDelay;
                        hasShot = false;
                    }
                }

                else if (EHM.enemyHealth <= 52)   //phase 2
                {
                    ChangeAnimTimer -= Time.deltaTime;
                    if(ChangeAnimTimer <= initChangeAnimTimer - AnimTimeIncrement)
                    {
                        if(anim.GetInteger("State") == 0 && AnimTimeIncrement <= initChangeAnimTimer)
                        {
                            anim.SetInteger("State", 2);
                        }
                        else if(anim.GetInteger("State") == 2 && AnimTimeIncrement <= initChangeAnimTimer)
                        {
                            anim.SetInteger("State", 0);
                        }
                        if(AnimTimeIncrement >= initChangeAnimTimer + 1)
                        {
                            //BossMove(speedX, speedY);
                        }
                        AnimTimeIncrement = AnimTimeIncrement + 0.2f;
                    }
                }
            }
            
        }

    }

    void Update()
    {
        if (SORL.StartFight)
        {
            if(AnimTimeIncrement >= initChangeAnimTimer + 1)
            {
                BossMove(speedX, speedY);
            }
        }
        
        if (SORL.ResetFight)
        {
            anim.SetInteger("State", 0);
            EHM.enemyHealth = EHM.maxHealth;
            ResetFight();
        }
    }

    void BossMove(float SX, float SY)
    {
        rb.velocity = new Vector3(SX, SY, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ceiling" || other.gameObject.tag == "Ground")
        {
            speedY = -speedY;
        }
        if (other.gameObject.tag == "Left Wall" || other.gameObject.tag == "Right Wall")
        {
            speedX = -speedX;

            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    void shootBone()
    {
        boneSound.Play();
        hasShot = true;
        Instantiate(bone, projectilePos.position, Quaternion.identity);
    }

    void shootAngledBone()
    {
        boneSound.Play();
        Instantiate(angledBone, projectilePos.position, Quaternion.identity);
    }

    void shootEyeProj()
    {
        laserSound.Play();
        Instantiate(EyeShot, projectilePos2.position, Quaternion.identity);
    }

    void ResetFight()
    {
        anim.SetInteger("State", 0);
        transform.localPosition = StartingPos;
        transform.localScale = StartingScale;
        speedX = startingSpeedX;
        speedY = startingSpeedX;
        BossMove(0.0f, 0.0f);
        FirstShot = 2f;
        AnimTransition = false;
        AnimTimeIncrement = 1;
        ChangeAnimTimer = initChangeAnimTimer;
    }
}
