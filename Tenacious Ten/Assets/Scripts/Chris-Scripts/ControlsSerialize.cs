using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlsSerialize : MonoBehaviour
{
    public KeyCode _moveLeft;
    public KeyCode _moveRight;
    public KeyCode _jump;
    public KeyCode _shootLemon;
    public KeyCode _pause;
    public Text Pause;
    public Text Left;
    public Text Jump;
    public Text Right;
    public Text ShootLemon;
    public string LoadIn_Left;
    public string LoadIn_Right;
    public string LoadIn_Jump;
    public string LoadIn_ShootLemon;
    public string LoadIn_Pause;
    public Button SaveButton;

    void Awake()
    {
        LoadAll();
        SaveButton.interactable = false;
    }

    void Update()
    {
        if (Pause.text != LoadIn_Pause || Left.text != LoadIn_Left || Jump.text != LoadIn_Jump || Right.text != LoadIn_Right || ShootLemon.text != LoadIn_ShootLemon)
        {
            SaveButton.interactable = true;
        }
        else
        {
            SaveButton.interactable = false;
        }
    }

    public void Save(){
        _moveRight = (KeyCode)Enum.Parse(typeof(KeyCode), Right.text);
        _moveLeft = (KeyCode)Enum.Parse(typeof(KeyCode), Left.text);
        _jump = (KeyCode)Enum.Parse(typeof(KeyCode), Jump.text);
        _pause = (KeyCode)Enum.Parse(typeof(KeyCode), Pause.text);
        _shootLemon = (KeyCode)Enum.Parse(typeof(KeyCode), ShootLemon.text);

        Debug.Log("Move Right1: " + _moveRight);
        Debug.Log("Move Left1: " + _moveLeft);
        Debug.Log("Jump1: " + _jump);
        Debug.Log("Pause1: " + _pause);
        Debug.Log("Shoot Lemon1: " + _shootLemon);

        ControlsSerializeManager.SaveControlsData(this);

        Debug.Log("Move Right2: " + ControlsSerializeManager.Load_MoveRight_Data());
        Debug.Log("Move Left2: " + ControlsSerializeManager.Load_MoveLeft_Data());
        Debug.Log("Jump2: " + ControlsSerializeManager.Load_Jump_Data());
        Debug.Log("Pause2: " + ControlsSerializeManager.Load_Pause_Data());
        Debug.Log("Shoot Lemon2: " + ControlsSerializeManager.Load_ShootLemon_Data());
        Debug.Log("Controls data saved!");
    }
    public void LoadAll()
    {
        LoadIn_Left = ControlsSerializeManager.Load_MoveLeft_Data().ToString();
        LoadIn_Right = ControlsSerializeManager.Load_MoveRight_Data().ToString();
        LoadIn_Jump = ControlsSerializeManager.Load_Jump_Data().ToString();
        LoadIn_ShootLemon = ControlsSerializeManager.Load_ShootLemon_Data().ToString();
        LoadIn_Pause = ControlsSerializeManager.Load_Pause_Data().ToString();
        Left.text = LoadIn_Left;
        Right.text = LoadIn_Right;
        Jump.text = LoadIn_Jump;
        ShootLemon.text = LoadIn_ShootLemon;
        Pause.text = LoadIn_Pause;

        Debug.Log("Controls data loaded!");
        Debug.Log("Move Right: " + ControlsSerializeManager.Load_MoveRight_Data());
        Debug.Log("Move Left: " + ControlsSerializeManager.Load_MoveLeft_Data());
        Debug.Log("Jump: " + ControlsSerializeManager.Load_Jump_Data());
        Debug.Log("Pause: " + ControlsSerializeManager.Load_Pause_Data());
        Debug.Log("Shoot Lemon: " + ControlsSerializeManager.Load_ShootLemon_Data());
    }
    public void DeleteSave()
    {
        ControlsSerializeManager.DeleteControlsData();
    }
    
}

