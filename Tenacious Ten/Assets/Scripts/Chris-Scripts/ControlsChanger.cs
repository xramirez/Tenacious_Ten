using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsChanger : MonoBehaviour
{
    public GameObject CurrentButton;
    public Text CurrentButtonText;
    public GameObject CurrentInputField;
    public InputField InputF;
    public GameObject SavedControls;    
    
    private bool listener;
    public static int activeButtons;
    // Start is called before the first frame update
    void Start()
    {
        listener = false;
        activeButtons = 0;
    }
    private void FixedUpdate()
    {
        if (listener && activeButtons > 0)
        {
            CurrentButtonText.text = WeirdKeys(InputF.text);
        }
    }

    public string WeirdKeys(string input)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return "Escape";    
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || input == " ")
            {
                return "Space";
            }
            return input.ToUpper();
        }
    }
    public void HideInputField()
    {
        if (activeButtons > 0)
        {
            listener = false;
            Debug.Log("Changing text");
            CurrentInputField.SetActive(false);
            activeButtons--;
        }
    }
    public void ShowInputField()
    {
        if (activeButtons < 1)
        {
            listener = true;
            CurrentInputField.SetActive(true);
            activeButtons++;
        }
    }
}
