using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoLevel1BossScene : MonoBehaviour
{
	string scenename = "Boss Fight 01";

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(scenename);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(scenename);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(scenename);
			//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}