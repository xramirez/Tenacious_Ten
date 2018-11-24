using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOrResetLevel : MonoBehaviour
{
    private PlayerManager player;
    public bool StartFight, ResetFight;
    public int counter;

    [SerializeField]
    AudioSource music;

    // Use this for initialization
    void Start()
    {
        counter = 0;
        StartFight = false;
        ResetFight = false;
    }

    // Update is called once per frame
    void Update()
    {

        ResetFight = false;
        if (PlayerHealthManager.checkRemainingHealth() < 1)
        {
            music.Stop();
            ResetFight = true;
            counter = 0;
            StartFight = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (counter <= 0)
            {
                counter++;
                music.Play();
            }
            StartFight = true;
        }
    }
}
