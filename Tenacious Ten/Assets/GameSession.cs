using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;

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

    }

    public void ProcessPlayerDeath()
    {
        if(playerLives>1){
            TakeLife();
        }
        else{
            ResetGameSession();
        }
    }
    private void TakeLife(){
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void ResetGameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
