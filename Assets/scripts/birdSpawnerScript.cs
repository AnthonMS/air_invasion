using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSpawnerScript : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    private Vector2 playerPos;
    private float playerLastXPos;

    //private int randomNum;
    public int spawnEveryX = 5;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (playerLastXPos + spawnEveryX < player.transform.position.x)
        {
            int randomNum = Random.Range(1, 101); // Random number between 1 and 100. 101 is exclusive
            if (randomNum < 50) // If less than 50
            {
                SpawnBird();
            } else
            {
                SpawnRangedBird();
            }
            
        }
    }

    private void SpawnBird()
    {
        playerLastXPos = player.transform.position.x;

        float randomX = Random.Range(playerPos.x, playerPos.x + 50);
        Vector3 camPos = cam.transform.position;
        float y = camPos.y + 7; // This y value will make it spawn right above the camera view.

        GameObject birdInstance = Instantiate(Resources.Load("yellowBird", typeof(GameObject))) as GameObject;
        birdInstance.transform.Translate(new Vector3(randomX, y, 0));
        birdInstance.transform.parent = transform;

    }

    private void SpawnRangedBird()
    {
        playerLastXPos = player.transform.position.x;
        Vector3 camPos = cam.transform.position;
        float randomFloat = Random.Range(2.5f, 6f); // 1.5 to 5.99
        float y = camPos.y - randomFloat;
        float x;
        int randomInt = Random.Range(1, 3); // 1 or 2
        if (randomInt == 1)
        {
            x = camPos.x + -28; // subtract -18 for the left of camera
        } else
        {
            x = camPos.x + 10; // add +5 to make it spawn to the right of camera
        }

        GameObject birdInstance = Instantiate(Resources.Load("rangedStork_new", typeof(GameObject))) as GameObject;
        birdInstance.transform.Translate(new Vector3(x, y, 0));
        birdInstance.transform.parent = transform;
    }
}
