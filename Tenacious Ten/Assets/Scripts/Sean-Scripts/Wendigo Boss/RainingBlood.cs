using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainingBlood : MonoBehaviour {

    public GameObject bloodlet;
    public Transform SpawnOne;
    public Transform SpawnTwo;
    public Transform SpawnThree;
    public Transform SpawnFour;
    public Transform SpawnFive;
    int rainValue;
    float rainOffset;
    public bool once;

    [SerializeField]
    AudioSource drip;

    [SerializeField]
    AudioSource shortRoar;


    // Use this for initialization
    void Start () {
        //SpawnOne = transform.Find("Spawn One");
        //SpawnTwo = transform.Find("Spawn Two");
        //SpawnThree = transform.Find("Spawn Three");
        //SpawnFour = transform.Find("Spawn Four");
        //SpawnFive = transform.Find("Spawn Five");
        //SpawnSix = transform.Find("Spawn Six");
        rainValue = Random.Range(0, 2);
        rainOffset = 1.5f;
    }

    // Update is called once per frame
    void Update () {
        rainOffset -= Time.deltaTime;

    }

    public void RainOneLayerOfBlood()
    {
        if (once)
        {
            shortRoar.Play();
            once = false;
        }
        if (rainValue == 0)
        {
            drip.Play();
            Instantiate(bloodlet, SpawnOne.position, Quaternion.identity);
            Instantiate(bloodlet, SpawnThree.position, Quaternion.identity);
            Instantiate(bloodlet, SpawnFive.position, Quaternion.identity);
            rainValue = 1;
        }
    
        else if(rainValue == 1)
        {
            drip.Play();
            Instantiate(bloodlet, SpawnTwo.position, Quaternion.identity);
            Instantiate(bloodlet, SpawnFour.position, Quaternion.identity);
            rainValue = 0;
        }
    }
}
