using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {

    private int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private PlayerHealthManager PHM;

    // Use this for initialization
    void Start () {

        PHM = FindObjectOfType<PlayerHealthManager>();
        health = PlayerHealthManager.playerHealth;
    }
	
	// Update is called once per frame
	void Update () {

        health = PlayerHealthManager.playerHealth;

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
		
	}
}
