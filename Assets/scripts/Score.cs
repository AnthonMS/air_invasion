using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public int scorenum;
    public Text scoreText;


    public Score()
    {
        
    }

	// Use this for initialization
	void Start () {
        scorenum = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateScore(){
        Debug.Log("updating score");
        scorenum++;
        scoreText.text="score="+scorenum;
        
    }
}
