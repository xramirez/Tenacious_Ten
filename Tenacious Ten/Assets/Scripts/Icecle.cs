using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icecle : MonoBehaviour {

    [SerializeField]
    GameObject FallingIcecle;
    [SerializeField]
    float fireRate;
    float nextfire;
	// Use this for initialization
	void Start () {
        nextfire = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfTimeToFire();
	}
    void CheckIfTimeToFire()
    {
        if (Time.time > nextfire)
        {
            Instantiate(FallingIcecle, transform.position, Quaternion.identity);
            //shootSound.Play();
            nextfire = Time.time + fireRate;
        }
    }
}
