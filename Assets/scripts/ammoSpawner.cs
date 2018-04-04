using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoSpawner : MonoBehaviour
{

    private GameObject player;
    private Vector2 playerPos;
    private float playerLastXPos;
    public int spawnEveryX;
    public int weapontier;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }
	
	// Update is called once per frame
	void Update ()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        weapontier = player.GetComponent<playerStats>().weaponTier;

        if (playerLastXPos + spawnEveryX < player.transform.position.x)
        {


            playerLastXPos = player.transform.position.x;

            /*GameObject ammoInstance = Instantiate(Resources.Load("ammoCrate", typeof(GameObject))) as GameObject;
            ammoInstance.transform.Translate(new Vector3(playerLastXPos + 20, playerPos.y, 0));
            Debug.Log("spawn ammo");*/

            switch (weapontier)
            {
                case 1:
                    GameObject ammoInstance = Instantiate(Resources.Load("ammoCrate", typeof(GameObject))) as GameObject;
                    ammoInstance.transform.Translate(new Vector3(playerLastXPos + 20, playerPos.y, 0));
                    Debug.Log("spawn ammo");
                    break;
                case 2:
                    ammoInstance = Instantiate(Resources.Load("silveraxeCrate", typeof(GameObject))) as GameObject;
                    ammoInstance.transform.Translate(new Vector3(playerLastXPos + 20, playerPos.y, 0));
                    Debug.Log("spawn axeammo");
                    break;
            }




        }
        /*
        if (playerLastXPos + spawnEveryX < player.transform.position.x && weapontier==2)
        {
            
           
            playerLastXPos = player.transform.position.x;

            GameObject ammoInstance = Instantiate(Resources.Load("silveraxeCrate", typeof(GameObject))) as GameObject;
            ammoInstance.transform.Translate(new Vector3(playerLastXPos + 20, playerPos.y, 0));
            Debug.Log("spawn silveraxe");





        }*/



    }
}
