using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteTrail : MonoBehaviour
{
    List<GameObject> trailParts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0, 0.1f); //repeats function every 0.2 seconds
    }

    // Update is called once per frame
    void SpawnTrailPart()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        trailPart.transform.localScale = transform.localScale;
        trailParts.Add(trailPart);

        StartCoroutine(FadeTrailPart(trailPartRenderer));
        StartCoroutine(DestroyTrailPart(trailPart, 0.2f));
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f;
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);

        trailParts.Remove(trailPart);
        Destroy(trailPart);
    }
}
