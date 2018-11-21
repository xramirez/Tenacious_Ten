using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepAttackEmitter : MonoBehaviour
{

    [SerializeField] int SweepsToCast;
    [SerializeField] GameObject SweepAttackTL;
    [SerializeField] GameObject SweepAttackTR;
    [SerializeField] GameObject SweepAttackML;
    [SerializeField] GameObject SweepAttackMR;
    [SerializeField] GameObject SweepAttackBL;
    [SerializeField] GameObject SweepAttackBR;
    [SerializeField] float castTimer;
    float initCastTimer;

    int locationChoice;

    int sweepCounter;

    // Use this for initialization
    void Start()
    {
        initCastTimer = castTimer;
        sweepCounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        castTimer -= Time.deltaTime;
        if (castTimer <= 0 && sweepCounter < SweepsToCast)
        {
            sweepCounter++;
            castTimer = initCastTimer;
            locationChoice = Random.Range(0, 6);
            if (locationChoice == 0)
            {
                Instantiate(SweepAttackTL, transform.GetChild(0).position, Quaternion.identity);
            }
            else if (locationChoice == 1)
            {
                Instantiate(SweepAttackTR, transform.GetChild(1).position, Quaternion.identity);
            }
            else if (locationChoice == 2)
            {
                Instantiate(SweepAttackML, transform.GetChild(2).position, Quaternion.identity);
            }
            else if (locationChoice == 3)
            {
                Instantiate(SweepAttackMR, transform.GetChild(3).position, Quaternion.identity);
            }
            else if (locationChoice == 4)
            {
                Instantiate(SweepAttackBL, transform.GetChild(4).position, Quaternion.identity);
            }
            else if (locationChoice == 5)
            {
                Instantiate(SweepAttackBR, transform.GetChild(5).position, Quaternion.identity);
            }

        }

        if (sweepCounter >= SweepsToCast)
        {
            Destroy(gameObject);
        }


    }
}
