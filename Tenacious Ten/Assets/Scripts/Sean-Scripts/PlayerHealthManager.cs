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

    SpriteRenderer sr;

    public PlayerManager Player;
    

    // Use this for initialization
    void Start() {

        text = GetComponent<Text>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        Player = FindObjectOfType<PlayerManager>();


        sr = GetComponent<SpriteRenderer>();

        isDead = false;

        isInvulnerable = false;
        justDamaged = false;

        invulTimer = 3f;
        opacityTimer = 0;
        
    }

    // Update is called once per frame
    void Update() {

        if (playerHealth <= 0 && !isDead)
        {
            //levelManager.RespawnPlayer();
            Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.25f);
            isInvulnerable = false;
            justDamaged = false;
            //isDead = true;
        }

        text.text = "" + playerHealth;

        if(justDamaged)
        {
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

}
