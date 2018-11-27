using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroTrackingSword : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed;

    //[SerializeField] GameObject Bow1;
    //GameObject Bow1;

    Transform PlayerPos;

    SpriteRenderer sr;
    bool readyToMove;

    PolygonCollider2D PC;

    [SerializeField] AudioSource wooshSound;

    // Use this for initialization
    void Start()
    {
        readyToMove = false;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        PC = GetComponent<PolygonCollider2D>();
        PC.enabled = false;
        sr.color = new Color(1f, 1f, 1f, 0f);

        PlayerPos = GameObject.Find("Player").transform;

        transform.right = PlayerPos.position - transform.position;
        
        //transform.rotation = Quaternion.Euler(0f,0f,transform.rotation.z + 180);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(sr.color.a <= 1f)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a+0.01f);
            transform.right = PlayerPos.position - transform.position;
        }
        else
        {
            readyToMove = true;
        }

        if(readyToMove)
        {
            PC.enabled = true;
            rb.velocity = transform.right * speed;
            wooshSound.Play();
        }

    }
}
