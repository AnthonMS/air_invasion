using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponUpgrade : MonoBehaviour
{
    
    // This is all the gameobject variables for the different tiers of weapons
    public GameObject weapon_1;
    public GameObject weapon_2;
    public GameObject weapon_3;
    public GameObject weapon_4;
    //public GameObject weapon_5;
    //public GameObject weapon_6;
    //public GameObject weapon_7;
    //public GameObject weapon_8;
    //public GameObject weapon_9;
    //public GameObject weapon_10;

    public bool max;

  

    // Use this for initialization
    void Start()
    {
        weapon_1 = Resources.Load("stone", typeof(GameObject)) as GameObject;
        weapon_2 = Resources.Load("silverAxe", typeof(GameObject)) as GameObject;
        weapon_3 = Resources.Load("ball_bowling1", typeof(GameObject)) as GameObject;
        weapon_4 = Resources.Load("goldenAxe", typeof(GameObject)) as GameObject;
        max = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(max);
    }

    public GameObject GetWeapon(int weaponTier)
    {
        GameObject returnVal = null;
        switch (weaponTier)
        {
            case 1:
                //Debug.Log("case1");
                returnVal = weapon_1;
                break;
            case 2:
                //Debug.Log("case2");
                returnVal = weapon_2;
                break;

            case 3:
                Debug.Log("case3");
                returnVal = weapon_3;
                break;
            case 4:
                Debug.Log("case4");
                returnVal = weapon_4;
                max = true;
                Debug.Log(max);

                break;
            default:
                returnVal = null;
                break;
        }
        return returnVal;
    }

}




