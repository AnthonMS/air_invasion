using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchInput : MonoBehaviour
{
    private playerStats playerStats;
    private bool isJumping = false;
    private GameObject player;
    private Rigidbody2D playerRB;

    // Use this for initialization
    void Start ()
    {
        playerStats = gameObject.GetComponent<playerStats>();
        playerRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Makes the player run
        transform.Translate(Vector3.right * playerStats.runningSpeed * Time.deltaTime);

        foreach (var touch in Input.touches)
        {
            TouchPhase phase = touch.phase;
            switch (phase)
            {
                case TouchPhase.Began:
                    // This is used to check if a button is pressed
                    Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    CheckBtnClick(touchPos, true);
                    break;
                case TouchPhase.Moved:
                    // This is not used
                    break;
                case TouchPhase.Stationary:
                    // This is not used
                    break;
                case TouchPhase.Ended:
                    // This throws a stone if touch is not on a button, and he isn't protecting
                    Vector3 touchPos2 = Camera.main.ScreenToWorldPoint(touch.position);
                    if (CheckHitCollider(touchPos2) == false)
                    {
                        if (!playerStats.isProtecting)
                        {
                            // Player not protecting, he can use weapon
                            Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                            ThrowStone(screenRay);
                        }
                        
                    }

                    break;
                case TouchPhase.Canceled:
                    // This is not used
                    break;
            }
        }
	}

    // This is just used to check if he is no longer jumping
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isJumping == true)
        {
            isJumping = false;
        }
    }

    // This just throws the stone in the direction of touch position.
    private void ThrowStone(Ray ray)
    {
        Vector3 touchPos = ray.origin;
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject weaponBullet = Instantiate(playerStats.currentWeapon, spawnPos, Quaternion.identity);
        Vector3 direction = touchPos - weaponBullet.transform.position;
        direction.z = 0;
        direction = direction.normalized;
        weaponBullet.GetComponent<Rigidbody2D>().velocity = direction * weaponBullet.GetComponent<stone>().speed;
    }

    // This checks if there are a hit collider, if so, it checks if it is a button, if so, do not throw stone
    private bool CheckHitCollider(Vector3 touchPosWorld)
    {
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            //We should have hit something with a 2D Physics collider!
            GameObject touchedObject = hitInformation.transform.gameObject;
            if (touchedObject.transform.tag == "Button")
            {
                //Debug.Log("Touched " + touchedObject.transform.name);
                ClickButton(touchedObject, false);
                return true;
            }
            else
            {
                Debug.Log("Touched " + touchedObject.transform.tag);
                return false;
            }

        } else
        {
            // We did not touch any object
            return false;
        }
    }

    // Checks if it is a button that is touched
    private void CheckBtnClick(Vector3 touchPos, bool startClick)
    {
        Vector2 touchPosWorld2D = new Vector2(touchPos.x, touchPos.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
        if (hitInformation.collider != null)
        {
            //We should have hit something with a 2D Physics collider!
            GameObject touchedObject = hitInformation.transform.gameObject;
            if (touchedObject.transform.tag == "Button")
            {
                ClickButton(touchedObject, startClick);
            }
        }
    }

    // Handles the event of a button clicked, jump or protect
    private void ClickButton(GameObject button, bool startClick)
    {
        // Logic for different buttons
        if (button.transform.name == "Jump_btn")
        {
            if (startClick)
            {
                button.SendMessage("ButtonClicked", true);
                // Make him jump
                // This checks if the player is already jumping, if not, make him jump.
                if (isJumping == false)
                {
                    playerRB.AddForce(Vector2.up * playerStats.jumpSpeed);
                    isJumping = true;
                }
            }
            else
            {
                button.SendMessage("ButtonClicked", false);
            }
        }
        else if (button.transform.name == "Protect_btn")
        {
            if (startClick)
            {
                button.SendMessage("ButtonClicked", true);
                // Make him protect
                player.SendMessage("ChangeProtection", true);
            }
            else
            {
                button.SendMessage("ButtonClicked", false);
                player.SendMessage("ChangeProtection", false);
            }

        }
    }
}
