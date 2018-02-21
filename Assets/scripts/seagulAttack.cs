using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seagulAttack : MonoBehaviour {

    private GameObject player;
    private Vector3 playerPos;
    public float speed = 5;
    public float damage = 10;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Debug.Log("Seagul flies into Player");
            player.SendMessage("TakeDamage", damage);
        }
    }
}
