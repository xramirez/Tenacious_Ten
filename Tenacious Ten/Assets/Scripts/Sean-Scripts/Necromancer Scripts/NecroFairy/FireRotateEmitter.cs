using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRotateEmitter : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float RotateSpeed;

    int randomDir;

    [SerializeField] float FireRate;
    float initFireRate;

    [SerializeField] GameObject FireShot;
    [SerializeField] Transform barrel;

    FairyMove Fairy;

       
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initFireRate = FireRate;
        barrel = this.gameObject.transform.GetChild(0);

        Fairy = GameObject.FindObjectOfType<FairyMove>();
        randomDir = Random.Range(0, 2);
        if(randomDir == 0)
        {
            RotateSpeed = -RotateSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, RotateSpeed) * Time.deltaTime);

        FireRate -= Time.deltaTime;
        if(FireRate <= 0)
        {
            Instantiate(FireShot, barrel.position, barrel.rotation);
            FireRate = initFireRate;
        }
    }

    void Update()
    {
        if(Fairy.FirstDeath)
        {
            Destroy(gameObject);
        }
    }
}
