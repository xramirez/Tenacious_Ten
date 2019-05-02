using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Manager : MonoBehaviour
{
    [SerializeField] L6BootsManager Boots;
    [SerializeField] L6HandsManager Hands;
    [SerializeField] L6TorsoManager Torso;
    [SerializeField] L6HelmetManager Helmet;
    [SerializeField] public bool check;

    public bool bootsDead, handsDead, TorsoDead, HelmetDead;

    // Start is called before the first frame update
    void Start()
    {
        check = false;
        bootsDead = false;
        handsDead = false;
        TorsoDead = false;
        HelmetDead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Boots.bootsIsDead == true)
        {
            check = true;
            //Debug.Log("BOOTS DEAD, ONTO HANDS");
        }
        if (Hands.handsIsDead == true)
        {
            check = true;
            //Debug.Log("HANDS DEAD, ONTO TORSO");
        }
        if (Torso.torsoIsDead == true)
        {
            check = true;
            //Debug.Log("TORSO DEAD, ONTO HELMET");
        }
        if (Torso.torsoIsDead == true)
        {
            check = true;
            //Debug.Log("TORSO DEAD, ONTO HELMET");
        }
        if (Helmet.helmetIsDead == true)
        {
            check = true;
            //Debug.Log("HELMET DEAD, ONTO BOSS");
        }
        bootsDead = Boots.bootsIsDead;
        handsDead = Hands.handsIsDead;
        TorsoDead = Torso.torsoIsDead;
        HelmetDead = Helmet.helmetIsDead;
    }
}
