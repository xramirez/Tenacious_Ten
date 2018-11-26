using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawnOnSpace : MonoBehaviour {
    public GameObject HUD;
    public bool spacebar;
    public bool available = false;
    public bool once = false;
    
    void LateUpdate()
    {
        //if (Input.GetKeyDown("space") && available == true)
        if (Input.GetKeyDown("space"))
            {
            //spacebar = true;
            HUD.GetComponent<Level1DeathMenu>().Respawn();
            //available = false;
            //once = false;
        }
    }
    /*
    void Update()
    {
        if(once == false)
        {
            once = true;
            Debug.Log("Start OnEnable");
            Invoke("boolswitch", 2);
        }
    }

    void boolswitch()
    {
        Debug.Log("Start boolswitch");
        available = true;
    }
    */
    /*
    void Update()
    {
        if(once == false)
        {
            once = true;
            Invoke()
            available = true;
        }
    }

    void Wait()
    {
        yield WaitForSeconds();
    }*/
}
