using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Animator animator;
    public Transform firePoint;
    public GameObject lemonBulletPreFab;
    public AudioSource swingSound;

	// Update is called once per frame
	void Update () {
        //animator.SetBool("IsShooting", false);
        if (Input.GetButtonDown("Fire1")){
            swingSound.Play();
            StartCoroutine(Shoot());
            //animator.SetBool("IsShooting", true);
        }
    }

    public IEnumerator Shoot () {
        //shoot logic
        animator.SetBool("IsShooting", true);
        Instantiate(lemonBulletPreFab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("IsShooting", false);
    }


}
