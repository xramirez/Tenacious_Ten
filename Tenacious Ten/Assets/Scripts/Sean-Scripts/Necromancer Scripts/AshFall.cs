using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshFall : MonoBehaviour {

    Rigidbody2D rb;
    float randomSizeValue;
    SpriteRenderer sr;

    float randomScaleValue;

    bool scaleNegative;

    AshFallEmitter AFE;

    float initialYScale;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        randomSizeValue = Random.Range(0.3f, 1f);

        transform.localScale = new Vector3(randomSizeValue, randomSizeValue, transform.localScale.z);

        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, randomSizeValue);

        scaleNegative = false;

        if(randomSizeValue >= 0.65f)
        {
            randomScaleValue = (randomSizeValue * 0.1f) - 0.02f;
        }
        else
        {
            randomScaleValue = (randomSizeValue * 0.1f);
        }
        //0.01f;//Random.Range(0.01f, 0.02f);

        AFE = GameObject.FindObjectOfType<AshFallEmitter>();
        initialYScale = transform.localScale.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(AFE.ashFlipInAir)
        {
            FlipInAir();
        }
        //FlipInAir();
        transform.position = new Vector3(transform.position.x, (transform.position.y - randomSizeValue * 0.07f), 0f);
        //transform.position = new Vector3(transform.position.x + randomSizeValue * 0.07f, transform.position.y, 0f);

    }

    void FlipInAir()
    {
        if (!scaleNegative)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - randomScaleValue, 0f);
            if (transform.localScale.y <= -initialYScale)//-0.97f
            {
                scaleNegative = true;
            }
        }

        else if (scaleNegative)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + randomScaleValue, 0f);
            if (transform.localScale.y >= initialYScale)
            {
                scaleNegative = false;
            }
        }

    }
}
