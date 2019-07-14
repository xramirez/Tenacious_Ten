using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameTime : MonoBehaviour {

    private GameObject UIText;
    private GameObject camera;
    private Text text;
    public int hours = 0;
    public int minutes = 0;
    public int seconds = 0;
    private float timer;
    public bool victory = false;

	// Use this for initialization
	void Start () {
        UIText = new GameObject("gameTimer");
        camera = GameObject.Find("CinemachineCamera(Knight)");
        UIText.transform.SetParent(camera.transform);
        RectTransform transf = UIText.AddComponent<RectTransform>();
        transf.anchoredPosition = new Vector2(camera.transform.position.x/2, camera.transform.position.y/2);

        text = UIText.AddComponent<Text>();
        text.text = hours.ToString() + ":" + minutes.ToString() + "0:0" + seconds.ToString();
        text.fontSize = 10;
        text.color = new Color(1,1,1,1);
        DontDestroyOnLoad(this);
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        float t = Mathf.Abs(timer);
        seconds = (int)t % 60;
        minutes = (int)t / 60;
        text.text = String.Format("{0:00}:{1:00}",minutes, seconds);
    }
}
