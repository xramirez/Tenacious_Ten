using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase6Lightning : MonoBehaviour
{
    [SerializeField] int moveSpeedAngle;
    [SerializeField] float timeUntilMove;
    Rigidbody2D rb;
    BoxCollider2D BC;
    int directionRandomizer;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
        directionRandomizer = Random.Range(0, 2);
        if(directionRandomizer == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 70);
        }
        else if (directionRandomizer == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 116);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeUntilMove -= Time.deltaTime;
        if(timeUntilMove <= 0)
        {
            BC.enabled = true;
            transform.Rotate(new Vector3(0, 0, moveSpeedAngle) * Time.deltaTime);
        }
    }
}
