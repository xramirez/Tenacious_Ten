using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    public int Health = 4;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Health<=0){
            Destroy(gameObject);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "background")
        {
            print(collision.name);
        }
        if(collision.name=="Lemon Bullet(Clone)"){
            Health -= 1;
        }
    }
}
