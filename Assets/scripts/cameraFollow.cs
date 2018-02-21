﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    private GameObject player;
    private Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // LateUpdate is called after Update each frame
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
