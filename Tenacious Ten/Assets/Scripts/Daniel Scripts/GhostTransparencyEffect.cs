using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTransparencyEffect : MonoBehaviour
{
	[SerializeField]
	float upper;
	[SerializeField]
	float lower;
	Color startColor;
	Color current;
	bool reverse;

    // Start is called before the first frame update
    void Start()
    {
		startColor = GetComponent<SpriteRenderer>().color;
		current = startColor;
		reverse = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		GetComponent<SpriteRenderer>().color = current;
		if (current.a < lower)
		{
			reverse = true;
		}
		else if(current.a > upper)
		{
			reverse = false;
		}
		if(reverse)
		{
			current.a += 0.01f;
		}
		else
		{
			current.a -= 0.01f;
		}
		Debug.Log("reverse: " + reverse + " Current: " + current);
	}
}
