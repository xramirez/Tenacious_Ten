using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelegateStats : MonoBehaviour
{
    public Text _TimesDied;
    public Text _ShotsFired;
    public Text _ShotsLanded;
    public Text _Accuracy;
    private string acc;
    private string acc1;
    private string acc2;

    // Update is called once per frame
    void Update()
    {
        _TimesDied.text = ScoreManager.Instance.TimesDied.ToString();
        _ShotsFired.text = ScoreManager.Instance.ShotsFired.ToString();
        _ShotsLanded.text = ScoreManager.Instance.ShotsLanded.ToString();
        /*
        if(ScoreManager.Instance.Accuracy != 0 && ScoreManager.Instance.Accuracy.ToString().Length > 3)
        {
            acc = ScoreManager.Instance.Accuracy.ToString().Substring(2, 4);
            acc1 = acc.Substring(0, 2);
            acc2 = acc.Substring(2, 2);
            _Accuracy.text = acc1 + "." + acc2 + "%";
        }*/
        _Accuracy.text = ScoreManager.Instance.Accuracy.ToString();
    }
}
