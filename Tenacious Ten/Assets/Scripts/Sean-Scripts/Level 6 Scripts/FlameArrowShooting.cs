using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArrowShooting : MonoBehaviour
{
    [SerializeField] float speedL;
    [SerializeField] float speedR;
    float speed;
    [SerializeField] float initialJumpForce;

    float angleZ;
    [SerializeField] float angleFactor;
    [SerializeField] float timeDestroyArrowOnImpact;
    [SerializeField] GameObject groundedArrow;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedL, speedR);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(rb.velocity.x, initialJumpForce));
        angleZ = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        //if (rb.velocity.y >= 0)
        //{
        //angleZ = angleZ + angleFactor;
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + angleFactor));
        //}
        //transform.right = testChaseObject.transform.position - transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            StartCoroutine(waitToDestroyArrow(timeDestroyArrowOnImpact));
        }
    }

    IEnumerator waitToDestroyArrow(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(groundedArrow, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}


