using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToCredits : MonoBehaviour {
	public void goToCreditsScene () {
        SceneManager.LoadScene("Credits");
	}
}
