using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransition : MonoBehaviour {

    Animator anim;
    bool haveNotReachedChest;
    bool hasSwung;
    [SerializeField] float moveValue;

    bool once = true;

    bool hasJumped = false;
    [SerializeField] float JumpForce;

    [SerializeField] float shrinkSpeed;

    LockedChestMove Chest;

    Rigidbody2D rb;

    public bool PlayerInChest;

    [SerializeField]
    AudioSource swingSound;

    [SerializeField]
    AudioSource warpSound;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        haveNotReachedChest = true;
        hasSwung = false;

        Chest = GameObject.FindObjectOfType<LockedChestMove>();

        rb = GetComponent<Rigidbody2D>();

        PlayerInChest = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(haveNotReachedChest && !hasSwung)
        {
            transform.position = new Vector3(transform.position.x + moveValue, transform.position.y, 0f);
            if(transform.position.x >= 9f)
            {
                haveNotReachedChest = false;
            }
        }
        else if (!haveNotReachedChest && !hasSwung)
        {
            anim.SetInteger("State", 1);
            StartCoroutine(waitForSwingAnim(1f));
        }
        else if(hasSwung)
        {
            if (once)
                swingSound.Play();
            once = false;
            StartCoroutine(waitForIdleAnim(0.25f));
        }


        if(Chest.ChestOpen)
        {
            if(!hasJumped)
            {
                warpSound.Play();
                hasJumped = true;
                rb.AddForce(new Vector2(rb.velocity.x, JumpForce));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x - shrinkSpeed, transform.localScale.y - shrinkSpeed, 0f);
            }

            if(transform.localScale.x <= 5 && hasJumped && transform.position.y <= -1.05f)
            {
                Destroy(gameObject);
                PlayerInChest = true;
			}
        }
	}
	IEnumerator waitForSwingAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetInteger("State", 2);
        hasSwung = true;
    }

    IEnumerator waitForIdleAnim(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetInteger("State", 1);
    }
}
