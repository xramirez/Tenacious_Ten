using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourToFiveTrans : MonoBehaviour {


    [SerializeField]
    AudioSource warpSound;

    [SerializeField]
    AudioSource roarSound;

    bool once = true;

    Animator anim;
    bool beamOut;
    bool PlayerOut;
    [SerializeField] GameObject Beam;
    [SerializeField] GameObject Bunjiman;
    [SerializeField] float WaitTimeToSpoutPlayer;
    bool endSceneNow;
	public GameObject ground;
    SpriteRenderer BlackScreen;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        beamOut = false;
        PlayerOut = false;
        endSceneNow = false;

        BlackScreen = transform.GetChild(1).GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        
        StartCoroutine(waitUnlockChest(1.5f));

        if(anim.enabled)
        {

            WaitTimeToSpoutPlayer -= Time.deltaTime;
            if (WaitTimeToSpoutPlayer <= 1 && !beamOut)
            {
                roarSound.Play();
                Instantiate(Beam, transform.GetChild(0).position, Quaternion.identity);
                beamOut = true;
            }

            if(WaitTimeToSpoutPlayer <= 0 && !PlayerOut)
            {
                PlayerOut = true;
                if (once)
                    warpSound.Play();
                once = false;
                Instantiate(Bunjiman, transform.GetChild(0).position, Quaternion.identity);
                StartCoroutine(waitToEndScene(3.25f));
            }
        }

        if(endSceneNow)
        {
            BlackScreen.color = new Color(1f,1f,1f,BlackScreen.color.a + 0.005f);

			Debug.Log("Moving to level 5...");
			Invoke("byeGround", 3f);
			Invoke("goToScene5", 4);
		}

	}
	void byeGround()
	{
		ground = GameObject.FindGameObjectWithTag("Ground");
		ground.SetActive(false);
	}
	void goToScene5()
	{
		SceneManager.LoadScene("Level_5.0");
	}

	IEnumerator waitUnlockChest(float time)
    {
        yield return new WaitForSeconds(time);
        anim.enabled = true;
    }

    IEnumerator waitToEndScene(float time)
    {
        yield return new WaitForSeconds(time);
        endSceneNow = true;
    }
}
