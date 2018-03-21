using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jump()
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("ClickButtonNew", true);
        Debug.Log("jump");
    }

    public void Protect(bool value)
    {
        GameObject.FindGameObjectWithTag("Player").SendMessage("ClickProtectButton", value);

    }
}
