using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotSky : MonoBehaviour
{

	Vector2 startingPosition;
	[SerializeField]
	float TotalDamage;
	[SerializeField]
	float modifier = 1f;
	[SerializeField]
	float offset = 0f;

	// Start is called before the first frame update
	void Start()
    {
		startingPosition = transform.position;
	}

    // Update is called once per frame
	// Max distance for ChariotLevel's sky is about 90 Unity Units to the left, so may need to ratio depending on decided total enemy health
	// Remember to reset sky depending on checkpoints and death. (Not done Yet)
    void FixedUpdate()
    {
		transform.position = new Vector2(((startingPosition.x - TotalDamage)/modifier) - offset, transform.position.y);
    }
}
