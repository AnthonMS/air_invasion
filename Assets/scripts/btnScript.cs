using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnScript : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public Sprite clickedSprite;
    public Sprite notClickedSprite;

    public Vector3 offset;

	// Use this for initialization
	void Start ()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        // This takes the button and positions it in the bottom left corner, with an offset. 
        // (Offset is set in Unity on the gameobject that holds this script)
        transform.position = Camera.main.ScreenToWorldPoint(offset);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ButtonClicked(bool clicked)
    {
        if (clicked)
            spriteRender.sprite = clickedSprite;

        if (!clicked)
            spriteRender.sprite = notClickedSprite;
    }

}
