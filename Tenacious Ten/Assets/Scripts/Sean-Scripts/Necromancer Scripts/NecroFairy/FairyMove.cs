using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMove : MonoBehaviour {

    Rigidbody2D rb;
    float alpha;
    public float MoveSpeed;
    public float Offset;
    [SerializeField] float diamaterX;
    [SerializeField] float diamaterY;

    [SerializeField] float RandomCastTimeLeft;
    [SerializeField] float RandomCastTimeRight;
    [SerializeField] float CastTime;
    bool CastTimeSet;
    [SerializeField] float CastTimeWarning;

    [SerializeField] GameObject FairyFire4Spread;
    [SerializeField] Transform FireSpawn;

    [SerializeField] GameObject FireRotator;

    bool facingRight, isFlying, RotatorCast, FireSpreadCast;

    Animator anim;

    StartOrResetLevel SORL;

    [SerializeField] bool FirstTimeAlive;
    [SerializeField] bool SecondTimeAlive;
    bool readyToMove;

    EnemyHealthManager Health;
    [SerializeField] int destroyAtThisHP;
    public bool FirstDeath;

    [SerializeField] AudioSource FireShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SORL = FindObjectOfType<StartOrResetLevel>();

        facingRight = true;
        isFlying = true;
        CastTimeSet = false;
        RotatorCast = false;
        FireSpreadCast = false;
        readyToMove = false;

        FirstDeath = false;

        if (FirstTimeAlive)
        {
            anim.SetInteger("State", 2);
        }
        
        Health = GetComponent<EnemyHealthManager>();
    }

    void FixedUpdate()
    {
        //if (Health.enemyHealth <= destroyAtThisHP)
        //{
        //    FirstDeath = true;
        //    anim.SetInteger("State", 3);
        //    Destroy(gameObject, 1.5f);
        //}

        if (FirstTimeAlive && !readyToMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, 0f);
            if(transform.position.y <= 3)
            {
                readyToMove = true;
            }
        }

        if(SecondTimeAlive && !readyToMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, 0f);
            if (transform.position.y >= -3)
            {
                alpha = 180;
                readyToMove = true;
            }
        }
        
        if(isFlying && readyToMove && Health.enemyHealth > destroyAtThisHP)
        {
            transform.position = new Vector2((diamaterX * Mathf.Sin(Mathf.Deg2Rad * alpha)), Offset + (diamaterY * Mathf.Cos(Mathf.Deg2Rad * alpha)));
            alpha += MoveSpeed;

            if (alpha == 360)
            {
                alpha = 0;
            }

            if (alpha >= 90 && alpha <= 270)
            {
                facingRight = false;
            }
            else
            {
                facingRight = true;
            }

            if (facingRight)
            {
                if (transform.localScale.x > 0)
                {
                    Vector3 temp = transform.localScale;
                    temp.x *= -1;
                    transform.localScale = temp;
                }
            }
            else if (!facingRight)
            {
                if (transform.localScale.x < 0)
                {
                    Vector3 temp = transform.localScale;
                    temp.x *= -1;
                    transform.localScale = temp;
                }
            }
        }

        if (!CastTimeSet)
        {
            CastTimeSet = true;
            CastTime = Random.Range(RandomCastTimeLeft, RandomCastTimeRight);
        }
        else if (CastTimeSet && readyToMove)
        {
            CastTime -= Time.deltaTime;
            if (CastTime <= CastTimeWarning && (alpha == 113 || alpha == 256 || alpha == 330|| alpha == 49))
            {
                isFlying = false;
                anim.SetInteger("State", 1);   
            }

            if(CastTime <= 0 && (alpha == 113  || alpha == 256 || alpha == 330 || alpha == 49))
            {
                if(!FireSpreadCast)
                {
                    FireShoot.Play();
                    Instantiate(FairyFire4Spread, FireSpawn.position, Quaternion.identity);
                    FireSpreadCast = true;
                }
                if(CastTime <= -1.25f)
                {
                    if(!RotatorCast)
                    {
                        RotatorCast = true;
                        Instantiate(FireRotator, FireSpawn.position, Quaternion.identity);
                    }
                    if(CastTime <= -9f)
                    {
                        RotatorCast = false;
                        FireSpreadCast = false;
                        isFlying = true;
                        CastTimeSet = false;
                        anim.SetInteger("State", 2);
                    }
                }
            }
        }
        

        
    }
    void Update()
    {

        if (Health.enemyHealth <= destroyAtThisHP)
        {
            FirstDeath = true;
            anim.SetInteger("State", 3);
            Destroy(gameObject, 1.5f);
        }

        if (SORL.ResetFight == true)
        {
            Destroy(gameObject);
        }

    }
}