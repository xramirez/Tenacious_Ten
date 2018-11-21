using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class world1SceneSwitch : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Go to name of scene. (You need to change the quotation parameter.)

        string SceneName = "Level_1.0";

        try
        {
            SceneManager.LoadScene(SceneName);
        }
        catch
        {
            Debug.Log("world2SceneSwitch.cs cannot find the SceneName  \"" + SceneName + "\".");
        }
    }
}
