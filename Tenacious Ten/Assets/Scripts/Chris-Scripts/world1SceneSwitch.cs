using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class world1SceneSwitch : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Go to name of scene. (You need to change the quotation parameter.)
        Debug.Log("Ive entered world1SceneSwitch");
        string SceneName = "Level_1.0";
        try
        {
            Debug.Log("Switching to \"" + SceneName + "\"...");
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        }
        catch(Exception e)
        {
            Debug.Log("world1SceneSwitch.cs cannot find the SceneName  \"" + SceneName + "\".");
        }
    }
}
