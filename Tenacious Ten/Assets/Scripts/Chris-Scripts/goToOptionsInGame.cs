using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToOptionsInGame : MonoBehaviour {
    public static bool Leave;
    public GameObject HidePause;
    private Scene current;
    void Start()
    {
        current = SceneManager.GetActiveScene();
        Debug.Log("Holding Scene: " + current.ToString());

    }

	public void goToOptionsInGameScene(){
        /*
        AsyncOperation asyncOp = new AsyncOperation();
        asyncOp = SceneManager.LoadSceneAsync("OptionsInGame", LoadSceneMode.Additive);
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            Debug.Log("Async Progress: " + asyncOp.progress);
        }
        asyncOp.allowSceneActivation = true;

        Scene options = SceneManager.GetSceneByName("OptionsInGame");
        if(options.IsValid())
        SceneManager.SetActiveScene(options);
        //SceneManager.UnloadScene(options);
        while (!Leave){;}
        SceneManager.UnloadSceneAsync(options);
        */
        AsyncOperation AO = SceneManager.LoadSceneAsync("OptionsInGame", LoadSceneMode.Additive);
        Scene options = SceneManager.GetSceneByName("OptionsInGame");
        /*
        Debug.Log("Number of children: " + HidePause.transform.childCount);
        Renderer[] rendComponents = new Renderer[HidePause.transform.childCount];
        rendComponents = HidePause.GetComponentsInChildren<Renderer>();

        foreach(Renderer child in rendComponents)
        {
            Debug.Log("Found object: " + child.name);
            child.enabled = false;
        }
        */
        Debug.Log("Waiting");
        while (AO.isDone) { Debug.Log("Progress: AO.progress"); }
        SceneManager.SetActiveScene(options);

        while (!Leave) {; }
        /*
        foreach (Renderer child in rendComponents)
        {
            child.enabled = true;
        }
        */
        SceneManager.SetActiveScene(current);
        SceneManager.UnloadSceneAsync("OptionsInGame");
    }
}
