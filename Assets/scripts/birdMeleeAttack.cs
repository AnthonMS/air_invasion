using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdMeleeAttack : MonoBehaviour
{
    //private float orgSpeed;
    public float speed;
    public float damage;
    public float xOffset;
    public float health;

    // Instantiate private variables
    private GameObject player;
    private Vector3 playerPos;
    private Vector3 attackPos;
    //private Rigidbody2D rigidBody;
    //private Collider2D boxCollider;
    private GameObject whiteFeathers;

    public bool isDead = false;
    private float startXPos;



    // Use this for initialization
    void Start ()
    {
        //Score scoreobj = new Score();

        //rigidBody = GetComponent<Rigidbody2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
        whiteFeathers = Resources.Load("yellowFeathers", typeof(GameObject)) as GameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // Calculate the position the bird is going to attack. The player is moving, so make sure it attacks in front of him.
        // This looks better then if the bird keeps updating the playerPos, as it will almost always get behind him and needs to catch up again.
        attackPos = new Vector3(playerPos.x + xOffset, -5, playerPos.z);

        startXPos = transform.position.x;
        //Debug.Log("Start X: "+startXPos + "Attack X: " + attackPos.x);
        if (attackPos.x > startXPos)
        {
            //Debug.Log("START: FLIP THE BIRD!");
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isDead) // if not dead
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, attackPos, speed * Time.deltaTime);

            Vector3 direction = (transform.position - attackPos);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
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
        else if (collision.tag == "Weapon")
        {
            //Debug.Log("Bird took " + collision.gameObject.GetComponent<weaponScript>().damage + " Damage!");
            health -= collision.gameObject.GetComponent<weaponScript>().damage;
            if (health <= 0)
            {
                //Debug.Log("Bird's health is below 0");
                killBird(true);
                player.SendMessage("updateScore");
            }
        }
    }

    private void killBird(bool destroy)
    {
        if (destroy)
        {
            Destroy(transform.gameObject);
            GameObject feathers = (GameObject)Instantiate(whiteFeathers, transform.position, transform.rotation);
            Destroy(feathers, 3f);
        }
        else
        {
            isDead = true;
            //rigidBody.isKinematic = false;
            //boxCollider.isTrigger = false;
        }
    }
}
