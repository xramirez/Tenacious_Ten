using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamArrowNew : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float AngledArrowSpeed;
    [SerializeField] float StraightArrowSpeed;

    [SerializeField] bool isAngledArrow;

    //[SerializeField] GameObject Bow1;
    GameObject Bow1;
    Transform bow;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Bow1 = GameObject.Find("Angled Bow Shot");

        if (isAngledArrow)
        {
            rb.velocity = Bow1.transform.right * AngledArrowSpeed;
        }
        else
        {
            rb.velocity = new Vector3(StraightArrowSpeed, 0f, 0f);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
