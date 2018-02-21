using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSpawnerScript : MonoBehaviour
{

    private GameObject player;
    private Vector3 playerPos;
    private float playerLastXPos;
    private int bird = 1;
    private int lastSpawnedBird = 1;

    //private int randomNum;
    public int spawnEveryX = 5;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //randomNum = Random.Range(1, 21); // Random number between 1 and 10. 11 is exclusive
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        //if (randomNum == 20)
        //{
        if (playerLastXPos + spawnEveryX < player.transform.position.x)
        {
            SpawnBird();
            //Debug.Log(playerLastXPos + 50);
            SpawnEagle();

        }
        //}
    }

    private void SpawnBird()
    {
        playerLastXPos = player.transform.position.x;

        float randomX = Random.Range(playerPos.x, playerPos.x + 50);
        //float randomY = Random.Range(playerPos.y + 10, playerPos.x + 15);
        float y = playerPos.y + 10;

        GameObject birdInstance = Instantiate(Resources.Load("meleeBird", typeof(GameObject))) as GameObject;
        birdInstance.transform.Translate(new Vector3(randomX, y, 0));
        birdInstance.transform.parent = transform;

    }

    private void SpawnEagle()
    {
        playerLastXPos = player.transform.position.x;
        float randomX1 = Random.Range(playerPos.x, playerPos.x + 60);
        float y1 = playerPos.y + 20;
        GameObject eagleInstance = Instantiate(Resources.Load("eagle", typeof(GameObject))) as GameObject;
        eagleInstance.transform.Translate(new Vector3(randomX1, y1, 0));
        eagleInstance.transform.parent = transform;

    }


}
