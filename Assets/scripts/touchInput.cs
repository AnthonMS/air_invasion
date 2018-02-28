using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchInput : MonoBehaviour
{
    private playerStats playerStats;

	// Use this for initialization
	void Start ()
    {
        playerStats = gameObject.GetComponent<playerStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.right * playerStats.runningSpeed * Time.deltaTime);

        foreach (var touch in Input.touches)
        {
            TouchPhase phase = touch.phase;
            switch (phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("New touch detected at position " + touch.position + " , index " + touch.fingerId);
                    break;
                case TouchPhase.Moved:
                    //Debug.Log("Touch index " + touch.fingerId + " has moved by " + touch.deltaPosition);
                    break;
                case TouchPhase.Stationary:
                    //SDebug.Log("Touch index " + touch.fingerId + " is stationary at position " + touch.position);
                    break;
                case TouchPhase.Ended:
                    //Debug.Log("Touch index " + touch.fingerId + " ended at position " + touch.position);
                    Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                    throwStone(screenRay);
                    break;
                case TouchPhase.Canceled:
                    //Debug.Log("Touch index " + touch.fingerId + " cancelled");
                    break;
            }
        }
	}

    private void throwStone(Ray ray)
    {
        Vector3 touchPos = ray.origin;
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject weaponBullet = Instantiate(playerStats.currentWeapon, spawnPos, Quaternion.identity);
        Vector3 direction = touchPos - weaponBullet.transform.position;
        direction.z = 0;
        direction = direction.normalized;
        weaponBullet.GetComponent<Rigidbody2D>().velocity = direction * weaponBullet.GetComponent<stone>().speed;
    }
}
