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
    private GameObject currentKey;
    
    private bool listener;
    public static int activeButtons;
    // Start is called before the first frame update
    void Start()
    {
        listener = false;
        currentKey = null;
        activeButtons = 0;
    }
    private void FixedUpdate()
    {
        if (listener && activeButtons > 0)
        {
            currentKey = CurrentButton;
        }
        else
        {
            currentKey = null;
        }
    }

    private void OnGUI()
    {
        if(currentKey != null)
        {
            Event cur_event = Event.current;
            if (cur_event.isKey && cur_event.keyCode.ToString() != "None")
            {
                Debug.Log("Text input-" + cur_event.keyCode.ToString());
                CurrentButtonText.text = cur_event.keyCode.ToString();
                HideInputField();
            }
        }
    }

    public void ChangeKeybinding(GameObject cur_clicked)
    {
        currentKey = cur_clicked;
    }

    public void HideInputField()
    {
        if (activeButtons > 0)
        {
            listener = false;
            //Debug.Log("Changing text");
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
