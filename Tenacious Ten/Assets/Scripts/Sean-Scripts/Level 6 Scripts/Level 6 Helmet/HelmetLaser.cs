using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetLaser : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float speed;

    Transform PlayerPos;
    
    bool readyToMove;

    [SerializeField] bool mainLaser;
    bool spawnedAdjLasers;

    [SerializeField] GameObject sideLaser;
    [SerializeField] int zAngle;
    [SerializeField] float waitTimeForSideLasers;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        PlayerPos = GameObject.Find("Player").transform;
        if(mainLaser)
        {
            transform.right = PlayerPos.position - transform.position;
        }

        spawnedAdjLasers = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(mainLaser)
        {
            rb.velocity = transform.right * speed;
            if (!spawnedAdjLasers)
            {
                spawnedAdjLasers = true;
                StartCoroutine(waitToSpawnSideLasers(waitTimeForSideLasers));
            }
        }
        else
        {
            rb.velocity = transform.right * speed;
        }


    }

    IEnumerator waitToSpawnSideLasers(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log(transform.rotation.z);

        Instantiate(sideLaser, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z + zAngle)));
        Instantiate(sideLaser, transform.position, Quaternion.Euler(new Vector3(0, 0, transform.eulerAngles.z - zAngle)));
    }
}