using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seagulMeleeAttack : MonoBehaviour
{
    //private float orgSpeed;
    public float speed = 10;
    public float damage = 10;
    public float xOffset = 5;

    // Instantiate private variables
    private GameObject player;
    private Vector3 playerPos;
    private Vector3 attackPos;
    private Rigidbody2D rigidBody;
    private Collider2D boxCollider;

    public bool isDead = false;

    // Use this for initialization
    void Start ()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // Calculate the position the bird is going to attack. The player is moving, so make sure it attacks in front of him.
        // This looks better then if the bird keeps updating the playerPos, as it will almost always get behind him and needs to catch up again.
        attackPos = new Vector3(playerPos.x + xOffset, playerPos.y - 5, playerPos.z);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDead) // if not dead
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, attackPos, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // This destroys the one holding this script, in this case the Bird/Seagul
            if (!isDead) // If not dead
            {
                killBird(true);
                player.SendMessage("TakeMeleeDamage", damage);
            }
        }
        else if (collision.tag == "Ground")
        {
            if (!isDead) // If not dead
            {
                killBird(true);
            }
        }
        else if (collision.tag == "Stone")
        {
            //Debug.Log("Bird got hit by stone");
            killBird(true);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    private void killBird(bool destroy)
    {
        if (destroy)
        {
            Destroy(transform.gameObject);
        }
        else
        {
            isDead = true;
            rigidBody.isKinematic = false;
            boxCollider.isTrigger = false;
        }
    }
}
