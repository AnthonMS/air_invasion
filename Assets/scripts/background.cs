using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {


    public float x = 0;
    public float speed;
    Vector3 startPOS;
    private Vector2 latestGroundPos;
    private GameObject player;
    private float i = 0;

    // Use this for initialization
    void Start () {
        startPOS = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);

        CheckForBackground();

    }

    private void CheckForBackground()
    {
        if (i < 1)
        {
            CreateBackground();
            i++;
        }
        startPOS = new Vector2(player.transform.position.x, player.transform.position.y);
        if (startPOS.x > latestGroundPos.x - 15)
        {
            CreateBackground();
        }
    }

    private void CreateBackground()
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
