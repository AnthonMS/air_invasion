using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSrc;
    public float delay;

  
    [Header("Audio Clips")]
    public AudioClip throwingSound;
    public AudioClip hurtSound;
    public AudioClip protectSound;
    public AudioClip killBird;
    public AudioClip spawnBoss;
    public AudioClip bossTakeDamage;
    public AudioClip bossSpawnMusic;
    public AudioClip gameOver;


	// Use this for initialization
	void Start ()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayDelayed(delay);

    
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

    public void PlayKillBirdSound()
    {
        audioSrc.PlayOneShot(killBird);
    }
    
    public void PlayBossSpawnSound()
    {
        audioSrc.PlayOneShot(spawnBoss);

    }
    public void PlayBossTakeDamageSound()
    {
        audioSrc.PlayOneShot(bossTakeDamage);
    }
    public void PlayBossTheme()
    {
        audioSrc.PlayOneShot(bossSpawnMusic);
        

        
        
    }





    public void GameOverSound()
    {
        audioSrc.PlayOneShot(gameOver);
    }
}


   // public PlayDelayedSound(AudioClip bossSpawnMusic, float delay)
  //  {
   //     audioSrc.PlayOneShot(bossSpawnMusic);
   //     yield return new WaitForSeconds(10);
        
   // }