using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroSweepAttack : MonoBehaviour {

    PolygonCollider2D PC;
    SpriteRenderer sr;

    bool chargingUp;
    [SerializeField] float chargeUpIncrement;

    int blinkIncrement;

    [SerializeField]
    AudioSource SweepAttackSound;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        PC = GetComponent<PolygonCollider2D>();
        blinkIncrement = 0;
        sr.color = new Color(1f, 1f, 1f, 0f);
        chargingUp = false;
        PC.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(!chargingUp)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a + chargeUpIncrement);
            if (sr.color.a >= 1f)
            {
                chargingUp = true;
            }
        }
        else
        {
            StartCoroutine(flash());
        }

        if(blinkIncrement >= 2)
        {
            PC.enabled = true;
            SweepAttackSound.Play();
        }

        if(blinkIncrement >= 5)
        {
            Destroy(gameObject);
        }

	}

    IEnumerator flash()
    {
        sr.color = new Color(1, 1, 1, 0.2f);
        yield return new WaitForSeconds(0.03f);
        blinkIncrement++;
        sr.color = new Color(1, 1, 1, 1);
    }
}
