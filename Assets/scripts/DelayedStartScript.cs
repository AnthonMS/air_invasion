﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStartScript : MonoBehaviour
{
    public GameObject countDown;
    // Use this for initialization
    void Start ()
    {
        StartCoroutine("StartDelay");
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    IEnumerator StartDelay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3.5f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        //countDown.gameObject.SetActive(false);
        Destroy(countDown.gameObject);
        Destroy(gameObject);
        Time.timeScale = 1;
    }
}