using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1DeathMenu : MonoBehaviour
{
	private LevelManager levelManager;
    //public static bool GameIsPaused = false;

    [SerializeField]
    public AudioSource onSpawn;

	public GameObject deathMenuUI;

	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		deathMenuUI.SetActive(false);

	}

	// Update is called once per frame
	void Update()
	{
		if (PlayerHealthManager.playerHealth <= 0)
		{
			deathMenuUI.SetActive(true);
			Time.timeScale = 0f;
		}

	}

	public void Respawn()
	{
        onSpawn.Stop();
        onSpawn.Play();
		levelManager.RespawnPlayer();
		Debug.Log("Spawned after death.");
		deathMenuUI.SetActive(false);
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Debug.Log("Quitting game");
		Application.Quit();
	}
}
