using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class world3SceneSwitch : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Go to name of scene. (You need to change the quotation parameter.)

        string SceneName = "Level_3.0";
        GameObject Loading = GameObject.Find("LoadingPanel");

        try
        {
            SceneManager.LoadScene(SceneName);
            Loading.SetActive(true);
        }
        catch
        {
            Debug.Log("world3SceneSwitch.cs cannot find the SceneName  \"" + SceneName + "\".");
        }
    }
}
