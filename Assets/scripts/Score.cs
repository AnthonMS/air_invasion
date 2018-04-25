﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int scorenum;
    public Text scoreText;
    public float health;
    public Text healthText;
    public Text ammoText;
    public float ammo;
    public float tempscore;
    private GameObject player;


    public Score()
    {
        
    }

	// Use this for initialization
	void Start () {
        scorenum = 0;
        health = 100;
        updateScoreP(0);
        player = GameObject.FindGameObjectWithTag("Player");

		
	}
	
	// Update is called once per frame
	void Update () {
        updateAmmo();
<<<<<<< HEAD
<<<<<<< HEAD
      //  Debug.Log(tempscore+=10*Time.deltaTime);
=======
        tempscore += 5 * Time.deltaTime;
        //Debug.Log(tempscore += 5 * Time.deltaTime);
>>>>>>> 263cd1c55331135992440f81406897c3b0a1bcf8
=======
        tempscore += 5 * Time.deltaTime;
        //Debug.Log(tempscore += 5 * Time.deltaTime);
>>>>>>> 263cd1c55331135992440f81406897c3b0a1bcf8
        if(tempscore>=1){
            updateScoreP(1);
            tempscore -= 1;
            
        }
	}

    public void updateScoreP(int score)
    {
        //Debug.Log("updating score");
        scorenum += score;
        scoreText.text = "Score: " + scorenum;

    }
    public void updateScore(){
       // Debug.Log("updating score");
        scorenum++;
        scoreText.text="Score: "+scorenum;
        
    }



    public void updateHealth(float damage)
    {
        Debug.Log("updating health");
        health-=damage;
        healthText.text = "Health: " + health;

    }

    public void updateAmmo(){
        ammo = player.GetComponent<playerStats>().ammo;
        ammoText.text = "Ammo: " + ammo;
    }
}
