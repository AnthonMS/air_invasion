using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboardInput : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // Sprites to use
    public Sprite protectSprite;
    public Sprite normalSprite;

    // Instantiate private variables
    private bool isJumping = false;
    private GameObject player;
    private SpriteRenderer spriteRender;
    private playerStats playerStats;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRender = player.GetComponent<SpriteRenderer>();
        playerStats = player.GetComponent<playerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * playerStats.runningSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            // This checks if the player is already jumping, if not, make him jump.
            if (isJumping == false)
            {
                rigidBody.AddForce(Vector2.up * playerStats.jumpSpeed);
                isJumping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            spriteRender.sprite = protectSprite;
            //isProtecting = true;
            player.SendMessage("ChangeProtection", true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            spriteRender.sprite = normalSprite;
            //isProtecting = false;
            player.SendMessage("ChangeProtection", false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!player.GetComponent<playerStats>().isProtecting)
            {
                if (player.GetComponent<playerStats>().ammo != 0)
                {
                    // throw/shoot current weapon
                    // Get mouse position
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 mousePos = ray.origin;
                    Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    // Create the bullet
                    GameObject weaponBullet = Instantiate(playerStats.currentWeapon, spawnPos, Quaternion.identity);
                    // Get the direction the bullet needs to fly in
                    Vector3 direction = mousePos - weaponBullet.transform.position;
                    direction.z = 0;
                    direction = direction.normalized;
                    // Make the bullet fly in that direction at the speed of you weapon
                    weaponBullet.GetComponent<Rigidbody2D>().velocity = direction * weaponBullet.GetComponent<weaponScript>().speed;
                    // Flip the weapon sprite, if mousePos is behind player
                    if (mousePos.x < player.transform.position.x)
                    {
                        weaponBullet.GetComponent<SpriteRenderer>().flipX = true;
                        // rotate the bullet counter clock wise
                        weaponBullet.SendMessage("RotationDirection", false);
                    }
                    else
                    {
                        // rotate bullet clock wise
                        weaponBullet.SendMessage("RotationDirection", true);
                    }

                    player.GetComponent<playerStats>().ammo -= 1;
                } else
                {
                    Debug.Log("OUT OF AMMO!!!");
                }
                
            } else
            {
                Debug.Log("You are protecting, can't use weapon!");
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isJumping == true)
        {
            isJumping = false;
        }
    }
}
