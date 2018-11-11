using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOrResetLevel : MonoBehaviour
{
    private PlayerManager player;

    public bool StartFight, ResetFight;

    // Use this for initialization
    void Start()
    {
        StartFight = false;
        ResetFight = false;
    }

    // Update is called once per frame
    void Update()
    {

        ResetFight = false;
        if (PlayerHealthManager.checkRemainingHealth() < 1)
        {
            ResetFight = true;
            StartFight = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartFight = true;
        }
    }
}
