using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

    public float speedX;
    public float speedY;

    Rigidbody2D rb;

    public GameObject bone;
    public GameObject angledBone;
    public GameObject EyeShot;

    public bool hasShot, startFight;

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


        projectilePos = transform.Find("projectilePos");
        projectilePos2 = transform.Find("projectilePos2");

        FirstShot = 2f;
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
                if (EHM.enemyHealth > 40)    //phase 1
                {
                    anim.SetInteger("State", 0);
                    shotDelayCounter -= Time.deltaTime;

                    if (shotDelayCounter <= 0 && !hasShot)
                    {
                        //shotDelayCounter = shotDelay;
                        shootBone();
                    }
                    if (shotDelayCounter <= -0.5)
                    {
                        //shootAngledBone();
                        shootEyeProj();
                        shotDelayCounter = shotDelay;
                        hasShot = false;
                    }
                }

                if (EHM.enemyHealth <= 40)   //phase 2
                {
                    anim.SetInteger("State", 1);
                    BossMove(speedX, speedY);
                }
            }
            
        }

    }

    void Update()
    {
        if (SORL.ResetFight)
        {
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
        hasShot = true;
        Instantiate(bone, projectilePos.position, Quaternion.identity);
    }

    void shootAngledBone()
    {
        Instantiate(angledBone, projectilePos.position, Quaternion.identity);
    }

    void shootEyeProj()
    {
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
    }
}
