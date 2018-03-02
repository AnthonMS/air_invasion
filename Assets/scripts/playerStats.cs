﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour {

    public float runningSpeed = 3f;
    public float jumpSpeed = 500.0f;
    public float health;
    public bool isProtecting = false;
    public GameObject currentWeapon;
    public int ammo;
    public int tier;
    public Sprite protectSprite;
    public Sprite normalSprite;

    private SpriteRenderer spriteRender;
    private int lastTierIncrease;
    private int increaseTier;

	// Use this for initialization
	void Start ()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        currentWeapon = Resources.Load("stone", typeof(GameObject)) as GameObject;
        tier = 1;
        increaseTier = 500;
        ammo += 10;
    }

    // Update is called once per frame
    void Update ()
    {
        checkTierIncrease();
	}

    private void checkTierIncrease()
    {
        if (transform.position.x > lastTierIncrease + increaseTier)
        {
            // The player has moved enough on the X-Axis to increase tier
            tier += 1;
            lastTierIncrease = (int)transform.position.x;
            Debug.Log("Tier Increased to: " + tier);
        }
    }

    public void TakeMeleeDamage(float damage)
    {
        //Debug.Log("Player takes " + damage + " damage");
        if (!isProtecting)
        {
            //Debug.Log(damage + " damage taken");
        }
        else
        {
            //Debug.Log("Player is protecting");
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
        //Debug.Log("COLLISION!");
        if (collision.gameObject.tag == "Ammo")
        {
            Destroy(collision.gameObject);
            ammo += 10;
        }
    }
}
