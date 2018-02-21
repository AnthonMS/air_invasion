using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    public float damage = 5;

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
        //Debug.Log(collision.tag);
        if (collision.tag == "Ground")
        {
            Destroy(transform.gameObject);
        }
        else if (collision.tag == "Player")
        {
            Destroy(transform.gameObject);
            GameObject.FindGameObjectWithTag("Player").SendMessage("TakeMeleeDamage", damage);
        }
    }
}
