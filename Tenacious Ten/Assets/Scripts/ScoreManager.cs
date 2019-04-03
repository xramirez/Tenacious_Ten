using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public int ShotsFired = 0;
    static public int ShotsLanded = 0;
    static public float Accuracy = 100;
    static public int TimesDied = 0;
    public Text DieCounter;
    public Text FireCounter;
    public bool ShowDieCounter = true;
    public bool ShowFireCounter = false;


    public UnityEvent Respawned;

    private void FixedUpdate()
    {
        DieCounter.text = TimesDied.ToString();
        FireCounter.text = ShotsFired.ToString();
    }

    public void Start()
    {
        if (ShowDieCounter){DieCounter.enabled = ShowDieCounter;}
        if (ShowFireCounter){FireCounter.enabled = FireCounter;}
    }

    public void TimeDied()
    {
        TimesDied = TimesDied + 1;
        Debug.Log("TimesDied = " + TimesDied);
    }
    void Accurate()
    {
        Accuracy = ShotsFired / ShotsLanded; Debug.Log("Accuracy = " + Accuracy);
    }
}

