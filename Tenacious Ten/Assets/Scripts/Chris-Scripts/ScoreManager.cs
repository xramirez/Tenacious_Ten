using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton 
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ScoreManager");
                go.AddComponent<ScoreManager>();
            }
            return _instance;
        }
    }
    #endregion

    public int TimesDied { get; set; }
    public float ShotsFired { get; set; }
    public float ShotsLanded { get; set; }
    public float Accuracy { get; set; }

    void Awake()
    {
        _instance = this;
        TimesDied = 0;
        ShotsFired = 0;
        ShotsLanded = 0;
        Accuracy = 0;
    }

    void Update()
    {
        if (ShotsFired != 0 && ShotsLanded != 0)
        {
            Accuracy = ShotsLanded / ShotsFired;
        }
    }

}

