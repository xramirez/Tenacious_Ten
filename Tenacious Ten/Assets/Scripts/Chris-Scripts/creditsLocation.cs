using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditsLocation : MonoBehaviour {

    public int Order;
    private GameObject sprite;

	// Use this for initialization
	void Start () {
        //int x = Camera.main.WorldToScreenPoint(new Vector3())
        int y = Screen.height;
        sprite = this.gameObject;
        Debug.Log("Current GameObject is: " + sprite);

        //Debug.Log("Old x" + x);
        //x = x / 2;
        //Debug.Log("New x" + x);
        //First Half of screen(x)
        if(Order > 1 && Order < 5)
        {
            //Vector3 newPos = new Vector3(x, sprite.transform.position.y, sprite.transform.position.z);
            //sprite.transform.position = newPos;
        }
        //Second Half of screen (x)
        else if(Order >= 5 && Order <= 9)
        {

        }

	}
}
