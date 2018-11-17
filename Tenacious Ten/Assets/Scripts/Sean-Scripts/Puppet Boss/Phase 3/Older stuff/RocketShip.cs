using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float speed;

    [SerializeField] GameObject LeftBullet;
    [SerializeField] GameObject RightBullet;
    [SerializeField] GameObject Deploy;
    [SerializeField] Transform ProjectileSpawn;
    [SerializeField] float SpawnTimer;
    int shotCounter;
    private float timer;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        timer = SpawnTimer;
        shotCounter = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector3(speed, 0f, 0f);

        SpawnTimer -= Time.deltaTime;
        if(SpawnTimer <= 0)
        {
            if(shotCounter <= 5)
            {
                SpawnTimer = timer;
                Instantiate(LeftBullet, ProjectileSpawn.position, Quaternion.identity);
                Instantiate(RightBullet, ProjectileSpawn.position, Quaternion.identity);
                Instantiate(Deploy, ProjectileSpawn.position, Quaternion.identity);
            }
            shotCounter++;
        }
	}
}
