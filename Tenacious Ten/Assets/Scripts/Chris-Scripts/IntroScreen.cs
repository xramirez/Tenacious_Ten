using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour {

    public SpriteRenderer Tenacious;
    public SpriteRenderer Presents;
    public SpriteRenderer Citruscide;
    private float fadeout = 0.0f;
    private float fadein = 1.0f;
    bool done = false;

    //WE DID IT BOIS. VANILLA COMMIT.

    // Use this for initialization
    void Start () {
        Tenacious.enabled = false;
        Presents.enabled = false;
        Citruscide.enabled = false;

        StartCoroutine(WaitFadeIn(0, 1, 1.0f, Tenacious));
        StartCoroutine(WaitFadeIn(1, 2, 1.0f, Presents));

        StartCoroutine(WaitFadeOut(4f, 0, 1.0f, Tenacious));
        StartCoroutine(WaitFadeOut(4f, 0, 1.0f, Presents));

        StartCoroutine(WaitFadeIn(4.9f, 4, 1.0f, Citruscide));
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    IEnumerator WaitLoadScene(float beforeSeconds, float afterSeconds)
    {
        yield return new WaitForSeconds(beforeSeconds);
        SceneManager.LoadScene("MainMenu");
        yield return new WaitForSeconds(afterSeconds);
    }

    IEnumerator WaitFadeOut(float beforeSeconds, float afterSeconds, float time, SpriteRenderer sr)
    {
        yield return new WaitForSeconds(beforeSeconds);
        StartCoroutine(FadeOut(time, sr));
        yield return new WaitForSeconds(afterSeconds);
    }

    IEnumerator WaitFadeIn(float beforeSeconds, float afterSeconds, float time, SpriteRenderer sr)
    {
        yield return new WaitForSeconds(beforeSeconds);
        StartCoroutine(FadeIn(time, sr));
        yield return new WaitForSeconds(afterSeconds);
        if(sr == Citruscide)
        {
            Debug.Log("Enter");
            SceneManager.LoadScene("MainMenu");
        }
    }
    
    
    IEnumerator FadeOut(float time, SpriteRenderer sr)
    {
        for (float i = time; i>= 0; i -= Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, i);
            sr.color = newColor;
            yield return null;
        }
    }

    IEnumerator FadeIn(float time, SpriteRenderer sr)
    {
        sr.enabled = true;
        float currentAlpha = sr.color.a;
        for (float x = 0; x <= 1; x += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, x);
            sr.color = newColor;
            yield return null;
        }
    }
}
