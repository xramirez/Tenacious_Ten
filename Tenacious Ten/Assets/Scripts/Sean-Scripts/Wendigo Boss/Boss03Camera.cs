using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Camera : MonoBehaviour
{

    public StartOrResetLevel SORL;

    public Vector3 StartingPos;

    public float transitionSpeed;

    PlayerHealthManager PHM;

    // Use this for initialization
    void Start()
    {
        SORL = FindObjectOfType<StartOrResetLevel>();

        StartingPos = transform.position;

        PHM = GameObject.FindObjectOfType<PlayerHealthManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SORL.StartFight)
        {
            if (transform.position.x != 0)
            {
                transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z);
            }
            if (transform.position.y != 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - transitionSpeed, transform.position.z);
            }
        }

        if (SORL.ResetFight)
        {
            //transform.localPosition = StartingPos;
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
