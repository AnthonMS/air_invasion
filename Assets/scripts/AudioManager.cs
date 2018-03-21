using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSrc;
  

    [Header("Audio Clips")]
    public AudioClip throwingSound;
    public AudioClip hurtSound;
    public AudioClip protectSound;


    

	// Use this for initialization
	void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayThrowingSound()
    {
        audioSrc.PlayOneShot(throwingSound);
    }

    public void PlayHurtSound()
    {
        audioSrc.PlayOneShot(hurtSound);
    }

    public void PlayProtectSound()
    {
        audioSrc.PlayOneShot(protectSound);
    }
}
