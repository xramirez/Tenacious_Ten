using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiArrow : MonoBehaviour {

    Rigidbody2D rb;
    [SerializeField] float speedX;
    [SerializeField] float speedY;

    [SerializeField] bool isAngledArrow;
    [SerializeField] bool isTestArrow;

    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Bow1;
    Transform player;
    Transform bow;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        Player1 = GameObject.FindWithTag("Player");
        player = Player1.transform;

        Bow1 = GameObject.FindWithTag("Angled Bow Shot");
        bow = Bow1.transform;

        if(isTestArrow)
        {
            rb.velocity = Bow1.transform.right * speedX;
        }

        if (isAngledArrow)  //assume speedX to be +, speedY to be -
        {
            speedX = 0.8f* ((player.position.x) +  Mathf.Abs(bow.position.x));
            if(Mathf.Abs(player.position.y - bow.position.y) <= 2.65)
            {
                speedY = -0.5f * (Mathf.Abs(player.position.y) + bow.position.y);
            }
            else
            {
                speedY = -0.9f * (Mathf.Abs(player.position.y) + bow.position.y);
            }
            if(player.position.y > bow.position.y)
            {
                speedY = -speedY;
            }

            if(player.position.x < bow.position.x)
            {
                speedX = -speedX;
            }
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
            //rb.velocity = new Vector3(speedX, speedY, 0);
        
	}
}
