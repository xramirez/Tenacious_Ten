using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCheckpoint : MonoBehaviour {
    public GameObject Checkpoint;
    public float[] checkPointPos;

    // Use this for initialization
    void Start () {

        checkPointPos[0] = Checkpoint.transform.position.x;
        checkPointPos[1] = Checkpoint.transform.position.y;
        checkPointPos[2] = Checkpoint.transform.position.z;
        checkPointPos[3] = Checkpoint.transform.position.z;
        checkPointPos[4] = Checkpoint.transform.position.z;
    }
}
