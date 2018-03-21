using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {

    public float speed = -5f;
    Vector2 startPOS;

	// Use this for initialization
	void Start () {
        startPOS = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);

       
        







    }


} 
