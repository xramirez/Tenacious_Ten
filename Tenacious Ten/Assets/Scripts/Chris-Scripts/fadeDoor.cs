using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeDoor : MonoBehaviour
{
    public GameObject Door;
    SpriteRenderer sr;
    float time = 1.5f;
    float alpha = 0.0f;

    void Start()
    {
        sr = Door.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("Trigger_Enter.");
            Door.GetComponent<AudioSource>().volume = 0.3f;
            Door.GetComponent<AudioSource>().Play();
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        float currentAlpha = sr.color.a;
        for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(currentAlpha, alpha, t));
            if(t >= 0 && t <= 0.3f)
            {
                Door.GetComponent<AudioSource>().volume = t;
            }
            sr.color = newColor;
            yield return null;
        }
        Door.GetComponent<AudioSource>().Stop();
        Destroy(Door);
    }
}
