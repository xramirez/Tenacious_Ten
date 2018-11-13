using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Explosion : MonoBehaviour {

    CapsuleCollider2D CC;
    int incre;
    [SerializeField] float firstValue;
    [SerializeField] float secondValue;
    [SerializeField] float thirdValue;
    [SerializeField] float fourthValue;
    [SerializeField] float fifthValue;

    Animation an;

    // Use this for initialization
    void Start () {
        CC = GetComponent<CapsuleCollider2D>();
        incre = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        incre++;
        if(incre == 8)
        {
            CC.size = new Vector2(CC.size.x,CC.size.y * firstValue);
        }
        if (incre == 14)
        {
            CC.size = new Vector2(CC.size.x, CC.size.y * secondValue);
        }
        if (incre == 21)
        {
            CC.size = new Vector2(CC.size.x, CC.size.y * thirdValue);
        }
        if (incre == 27)
        {
            CC.size = new Vector2(CC.size.x, CC.size.y * fourthValue);
        }
        if (incre == 33)
        {
            CC.size = new Vector2(CC.size.x, CC.size.y * fifthValue);
        }
    }
}
