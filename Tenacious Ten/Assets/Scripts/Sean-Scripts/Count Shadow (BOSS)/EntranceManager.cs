using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour
{


    public StartOrResetLevel SORL;

    Vector3 StartingPos;

    float change;

    // Use this for initialization
    void Start()
    {
        SORL = FindObjectOfType<StartOrResetLevel>();

        StartingPos = transform.localPosition;
        change = 0.08f;

    }

    // Update is called once per frame
    void Update()
    {

        if (SORL.StartFight)
        {
            if (transform.localPosition.y >= 2.84f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - change, transform.position.z);
            }
        }
        if (SORL.ResetFight)
        {
            transform.localPosition = StartingPos;
        }

    }
}
