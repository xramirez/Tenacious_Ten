using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshFallLevelFive : MonoBehaviour
{

    [SerializeField] float RandomTimeL;
    [SerializeField] float RandomTimeR;
    [SerializeField] GameObject AshFall;
    [SerializeField] public bool ashFlipInAir;
    bool emitTimeSet;
    float emitTimer;

    GameObject Player;
    float playerLocX;
    float playerLocY;

    // Use this for initialization
    void Start()
    {
        emitTimeSet = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerLocX = Player.transform.position.x;
        Debug.Log(playerLocX);
        playerLocY = Player.transform.position.y;
        transform.position = new Vector3(Random.Range(playerLocX - 15f, playerLocX + 23f), playerLocY + 7f, 0f);
        //transform.position = new Vector3(transform.position.x, Random.Range(-10.4f, 10.4f), 0f);

        if (!emitTimeSet)
        {
            emitTimeSet = true;
            emitTimer = Random.Range(RandomTimeL, RandomTimeR);
        }
        emitTimer -= Time.deltaTime;
        if (emitTimer <= 0)
        {
            emitTimeSet = false;
            Instantiate(AshFall, transform.position, Quaternion.identity);
        }

    }
}
