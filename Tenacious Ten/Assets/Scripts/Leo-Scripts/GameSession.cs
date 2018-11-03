using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    public GameObject currentCheckpoint;
    private PlayerManager player;

    private void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        Debug.Log("Current Lives: " + playerLives);
    }

    public void RespawnPlayer(){
        print("Player Respawned");
        player.transform.position = currentCheckpoint.transform.position;
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives>1){
            RespawnPlayer();
            //TakeLife();
        }
        else{
            ResetGameSession();
        }
    }
    private void TakeLife(){
        playerLives--;
        Debug.Log("Current Lives: " + playerLives);
        //var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
    }
    private void ResetGameSession(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level Has Been Reset");
        Destroy(gameObject);
    }

}
