using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedChestMove : MonoBehaviour {

    //[SerializeField] float randomValueLeft;
    //[SerializeField] float randomValueRight;
    [SerializeField] float moveDist;
    bool DistanceSet;

    bool movingLeft;

    bool chestMove;

    Animator anim;
    bool pauseMove;

    public bool ChestOpen;

    SpriteRenderer Chains;

    PlayerTransition Player;

    // Use this for initialization
    void Start () {

        chestMove = true;

        anim = GetComponent<Animator>();
        anim.enabled = false;

        DistanceSet = false;
        movingLeft = true;
        pauseMove = false;

        ChestOpen = false;

        Chains = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        Player = GameObject.FindObjectOfType<PlayerTransition>();
    }
	
	// Update is called once per frame
	void Update () {

        if(chestMove)
        {
            RattleChest();
        }

        if(Player.PlayerInChest)
        {
            anim.SetInteger("State", 1);
        }
        
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            chestMove = false;
            StartCoroutine(waitToDestroyChains(2f));
            if (Chains.color.a <= 0)
            {
                anim.enabled = true;
                if (Chains.color.a <= -0.2)
                {
                    ChestOpen = true;
                }
            }
        }
    }

    IEnumerator waitToDestroyChains(float time)
    {
        yield return new WaitForSeconds(time);
        Chains.color = new Color(1f, 1f, 1f, Chains.color.a - 0.01f);
    }

    void RattleChest()
    {
        if (pauseMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        else if (movingLeft)
        {
            transform.position = new Vector3(transform.position.x - moveDist, transform.position.y, 0f);
            if (transform.position.x <= 9f)
            {
                movingLeft = false;
            }
        }
        else if (!movingLeft)
        {
            transform.position = new Vector3(transform.position.x + moveDist, transform.position.y, 0f);
            if (transform.position.x >= 9.5f)
            {
                movingLeft = true;
            }
        }
    }
}
