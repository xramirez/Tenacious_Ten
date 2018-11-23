using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class world3SceneSwitch : StateMachineBehaviour {

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Go to name of scene. (You need to change the quotation parameter.)
        Debug.Log("Ive entered world3SceneSwitch");
        string SceneName = "Level_3.0";
        SceneManager.LoadScene(SceneName);
    }
}
