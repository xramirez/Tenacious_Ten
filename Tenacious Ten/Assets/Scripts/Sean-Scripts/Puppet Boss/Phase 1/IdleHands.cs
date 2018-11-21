using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleHands : MonoBehaviour {

    Vector3 StartingPos;
    //[SerializeField]PuppetBossManager BM;
    PuppetBossManager BM;
    public float idleSpeed;
    public float SwitchDirTime;
    bool hasSwitched;

    Phase1DollHealthManager LeftDoll;
    Phase1DollHealthManager RightDoll;

    [SerializeField] bool isLeftHand;
    [SerializeField] bool isRightHand;

    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        StartingPos = transform.position;
        hasSwitched = false;

        BM = FindObjectOfType<PuppetBossManager>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        sr.color = new Color(1f, 1f, 1f, sr.color.a + 0.01f);
        if (BM.phaseOneActivated && sr.color.a >= 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + idleSpeed, transform.position.z);
            if (!hasSwitched)
            {
                StartCoroutine(WaitForXSeconds(SwitchDirTime));
                hasSwitched = true;
            }

            if (GameObject.Find("Left Doll") != null || GameObject.Find("Right Doll") != null)
            {
                LeftDoll = GameObject.Find("Left Doll").GetComponent<Phase1DollHealthManager>();
                RightDoll = GameObject.Find("Right Doll").GetComponent<Phase1DollHealthManager>();
            }
            else
            {
                //Debug.Log("LOL NOT WORKING");
            }
        }
        else if(BM.phaseTwoActivated)
        {
            Destroy(gameObject);
        }

	}

    IEnumerator WaitForXSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        idleSpeed = -idleSpeed;
        hasSwitched = false;
    }
}
