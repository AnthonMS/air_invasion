using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoSpawner : MonoBehaviour
{

    private GameObject player;
    private Vector2 playerPos;
    private float playerLastXPos;
    public int spawnEveryX;

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
        if (playerLastXPos + spawnEveryX < player.transform.position.x)
        {
            playerLastXPos = player.transform.position.x;

            GameObject ammoInstance = Instantiate(Resources.Load("ammoCrate", typeof(GameObject))) as GameObject;
            ammoInstance.transform.Translate(new Vector3(playerLastXPos + 20, playerPos.y, 0));
        }
    }
}
