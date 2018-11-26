using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamienSkull : MonoBehaviour {

    [SerializeField] float RotateSpeed;
    GameObject Player;
    float initRotateSpeed;

    Rigidbody2D rb;

    [SerializeField] float MoveTimer;
    float initMoveTimer;
    bool hasMoved;

    float PrevPlayerPosX;
    float PrevPlayerPosY;

    [SerializeField] float MoveSpeed;
    [SerializeField] float minDistanceCheck;

    DamienAttack Damien;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        initMoveTimer = MoveTimer;
        hasMoved = false;
        MoveTimer = 0;

        initRotateSpeed = RotateSpeed;

        Damien = GameObject.FindObjectOfType<DamienAttack>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if(Damien.skullExploTimer <= 0)
        {
            Destroy(gameObject);
        }

        if(Damien.FirstDeath)
        {
            Destroy(gameObject);
        }

        //transform.Rotate(new Vector3(0, 0, RotateSpeed) * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(0f, 0f, RotateSpeed * Time.deltaTime);

        RotateSpeed = RotateSpeed + initRotateSpeed;

        MoveTimer -= Time.deltaTime;
        if(MoveTimer <= 0)
        {
            if(!hasMoved)
            {
                hasMoved = true;
                LocatePlayerOnFrame();
                transform.right = Player.transform.position - transform.position;
                //transform.up = Player.transform.up - transform.up;
                rb.velocity = transform.right * MoveSpeed;
            }
            Debug.Log(Mathf.Abs(transform.position.x - PrevPlayerPosX));
            Debug.Log("^^ That's X difference ^^");
            Debug.Log(Mathf.Abs(transform.position.y - PrevPlayerPosY));
            Debug.Log("^^ That's Y difference ^^");

            if (Mathf.Abs(transform.position.x - PrevPlayerPosX) < minDistanceCheck && Mathf.Abs(transform.position.y - PrevPlayerPosY) < minDistanceCheck)//if(PrevPlayerPos && transform.position == PrevPlayerPos.position)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                MoveTimer = initMoveTimer;
                hasMoved = false;
            }
            
        }


    }

    void LocatePlayerOnFrame()
    {
        PrevPlayerPosX = Player.transform.position.x;
        PrevPlayerPosY = Player.transform.position.y;

    }
}
