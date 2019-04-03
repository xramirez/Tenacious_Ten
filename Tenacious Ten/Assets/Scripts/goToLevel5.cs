using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLevel5 : MonoBehaviour {

	// Use this for initialization
	void Start() {
		Debug.Log("Moving to level 5...");
		Invoke("goToScene5", 10);
	}
	void goToScene5()
	{
		SceneManager.LoadScene("Level_5.0");
	}
}
