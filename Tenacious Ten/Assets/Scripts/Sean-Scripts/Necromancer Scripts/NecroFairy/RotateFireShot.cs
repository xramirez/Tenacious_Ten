using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFireShot : MonoBehaviour
{

    [SerializeField] float speed;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed; 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
