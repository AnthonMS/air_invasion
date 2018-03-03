using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponScript : MonoBehaviour {

    public string weaponType;
    public float damage;
    public float speed;
    public float rotationSpeed;
    //private bool clockRotation;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MoveRotation(rb.rotation + rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Bird")
        {
            //Debug.Log("Stone hit bird!");
            Destroy(transform.gameObject);
        }
        else if (collision.tag == "Ground")
        {
            Destroy(transform.gameObject);
        }
        else if (collision.tag == "Boss")
        {
            Destroy(transform.gameObject);
        }
    }

    public void RotationDirection(bool clockwise)
    {
        //clockRotation = clockwise;
        if (clockwise)
        {
            rotationSpeed = -rotationSpeed;
        }
        else
        {
            rotationSpeed = +rotationSpeed;
        }
    }
}
