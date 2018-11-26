using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBoss4 : MonoBehaviour
{
    StartOrResetLevel SORL;
    Animator anim;
    BoxCollider2D bc;
    string scenename = "Boss Fight 04";


    // Use this for initialization
    void Start()
    {
        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        anim.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (SORL.StartFight)
        {
            anim.enabled = true;
            StartCoroutine(waitForTrigger(1.75f));
        }
    }

    IEnumerator waitForTrigger(float time)
    {
        yield return new WaitForSeconds(time);
        bc.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("hello");
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scenename);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("hello");
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scenename);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        print("hello");
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(scenename);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
