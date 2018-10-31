using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBossManager : MonoBehaviour
{
    Vector3 StartingPos;
    Vector3 StartingScale;

    float startingPosX;

    bool facingLeft;

    public float dashSpeedX;

    public GameObject fireball;

    Rigidbody2D rb;
    Animator anim;
    EnemyHealthManager EHM;

    Transform projectilePos;

    public float shotDelay;
    private float shotDelayCounter;

    public float dashDelay;
    private float dashDelayCounter;

    // Use this for initialization
    void Start()
    {

        StartingPos = transform.localPosition;
        StartingScale = transform.localScale;
        startingPosX = transform.position.x;

        facingLeft = true;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        EHM = GetComponent<EnemyHealthManager>();

        projectilePos = transform.Find("projectilePos");
    }

    // Update is called once per frame
    void Update()
    {

        if (EHM.enemyHealth > 50)    //phase 1
        {

            shotDelayCounter -= Time.deltaTime;
            dashDelayCounter -= Time.deltaTime;

            if (shotDelayCounter <= 0)
            {
                shotDelayCounter = shotDelay;
                //CastFireball();
            }

            if (dashDelayCounter <= 0 && facingLeft)
            {
                DashAttackLeftFull();
                dashDelayCounter = dashDelay;
            }

           // if (dashDelayCounter <= 0 && !facingLeft)
            //{
            //    dashDelayCounter = dashDelay;
            //}
            StopDash();
        }
    }

    void Flip() //flip boss depending on side
    {
        facingLeft = !facingLeft;
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    void DashAttackLeftFull()   //dash attack facing left the length of the whole room (full)
    {
        rb.velocity = new Vector3(-dashSpeedX, rb.velocity.y, 0);
    }

    void DashAttackRightFull()  //dash attack facing right the length of the whole room (full)
    {

    }

    void StopDash()
    {
        if (transform.position.x <= -startingPosX && facingLeft)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            Flip();
        }
    }

    void CastFireball() //cast fireball / projectile
    {
        anim.SetInteger("State", 1);
        Instantiate(fireball, projectilePos.position, Quaternion.identity);
    }
}
