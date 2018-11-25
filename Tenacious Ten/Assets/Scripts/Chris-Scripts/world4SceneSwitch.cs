using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class world4SceneSwitch : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Go to name of scene. (You need to change the quotation parameter.)
        Debug.Log("Ive entered world4SceneSwitch");
        string SceneName = "Lvl3 to 4 transition";

        try
        {
            SceneManager.LoadScene(SceneName);
        }
        catch
        {
            Debug.Log("world4SceneSwitch.cs cannot find the SceneName  \"" + SceneName + "\".");
        }
    }
}
