using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class playerStats : MonoBehaviour
{

    public float runningSpeed = 3f;
    public float jumpSpeed = 500.0f;
    public float health;
    public bool isProtecting = false;
    public GameObject currentWeapon;
    public int ammo;
    public int tier;
    public Sprite protectSprite;
    public Sprite normalSprite;
    public Text healthText;

    private SpriteRenderer spriteRender;
    private int lastTierIncrease;
    public int increaseTier;
    private int weaponTier;

    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        currentWeapon = Resources.Load("stone", typeof(GameObject)) as GameObject;
        //currentWeapon = Resources.Load("silverAxe", typeof(GameObject)) as GameObject;
        tier = 1;
        weaponTier = 1;
        ammo += 10;
        health = 100;
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        checkTierIncrease();
        gameObject.SendMessage("updateAmmo");

    }

    private void checkTierIncrease(){
        if (transform.position.x > lastTierIncrease + increaseTier)
        {
            // The player has moved enough on the X-Axis to increase tier
            tier += 1;
            lastTierIncrease = (int)transform.position.x;
            Debug.Log("Tier Increased to: " + tier);
            GameObject.Find("birdSpawner").SendMessage("StartStopBossFight", true);
        }
    }

    public void TakeMeleeDamage(float damage)
    {
        //Debug.Log("Player takes " + damage + " damage");
        if (!isProtecting)
        {
            health -= damage;
            updateHealth();
            //Debug.Log(damage + " melee damage taken");
        }
        else
        {
            //Debug.Log("Player is protecting");
        }
    }


    public void updateHealth()
    {
        if (health > 0)
        {
            healthText.text = "Health: " + health;
        }
        else
        {
            healthText.text = "RIP";
        }
    }

    public void ChangeProtection(bool protect)
    {
        isProtecting = protect;
        if (protect)
            spriteRender.sprite = protectSprite;

        if (!protect)
            spriteRender.sprite = normalSprite;
    }

    public void GiveAmmo(int amount)
    {
        ammo += amount;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("COLLISION! " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ammo")
        {
            Destroy(collision.gameObject);
            ammo += 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WeaponUpgrade")
        {
            weaponTier += 1;
            //Debug.Log("UPGRADE: Tier " + weaponTier);
            Destroy(collision.gameObject);
            //GameObject testWep = collision.gameObject.SendMessage("GetWeapon", 1);
            //collision.gameObject.GetComponent<weaponUpgrade>().weapon_1;
            GameObject testWep = collision.gameObject.GetComponent<weaponUpgrade>().GetWeapon(weaponTier);
            if (testWep != null)
            {
                Debug.Log(testWep.name);
                currentWeapon = testWep;
            }
            else
            {
                Debug.Log("TestWep returned as null!");
            }
        }
    }
}