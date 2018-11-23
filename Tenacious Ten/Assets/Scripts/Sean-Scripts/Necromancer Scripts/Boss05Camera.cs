using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss05Camera : MonoBehaviour
{

    public StartOrResetLevel SORL;

    public Vector3 StartingPos;

    public float transitionSpeed;

    NecroBossManager NBM;

    PlayerHealthManager PHM;

    // Use this for initialization
    void Start()
    {
        SORL = FindObjectOfType<StartOrResetLevel>();

        NBM = FindObjectOfType<NecroBossManager>();

        StartingPos = transform.localPosition;

        PHM = FindObjectOfType<PlayerHealthManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SORL.StartFight)
        {
            if (transform.position.y > 15)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - transitionSpeed, transform.position.z);
            }
        }

        if(SORL.StartFight && NBM.Phase2Activated)
        {
            if (transform.position.y > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - transitionSpeed * 2, transform.position.z);
            }
        }
        

    }

    void Update()
    {
        if (!SORL.StartFight && !PHM.isDead)
        {
            transform.position = StartingPos;
        }
    }
}