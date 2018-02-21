using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stone : MonoBehaviour
{
    public float damage = 10;
    public float speed = 20;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bird")
        {
            //Debug.Log("Stone hit bird!");
            Destroy(transform.gameObject);
        }
        else if (collision.tag == "Ground")
        {
            Destroy(transform.gameObject);
        }
    }
}
