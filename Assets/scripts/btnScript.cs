using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnScript : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public Sprite clickedSprite;
    public Sprite notClickedSprite;

	// Use this for initialization
	void Start ()
    {
        spriteRender = GetComponent<SpriteRenderer>();
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
