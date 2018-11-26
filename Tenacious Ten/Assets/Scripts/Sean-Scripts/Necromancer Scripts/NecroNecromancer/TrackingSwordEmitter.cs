using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingSwordEmitter : MonoBehaviour {

    [SerializeField] int SwordsToCast;
    [SerializeField] GameObject TrackingSword01;
    [SerializeField] GameObject TrackingSword02;
    [SerializeField] GameObject TrackingSword03;
    [SerializeField] GameObject TrackingSword04;
    [SerializeField] GameObject TrackingSword05;
    [SerializeField] GameObject TrackingSword06;
    [SerializeField] float castTimer;
    float initCastTimer;

    int locationChoice;

    int swordCounter;
    
    [SerializeField] AudioSource PhaseInSwordSound;

    // Use this for initialization
    void Start () {
        initCastTimer = castTimer;
        swordCounter = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        castTimer -= Time.deltaTime;
        if(castTimer <= 0 && swordCounter < SwordsToCast)
        {
            swordCounter++;
            castTimer = initCastTimer;
            locationChoice = Random.Range(0, 6);
            if(locationChoice == 0)
            {
                Instantiate(TrackingSword01, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (locationChoice == 1)
            {
                Instantiate(TrackingSword02, transform.GetChild(1).position, Quaternion.identity);
            }
            else if (locationChoice == 2)
            {
                Instantiate(TrackingSword03, transform.GetChild(2).position, Quaternion.identity);
            }
            else if (locationChoice == 3)
            {
                Instantiate(TrackingSword04, transform.GetChild(3).position, Quaternion.identity);
            }
            else if (locationChoice == 4)
            {
                Instantiate(TrackingSword05, transform.GetChild(4).position, Quaternion.identity);
            }
            else if (locationChoice == 5)
            {
                Instantiate(TrackingSword06, transform.GetChild(5).position, Quaternion.identity);
            }
            PhaseInSwordSound.Play();

        }

        if(swordCounter >= SwordsToCast)
        {
            Destroy(gameObject);
        }


    }
}
