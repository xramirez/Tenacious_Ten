using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //to include "Text"

public class PlayerHealthManager : MonoBehaviour {

    public static int playerHealth;

    public int maxPlayerHealth;

    Text text;

    private LevelManager levelManager;

    public bool isDead;

    public static bool isInvulnerable;
    public static bool justDamaged;

    public float invulTimer;
    public int opacityTimer;

    public bool isHurt = false;

    [SerializeField]
    AudioSource hurtSound;

    [SerializeField]
    AudioSource onSpawn;

    SpriteRenderer sr;

    public GameObject Player;

    Level1DeathMenu stopper;
    

    // Use this for initialization
    void Start() {

        text = GetComponent<Text>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        Player = GameObject.FindWithTag("Player");


        sr = GetComponent<SpriteRenderer>();

        isDead = false;

        isInvulnerable = false;
        justDamaged = false;

        invulTimer = 3f;
        opacityTimer = 0;

        stopper = FindObjectOfType<Level1DeathMenu>();
        
    }

    // Update is called once per frame
    void Update() {

        if (playerHealth <= 0 && !isDead)
        {
            //levelManager.RespawnPlayer();
            Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
            isInvulnerable = false;
            justDamaged = false;
            isDead = true;
            stopper.onSpawn.Stop();
        }

        //text.text = "" + playerHealth;

        if(justDamaged)
        {
            if(!isHurt)
            { 
                hurtSound.Play();
                isHurt = true;
            }

            invulTimer -= Time.deltaTime;
            opacityTimer++;
            if(opacityTimer % 20 == 0)
            {
                Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else if(opacityTimer % 10 == 0)
            {
                Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
            }

            if(invulTimer <= 0)
            {
                invulTimer = 3f;
                isInvulnerable = false;
                justDamaged = false;
                isHurt = false;
                Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }

        }


    }

    public static void HurtPlayer(int damage)
    {
        if (!isInvulnerable)
        {
            playerHealth -= damage;
            justDamaged = true;
        }
        isInvulnerable = true;

    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    public static int checkRemainingHealth()
    {
        return playerHealth;
    }

    public static void SetHP(int x){ playerHealth = x; }
}
