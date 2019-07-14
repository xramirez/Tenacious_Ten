using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ControlsManager : MonoBehaviour
{
    public Text Pause;
    public Text Left;
    public Text Jump;
    public Text Right;
    public Text ShootLemon;
    public Button SaveButton;
    private ControlsSerialize buildStruct;

    // Start is called before the first frame update
    void Start()
    {
        buildStruct = new ControlsSerialize();
        SaveButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pause.text != "Pause" && Left.text != "Left" && Jump.text != "Jump" && Right.text != "Right" && ShootLemon.text != "Shoot Lemon")
        {
            SaveButton.interactable = true;
        }
        else
        {
            SaveButton.interactable = false;
        }
    }

    public void SaveMyControls()
    {
        Debug.Log(((KeyCode)Enum.Parse(typeof(KeyCode), Left.text)).ToString());

        KeyCode KeyRight = (KeyCode)Enum.Parse(typeof(KeyCode), Right.text);
        KeyCode KeyLeft = (KeyCode)Enum.Parse(typeof(KeyCode), Left.text);
        KeyCode KeyJump = (KeyCode)Enum.Parse(typeof(KeyCode), Jump.text);
        KeyCode KeyPause = (KeyCode)Enum.Parse(typeof(KeyCode), Pause.text);
        KeyCode KeyShootLemon = (KeyCode)Enum.Parse(typeof(KeyCode), ShootLemon.text);

        Debug.Log("Move Right1: " + KeyRight);
        Debug.Log("Move Left1: " + KeyLeft);
        Debug.Log("Jump1: " + KeyJump);
        Debug.Log("Pause1: " + KeyPause);
        Debug.Log("Shoot Lemon1: " + KeyShootLemon);

        if (Left.name == "Left") { buildStruct._moveLeft = KeyLeft; }
        else if (Right.name == "Right") { buildStruct._moveRight = KeyRight;  }
        else if (Jump.name == "Jump") { buildStruct._jump = KeyJump; }
        else if (Pause.name == "Pause") { buildStruct._pause = KeyPause; }
        else if (ShootLemon.name == "Shoot Lemon") { buildStruct._shootLemon = KeyShootLemon; }

        ControlsSerializeManager.SaveControlsData(buildStruct);

        Debug.Log("Move Right2: " + ControlsSerializeManager.Load_MoveRight_Data());
        Debug.Log("Move Left2: " + ControlsSerializeManager.Load_MoveLeft_Data());
        Debug.Log("Jump2: " + ControlsSerializeManager.Load_Jump_Data());
        Debug.Log("Pause2: " + ControlsSerializeManager.Load_Pause_Data());
        Debug.Log("Shoot Lemon2: " + ControlsSerializeManager.Load_ShootLemon_Data());
        Debug.Log("Controls data saved!");
    }
}
