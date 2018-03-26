using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdSpawnerScript : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    private playerStats playerStats;
    private Vector2 playerPos;
    private float playerLastXPos;

    //private int randomNum;
    private int spawnEveryX;
    private string birdToSpawn = "yellowBird";
    private int birdHealth = 10;
    private int birdSpeed = 10;
    private int birdXOffset = 10;
    public bool bossFight = false;
    private int bossTier;
    private string bossToSpawn = "greenDragon";

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<playerStats>();
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        spawnEveryX = 10;
        bossTier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossFight)
        {
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            if (playerLastXPos + spawnEveryX < player.transform.position.x)
            {
                int randomNum = Random.Range(1, 101); // Random number between 1 and 100. 101 is exclusive
                if (randomNum < 70) // If less than 50
                {
                    SpawnBirdTest();
                }
                else
                {
                    SpawnRangedBird();
                }

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
        float randomFloat = Random.Range(2.5f, 6f); // 2.5 to 5.99
        float y = camPos.y - randomFloat;
        float x;
        int randomInt = Random.Range(1, 3); // 1 or 2
        if (randomInt == 1)
        {
            x = camPos.x + -28; // subtract -18 for the left of camera
        } else
        {
            x = camPos.x + 25; // add +5 to make it spawn to the right of camera
        }

        GameObject birdInstance = Instantiate(Resources.Load("rangedStork_new", typeof(GameObject))) as GameObject;
        birdInstance.transform.Translate(new Vector3(x, y, 0));
        birdInstance.transform.parent = transform;
    }

    private void SpawnBirdTest()
    {
        playerLastXPos = player.transform.position.x;

        float randomX = Random.Range(playerPos.x, playerPos.x + 50);
        Vector3 camPos = cam.transform.position;
        float y = camPos.y + 7; // This y value will make it spawn right above the camera view.

        //SwitchTierSetting();

        GameObject birdInstance = Instantiate(Resources.Load(birdToSpawn, typeof(GameObject))) as GameObject;
        //birdInstance.GetComponent<birdMeleeAttack>().health = birdHealth;
        //birdInstance.GetComponent<birdMeleeAttack>().speed = birdSpeed;
        //birdInstance.GetComponent<birdMeleeAttack>().xOffset = birdXOffset;
        birdInstance.transform.Translate(new Vector3(randomX, y, 0));
        birdInstance.transform.parent = transform;

        //Debug.Log("Spawned a " + birdToSpawn + " With " + birdHealth + " Health, And " + birdSpeed + " Speed, And xOffset " + birdXOffset);
    }


    private void SpawnBoss()
    {
        SwitchTierSetting();

        Vector3 camPos = cam.transform.position;
        float y = 0;
        float x = camPos.x + 25;

        GameObject birdInstance = Instantiate(Resources.Load(bossToSpawn, typeof(GameObject))) as GameObject;
        birdInstance.transform.Translate(new Vector3(x, y, 0));
        GameObject.FindGameObjectWithTag("AudioManager").SendMessage("PlayBossSpawnSound");
    }


    public void StartStopBossFight(bool bossFight)
    {
        if (bossFight)
        {
            this.bossFight = true;
            Debug.Log("Starting Boss Fight!");
            SpawnBoss();
        }
        else
        {
            this.bossFight = false;
            Debug.Log("Ending Boss Fight!");
        }
    }

    private void SwitchTierSetting()
    {
        switch (playerStats.tier)
        {
            case 1:
                birdToSpawn = "yellowBird";
                bossToSpawn = "greenDragon";
                //birdHealth = 10;
                //birdSpeed = 5;
                //birdXOffset = 10;
                break;
            case 2:
                birdToSpawn = "brownBirdPot";
                //birdHealth = 15;
                //birdSpeed = 6;
                //birdXOffset = 10;
                break;
            case 3:
                birdToSpawn = "blueBirdHat";
                //birdHealth = 20;
                //birdSpeed = 7;
                //birdXOffset = 8;
                break;
            case 4:
                birdHealth = 25;
                birdSpeed = 8;
                //playerStats.currentWeapon.GetComponent<weaponScript>().damage = 20;
                break;
            case 5:
                birdHealth = 30;
                birdSpeed = 9;
                birdXOffset = 6;
                break;
            case 6:
                birdHealth = 35;
                birdSpeed = 10;
                birdXOffset = 5;
                break;
            case 7:
                birdHealth = 40;
                break;
            case 8:
                birdHealth = 45;
                //playerStats.currentWeapon.GetComponent<weaponScript>().damage = 40;
                break;
            case 9:
                birdHealth = 50;
                break;
            case 10:
                birdHealth = 55;
                break;
            case 11:
                birdHealth = 60;
                break;
            case 12:
                birdHealth = 65;
                //playerStats.currentWeapon.GetComponent<weaponScript>().damage = 60;
                break;
            case 13:
                birdHealth = 70;
                break;
            case 14:
                birdHealth = 75;
                break;
            case 15:
                birdHealth = 80;
                break;
            case 16:
                birdHealth = 85;
                break;
            case 17:
                birdHealth = 90;
                break;
            case 18:
                birdHealth = 95;
                break;
            case 19:
                birdHealth = 100;
                break;
            case 20:
                birdHealth = 105;
                break;
            default:
                birdHealth = 20;
                break;
        }
    }
}
