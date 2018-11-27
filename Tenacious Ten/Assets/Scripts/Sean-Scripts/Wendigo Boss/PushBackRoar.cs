using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBackRoar : MonoBehaviour {

    public Boss03Manager Wendigo;
    public PlayerManager player;
    public Boss03Phase3 P3;
    public GameObject LeftWind;
    public GameObject RightWind;
    public Transform LeftPos;
    public Transform RightPos;
    float windTimer;
    public bool playerIsIdle;


    // Use this for initialization
    void Start () {
        //Wendigo = GetComponent<BossManager>();
        //player = GetComponent<PlayerManager>();
        //P2 = GetComponent<Phase2>();
        windTimer = 0.5f;
        playerIsIdle = true;

    }
	
	// Update is called once per frame
	void Update () {
        windTimer -= Time.deltaTime;
        if(P3.pushBackTimer > 0.5f && windTimer <= 0 && P3.isPushingBack)
        {
            if(Wendigo.facingLeft)
            {
                Instantiate(LeftWind, LeftPos.position, Quaternion.identity);
            }
            else if(!Wendigo.facingLeft)
            {
                Instantiate(RightWind, RightPos.position, Quaternion.identity);
            }
            windTimer = 0.5f;
        }
    }

    public void Roar()
    {
        if (Wendigo.facingLeft && P3.isPushingBack)
        {
            if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D))
            {
                player.anim.SetInteger("State", 2);
                player.speed = 3;
                //playerIsIdle = false;
            }
            //if (Input.GetKeyUp(KeyCode.RightArrow))
            //{
               // player.anim.SetInteger("State", 0);
               // player.speed = -6;
              //  Debug.Log("Key code up RIGHT");
            //}
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                player.anim.SetInteger("State", 2);
                player.speed = -9;
                //playerIsIdle = false;
            }
            //if (Input.GetKeyUp(KeyCode.LeftArrow))
            //{
            //    player.anim.SetInteger("State", 0);
            //    player.speed = -6;
            //    Debug.Log("Key code up LEFT");
            //}
            else if (playerIsIdle)
            {
                player.speed = -6;
            }
        }
        else if(!Wendigo.facingLeft && P3.isPushingBack)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                player.anim.SetInteger("State", 2);
                player.speed = 9;
                //playerIsIdle = false;
            }
           // if (Input.GetKeyUp(KeyCode.RightArrow))
            //{
            //    player.anim.SetInteger("State", 0);
             //   player.speed = 6;
            //}
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                player.anim.SetInteger("State", 2);
                player.speed = -3;
                //playerIsIdle = false;
            }
            //if (Input.GetKeyUp(KeyCode.LeftArrow))
            //{
            //    player.anim.SetInteger("State", 0);
            //    player.speed = 6;
            //}
            else if (playerIsIdle)
            {
                player.speed = 6;
            }
        }
    }
}
