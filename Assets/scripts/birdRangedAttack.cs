using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdRangedAttack : MonoBehaviour {

    public float frontSpeed = 10;
    public float backSpeed = 5;
    public float damage = 10;
    public bool isDead = false;

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 myPos;
    private Camera cam;
    private Vector3 camPos;
    private bool flyRight;
    private bool droppedPoop = false;
    private SpriteRenderer spriteRender;
    private GameObject birdPoop;
    private GameObject whiteFeathers;

    // Use this for initialization
    void Start ()
    {
        birdPoop = Resources.Load("birdPoop", typeof(GameObject)) as GameObject;
        whiteFeathers = Resources.Load("whiteFeathers", typeof(GameObject)) as GameObject;
        spriteRender = this.GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        camPos = cam.transform.position; // x is bigger on cam if bird spawn on the left
        myPos = transform.position; // x is bigger on bird if it spawn on the right
        // If x is larger on cam, make bird start flying to the right
        // If x is larger on bird, make it fly to the left
        if (camPos.x > myPos.x)
        {
            //spriteRender.sprite = rightSprite;
            spriteRender.flipX = true;
            flyRight = true;
        } else
        {
            //spriteRender.sprite = leftSprite;
            spriteRender.flipX = false;
            flyRight = false;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        Fly();

        DropBirdPoop();
    }

    private void DropBirdPoop()
    {
        playerPos = player.transform.position;
        float randomFloat = Random.Range(2.5f, 7.5f);

        // Check if the bird is around above the player
        if (transform.position.x + randomFloat > playerPos.x &&
            transform.position.x - randomFloat < playerPos.x)
        {
            // Checks that it has only dropped 1 poop on that flyby. This is set to false as soon as the bird changes direction
            // Also checks if the bird is dead, this is because for 3 seconds after collision with stone,
            // It is dead, but it is only invisble to make the poop dropped right before death stay in the game until it collides.
            if (!droppedPoop && !isDead)
            {
                //Debug.Log("DROP POOP NOW!");
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                // Create the poop
                GameObject poop = (GameObject)Instantiate(birdPoop, spawnPos, Quaternion.identity);
                poop.transform.parent = transform;
                droppedPoop = true;
            }

        }
    }

    private void Fly()
    {
        if (flyRight)
        {
            // The bird spawned to the left
            transform.Translate(Vector3.right * frontSpeed * Time.deltaTime);
        }
        else
        {
            // The bird spawned to the right
            transform.Translate(Vector3.left * backSpeed * Time.deltaTime);
        }
        // This will calculate if the bird hits the border of screen and will have to change direction
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        myPos = transform.position;
        if (leftBorder > myPos.x)
        {
            // It's to far left
            flyRight = true;
            spriteRender.flipX = true;
            //spriteRender.sprite = rightSprite;
            droppedPoop = false;
        }
        else if (rightBorder < myPos.x)
        {
            // It's to far right
            flyRight = false;
            spriteRender.flipX = false;
            //spriteRender.sprite = leftSprite;
            droppedPoop = false;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Weapon")
        {
            // The bird is dead, but it might have just dropped a poop
            // Because of this, we will just make it invisible and not collidable
            // And after 3 seconds, destroy it, this way, the poop will have fallen to the ground.
            isDead = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 3f);
            Destroy(collision.gameObject); // Destroy the stone
            GameObject feathers = (GameObject)Instantiate(whiteFeathers, transform.position, transform.rotation); // Create feathers
            Destroy(feathers, 2f); // Destory feathers after 2 seconds when they are gone anyway.
            //player.SendMessage("updateScore");
        }
    }
}
