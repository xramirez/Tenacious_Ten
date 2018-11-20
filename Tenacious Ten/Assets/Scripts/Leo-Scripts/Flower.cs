using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    [SerializeField]
    GameObject FlowerBullet;

    [SerializeField]
    AudioSource shootSound;

	[SerializeField]
    float fireRate;
    float nextFire;

	// Use this for initialization
	void Start () {
        nextFire = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfTimeToFire();
	}
    void CheckIfTimeToFire(){
        if(Time.time>nextFire){
            Instantiate(FlowerBullet, transform.position, Quaternion.identity);
            shootSound.Play();
            nextFire = Time.time + fireRate;
        }
    }
}
