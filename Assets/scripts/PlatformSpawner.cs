using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public float x = 0;

    private Vector2 latestGroundPos;
    private GameObject player;
    private Vector3 playerPos;
    private float i = 0;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckForPlatform();
    }

    private void CheckForPlatform()
    {
        if (i < 15)
        {
            CreatePlatform();
            i++;
        }
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        if (playerPos.x > latestGroundPos.x - 15)
        {
            CreatePlatform();
        }
    }

    private void CreatePlatform()
    {
        float y = 0;
        GameObject groundInstance = Instantiate(Resources.Load("ground", typeof(GameObject))) as GameObject;
        groundInstance.transform.Translate(new Vector3(x, y, 0));
        groundInstance.transform.parent = transform;
        x += 2f;

        latestGroundPos = new Vector2(groundInstance.transform.position.x, groundInstance.transform.position.y);
        //Debug.Log(latestGroundPos);
    }

}
