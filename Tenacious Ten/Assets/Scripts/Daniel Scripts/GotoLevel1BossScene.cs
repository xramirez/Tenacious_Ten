using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoLevel1BossScene : MonoBehaviour
{
	string scenename = "Boss Fight 01";

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(scenename);
		}
	}
}