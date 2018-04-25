using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeManager : MonoBehaviour {

    private GameObject player;
    private GameObject upgradeMan;
    private Vector3 playerPos;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        upgradeMan = GameObject.FindGameObjectWithTag("UpgradeManager");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnWeapon(){
        Debug.Log("spawn wepaon upgrademangager");
        if (upgradeMan.GetComponent<weaponUpgrade>().maxwep == false)
        {
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            GameObject weaponUpgrade = Instantiate(Resources.Load("weaponUpgrade", typeof(GameObject))) as GameObject;
            weaponUpgrade.transform.Translate(new Vector3(playerPos.x + 5, playerPos.y, 0));
        }
    }
}


