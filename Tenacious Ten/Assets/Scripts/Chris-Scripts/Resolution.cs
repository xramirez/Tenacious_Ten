using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour {
    private Rect SafeScreen;

    // Use this for initialization
    void Start () {
        Debug.Log(Screen.width + "x" + Screen.height);
        Debug.Log(Screen.fullScreen);

        float scalar = 2.1f;
        int Width = (int)(Screen.height * scalar);
        if (Width > Screen.width)
        {
            Debug.Log("Exception: Lowest possible width updated Screen)");
            float smallestHeight = Screen.currentResolution.width / scalar;
            float smallestWidth = smallestHeight * scalar;

            Debug.Log("Exception: Lowest possible width updated Screen = (" + Width + ", " + SafeScreen.height + ")");
            Screen.SetResolution((int)smallestWidth, (int)smallestHeight, Screen.fullScreen);
        }
        else
        {
            Debug.Log("Updated Screen = (" + Width + ", " + Screen.height + ")");
            Screen.SetResolution(Width, (int)Screen.height, Screen.fullScreen);
        }
    }
}
