using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLaser : MonoBehaviour
{
    [SerializeField] float moveDownIncrement;
    bool hasReachedGround;
    [SerializeField] float waitTimerUntilLaser;
    [SerializeField] GameObject movingLaser;
    // Start is called before the first frame update
    void Start()
    {
        hasReachedGround = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!hasReachedGround)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveDownIncrement, transform.position.z);
            if(transform.position.y <= 1.2)
            {
                hasReachedGround = true;
            }
        }

        if(hasReachedGround)
        {
            waitTimerUntilLaser -= Time.deltaTime;
            if(waitTimerUntilLaser <= 0)
            {
                Instantiate(movingLaser, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }


    }
}
