using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchInput : MonoBehaviour
{
    private playerStats playerStats;
    private bool isJumping = false;
    private GameObject player;
    private Rigidbody2D playerRB;
    private bool buttonClick = false;

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
                    CheckBtnClick(touchPos2, true);
                    if (CheckHitCollider(touchPos2) == false)
                    {
                        
                        if (!playerStats.isProtecting && !buttonClick)
                        {
                            // Player not protecting, he can use weapon
                            Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                            ThrowStone(screenRay);
                        }
                        //buttonClick = false;
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
        if (player.GetComponent<playerStats>().ammo != 0)
        {
            Vector3 touchPos = ray.origin;
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject weaponBullet = Instantiate(playerStats.currentWeapon, spawnPos, Quaternion.identity);
            Vector3 direction = touchPos - weaponBullet.transform.position;
            direction.z = 0;
            direction = direction.normalized;
            weaponBullet.GetComponent<Rigidbody2D>().velocity = direction * weaponBullet.GetComponent<weaponScript>().speed;
            // Flip the weapon sprite, if mousePos is behind player
            if (touchPos.x < player.transform.position.x)
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
        }
        else {
            Debug.Log("Out OF AMMO");
        }
    }

    // This checks if there are a hit collider, if so, it checks if it is a button, if so, do not throw stone
    private bool CheckHitCollider(Vector3 touchPosWorld)
    {
        Debug.Log("hits collider");
        Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
        if (hitInformation.collider != null) 
        {
            //We should have hit something with a 2D Physics collider!
            GameObject touchedObject = hitInformation.transform.gameObject;
            if (touchedObject.tag == "Button")
            {
                Debug.Log("Touched " + touchedObject.name);
                ClickButton(touchedObject, false);
                return true;
            }
            else
            {
                Debug.Log("Touched " + touchedObject.tag);
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

    public void ClickButtonNew(bool jump)
    {
        if (jump)
        {
            if (isJumping == false)
            {
                playerRB.AddForce(Vector2.up * playerStats.jumpSpeed);
                isJumping = true;
                buttonClick = false;
            }
        }
        else
        {
            Debug.Log("Protect");
        }
    }

    public void ClickProtectButton(bool protect)
    {
            player.SendMessage("ChangeProtection", protect);
    }

    public void setButtonClick(bool value){
        this.buttonClick = value;
    }
}
