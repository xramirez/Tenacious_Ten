using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss04Camera : MonoBehaviour
{

    public StartOrResetLevel SORL;

    public Vector3 StartingPos;

    public float transitionSpeed;

    PlayerHealthManager PHM;

    // Use this for initialization
    void Start()
    {
        SORL = FindObjectOfType<StartOrResetLevel>();

        StartingPos = transform.localPosition;

        PHM = GameObject.FindObjectOfType<PlayerHealthManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SORL.StartFight)
        {
            if (transform.position.x < -(transitionSpeed - 0.01f))
            {
                transform.position = new Vector3(transform.position.x + transitionSpeed, transform.position.y, transform.position.z);
            }
            if (transform.position.y > (transitionSpeed - 0.01f))
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
