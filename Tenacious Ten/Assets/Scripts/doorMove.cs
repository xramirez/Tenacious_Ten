using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorMove : MonoBehaviour
{
    public Level6Manager L6Manager;
    public goToLevelSelect goHome;
    public bool moveToStart;



    [SerializeField]
    float moveSpeedLeft;
    // Start is called before the first frame update
    void Start()
    {
        L6Manager = FindObjectOfType<Level6Manager>();
        goHome = FindObjectOfType<goToLevelSelect>();
        moveToStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveToStart == false && L6Manager.HelmetDead)
        {
            transform.position = new Vector3(transform.position.x - moveSpeedLeft, transform.position.y, transform.position.z);
            if (transform.position.x <= -530)
            {
                moveToStart = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            goHome.loadLevelSelectScene();
        }
    }
}

