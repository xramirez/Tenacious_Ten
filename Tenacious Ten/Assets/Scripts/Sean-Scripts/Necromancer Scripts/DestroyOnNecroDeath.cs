using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnNecroDeath : MonoBehaviour
{

    NecromancerPhase6 BM;
    bool fightComplete;

    // Use this for initialization
    void Start()
    {
        BM = GameObject.FindObjectOfType<NecromancerPhase6>();
        fightComplete = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindObjectOfType<NecromancerPhase6>() != null)
        {
            BM = GameObject.FindObjectOfType<NecromancerPhase6>();
        }



        if (BM.GetComponent<EnemyHealthManager>().enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
