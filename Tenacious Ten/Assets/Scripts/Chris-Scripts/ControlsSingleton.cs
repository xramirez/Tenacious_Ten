using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSingleton : MonoBehaviour
{
    #region Singleton 
    private static ControlsSingleton _instance;
    public static ControlsSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ControlsSingleton");
                go.AddComponent<ControlsSingleton>();
            }
            return _instance;
        }
    }
    #endregion

    public KeyCode CurrentMoveLeft { get; set; }
    public KeyCode CurrentMoveRight { get; set; }
    public KeyCode CurrentMoveJump { get; set; }
    public KeyCode CurrentMoveShootLemon { get; set; }
    public KeyCode CurrentPause { get; set; }

    void Awake()
    {
        _instance = this;
        CurrentMoveLeft = ControlsSerializeManager.Load_MoveLeft_Data();
        CurrentMoveRight = ControlsSerializeManager.Load_MoveRight_Data();
        CurrentMoveJump = ControlsSerializeManager.Load_Jump_Data();
        CurrentMoveShootLemon = ControlsSerializeManager.Load_ShootLemon_Data();
        CurrentPause = ControlsSerializeManager.Load_Pause_Data();
    }
}
