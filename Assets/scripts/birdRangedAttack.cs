using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdRangedAttack : MonoBehaviour {

    public float frontSpeed = 10;
    public float backSpeed = 5;
    public float damage = 10;
    public bool isDead = false;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private GameObject player;
    private Vector3 playerPos;
    private Vector3 myPos;
    private Camera cam;
    private Vector3 camPos;
    private bool flyRight;
    private bool droppedPoop = false;
    private SpriteRenderer spriteRender;
    private GameObject birdPoop;

    // Use this for initialization
    void Start ()
    {
        birdPoop = Resources.Load("birdPoop", typeof(GameObject)) as GameObject;
        spriteRender = this.GetComponent<SpriteRenderer>();
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        camPos = cam.transform.position; // x is bigger on cam if bird spawn on the left
        myPos = transform.position; // x is bigger on bird if it spawn on the right
        // If x is larger on cam, make bird start flying to the right
        // If x is larger on bird, make it fly to the left
        if (camPos.x > myPos.x)
        {
            spriteRender.sprite = rightSprite;
            flyRight = true;
        } else
        {
            spriteRender.sprite = leftSprite;
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
            if (!droppedPoop)
            {
                //Debug.Log("DROP BIRD POOP NOW!");
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                // Create the poop
                // As of now, the dropped poop will be destroyed when the bird is destroyed.
                // This is not supposed to happen, but I cannot get the other instantiate to spawn them at the right position
                // This is fixed by changing the parent of the poop
                GameObject poop = Instantiate(birdPoop, spawnPos, Quaternion.identity);
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
        // This will calculate if the bird will have to change direction
        camPos = cam.transform.position;
        myPos = transform.position;
        if (camPos.x - 10 > myPos.x && camPos.x > myPos.x)
        {
            // It's to far left
            flyRight = true;
            spriteRender.sprite = rightSprite;
            droppedPoop = false;
        }
        else if (myPos.x > camPos.x + 10)
        {
            // It's to far right
            flyRight = false;
            spriteRender.sprite = leftSprite;
            droppedPoop = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Stone")
        {
            //Debug.Log("Bird got hit by stone");
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
