using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour {

    public float health;
    private bool isProtecting = false;
    public GameObject currentWeapon;

	// Use this for initialization
	void Start ()
    {
        currentWeapon = Resources.Load("stone", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update () {
		
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
    }
}
