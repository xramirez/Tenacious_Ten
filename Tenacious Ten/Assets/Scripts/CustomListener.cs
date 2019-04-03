using UnityEngine;
using UnityEngine.Events;

public class CustomListener : MonoBehaviour
{
    //https://stackoverflow.com/questions/36244660/simple-event-system-in-unity
    [Header("Drag me to any function you want to listen for")]
    public UnityEvent Invoked;
    private void InvokeMe()
    {
        if (Invoked != null)
            Invoked.Invoke();
    }
}
