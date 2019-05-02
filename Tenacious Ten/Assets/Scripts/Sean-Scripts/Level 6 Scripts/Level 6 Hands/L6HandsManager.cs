using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L6HandsManager : MonoBehaviour
{

    public int maxHealth;
    public int enemyHealth;

    public GameObject deathEffect;

    SpriteRenderer sr;
	ChariotMiniReset2 track;
	[SerializeField]
	GameObject track3;
	[SerializeField]
	GameObject track4;
	bool skyStarter = false;

    [SerializeField] public bool handsIsDead;

    [SerializeField] AudioSource hurtSound;

    public bool moveToStart;
    [SerializeField] float moveSpeedLeft;

    public Level6Manager L6Manager;

    Rigidbody2D rb;
    Animator anim;

    bool spawnBombs;
    [SerializeField]int amtOfBombs;
    bool hasWaitedForNextBomb;
    bool spawnSpikedBall;
    int attackChoice;
    bool chosenAnAttack;
    int countBombs;

    bool waitOnce;
    [SerializeField] float waitJumpTimer;
    float jumpTimerStart;

    [SerializeField] GameObject handBomb;
    [SerializeField] float timeBetweenBombs;

    bool hasJumpedForSpike;
    bool actuallyJumped;
    bool spikedBallSpawned;
    [SerializeField] int jumpSpeedY;
    [SerializeField] GameObject spikedBall;

    int moveOneCounter;
    int moveTwoCounter;

    void Start()
    {
        maxHealth = enemyHealth;
        sr = GetComponent<SpriteRenderer>();
        handsIsDead = false;
        moveToStart = false;
        L6Manager = FindObjectOfType<Level6Manager>();
		track = GetComponent<ChariotMiniReset2>();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        spawnBombs = false;
        hasWaitedForNextBomb = true;
        spawnSpikedBall = false;
        chosenAnAttack = false;
        attackChoice = 0;
        countBombs = 0;

        hasJumpedForSpike = false;
        actuallyJumped = false;

        waitOnce = false;

        spikedBallSpawned = false;

        jumpTimerStart = waitJumpTimer;

        moveOneCounter = 0;
        moveTwoCounter = 0;
	}

    void FixedUpdate()
    {
		if (enemyHealth <= 5)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            handsIsDead = true;
        }
        if (moveToStart == false && L6Manager.bootsDead)
        {
			if(skyStarter == false)
			{
				track.damageBenchmark = track.pushBack.TotalDamage;
				track3.GetComponent<ChariotMiniReset3>().damageBenchmark = track3.GetComponent<ChariotMiniReset3>().pushBack.TotalDamage;
				track4.GetComponent<ChariotMiniReset4>().damageBenchmark = track4.GetComponent<ChariotMiniReset4>().pushBack.TotalDamage;
				skyStarter = true;
			}
            transform.position = new Vector3(transform.position.x - moveSpeedLeft, transform.position.y, transform.position.z);
            if (transform.position.x <= -53)
            {
                moveToStart = true;
            }
        }

        if(chosenAnAttack == false && moveToStart)
        {
            attackChoice = Random.Range(0, 2);
            chosenAnAttack = true;

            if (moveOneCounter == 2)
            {
                attackChoice = 1;
            }
            else if (moveTwoCounter == 2)
            {
                attackChoice = 0;
            }
        }
        else if (chosenAnAttack && attackChoice == 0)
        {
            if(countBombs < amtOfBombs && hasWaitedForNextBomb)
            {
                //Instantiate(handBomb, transform.GetChild(0).position, Quaternion.identity);
                anim.SetInteger("State", 1);
                anim.Play("HandJump", -1, 0f);
                hasWaitedForNextBomb = false;
                StartCoroutine(waitForNextBomb(timeBetweenBombs));
            }
            else if(countBombs >= amtOfBombs && !waitOnce)
            {
                StartCoroutine(waitForJumpAnim(1f));
                StartCoroutine(waitBeforeNextAttack(5f));
                waitOnce = true;
                moveOneCounter = moveOneCounter + 1;
                moveTwoCounter = 0;
            }
            //Debug.Log(countBombs);
        }
        else if (chosenAnAttack && attackChoice == 1)
        {
            if(hasJumpedForSpike)
            {
                waitJumpTimer -= Time.deltaTime;
            }

            if (!hasJumpedForSpike)
            {
                anim.SetInteger("State", 1);
                hasJumpedForSpike = true;
            }
            else if (hasJumpedForSpike && waitJumpTimer <= 0 && rb.isKinematic == true && !actuallyJumped)
            {
                rb.isKinematic = false; //false allows movement
                rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
                actuallyJumped = true;
            }
            else if (hasJumpedForSpike && !waitOnce && actuallyJumped)
            {
                //rb.isKinematic = true;
                StartCoroutine(waitBeforeNextAttack(5f));
                waitOnce = true;
            }
            else if(waitOnce && !spikedBallSpawned)
            {
                if(transform.position.y > 0 && Mathf.Abs(rb.velocity.y)  < 0.5)
                {
                    Instantiate(spikedBall, transform.GetChild(1).position, Quaternion.identity);
                    spikedBallSpawned = true;
                    moveTwoCounter = moveTwoCounter + 1;
                    moveOneCounter = 0;
                }
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if(col.gameObject.tag == "Ground")
        //{
        //    rb.isKinematic = true;
       // }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground" && Mathf.Abs(rb.velocity.y) < 1)
        {
            rb.isKinematic = true;
            anim.SetInteger("State", 0);
            Debug.Log("WORKING AS INTENDED");
        }
    }

    public void giveDamage(int damageToGive)
    {
        hurtSound.Play();
        enemyHealth -= damageToGive;
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        sr.color = new Color(1, 1, 0.3f, 1);
        yield return new WaitForSeconds(0.03f);
        sr.color = new Color(1, 1, 1, 1);
    }
    IEnumerator waitForNextBomb(float time)
    {
        countBombs = countBombs + 1;
        yield return new WaitForSeconds(time);
        //anim.Play("HandJump", -1, 0f);
        Debug.Log("On creation of bomb, countBomb is:");
        Debug.Log(countBombs);
        hasWaitedForNextBomb = true;
        Instantiate(handBomb, transform.GetChild(0).position, Quaternion.identity);
    }

    IEnumerator waitForJumpAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetInteger("State", 0);
    }
    IEnumerator waitBeforeNextAttack(float time)
    {
        yield return new WaitForSeconds(time);
        actuallyJumped = false;
        waitOnce = false;
        chosenAnAttack = false;
        countBombs = 0;
        hasJumpedForSpike = false;
        waitJumpTimer = jumpTimerStart;
        spikedBallSpawned = false;
    }
}