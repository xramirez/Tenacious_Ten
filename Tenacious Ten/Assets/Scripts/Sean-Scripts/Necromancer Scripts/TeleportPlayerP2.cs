using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerP2 : MonoBehaviour {

    GameObject Player;
    public bool startPhase2;
    NecroBossManager NBM;
    Animator anim;
    BoxCollider2D BC;
    EnemyHealthManager EHM;
    NecromancerPhase1 P1;

    StartOrResetLevel SORL;

    bool doorUnlocked;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        NBM = FindObjectOfType<NecroBossManager>();

        P1 = GameObject.FindObjectOfType<NecromancerPhase1>();

        SORL = GameObject.FindObjectOfType<StartOrResetLevel>();

        anim = GetComponent<Animator>();

        BC = GetComponent<BoxCollider2D>();
        BC.enabled = false;

        EHM = P1.GetComponent<EnemyHealthManager>();

        doorUnlocked = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    void Update()
    {
        if (EHM.enemyHealth <= P1.Phase1HP)
        {
            if (!doorUnlocked)
            {
                doorUnlocked = true;
                StartCoroutine(waitForGate(2f));
            }
        }

        if (SORL.ResetFight)
        {
            anim.SetInteger("State", 0);
            doorUnlocked = false;
            BC.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            NBM.Phase2Activated = true;
            Player.transform.position = new Vector3(Player.transform.position.x + 5f, Player.transform.position.y - 5f, 0f); 
        }
    }

    IEnumerator waitForGate(float time)
    {
        yield return new WaitForSeconds(time);
        BC.enabled = true;
        anim.SetInteger("State", 1);
    }
}
