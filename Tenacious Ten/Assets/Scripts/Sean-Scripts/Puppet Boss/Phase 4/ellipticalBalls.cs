using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ellipticalBalls : MonoBehaviour {
    Rigidbody2D rb;
    float alpha;
    public float RotationSpeed;
    public float MoveSpeed;
    [SerializeField] float OffsetY;
    [SerializeField] float OffsetX;
    public static float moveSpeedX;
    public float moveSpeedY;
    [SerializeField] float diamaterX;
    [SerializeField] float diamaterY;

    [SerializeField] bool rightBall;
    [SerializeField] bool leftBall;
    [SerializeField] bool downBall;

    public static bool moveBalls;

    StartOrResetLevel SORL;

    SpriteRenderer sr;

    float opacityCounter;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0);
        opacityCounter = 0;


        rb = GetComponent<Rigidbody2D>();
        SORL = FindObjectOfType<StartOrResetLevel>();
        moveSpeedX = 0.075f;
        if(rightBall)
        {
            alpha = 90;
        }
        else if(downBall)
        {
            alpha = 180;
        }
        else if(leftBall)
        {
            alpha = 270;
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(OffsetX + (diamaterX * Mathf.Sin(Mathf.Deg2Rad * alpha)), OffsetY + (diamaterY * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        alpha += MoveSpeed;//can be used as speed

        if(moveBalls)
        {
            OffsetX = OffsetX + moveSpeedX;
            OffsetY = OffsetY + moveSpeedY;
        }
        opacityCounter = opacityCounter + 0.01f;
        sr.color = new Color(1, 1, 1, opacityCounter);
    }
    public static void changeMoveBalls()
    {
        moveBalls = !moveBalls;
    }
}