using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

   
    void Start()
    {
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            weapons[current_Weapon_Index].gameObject.SetActive(false);

            current_Weapon_Index += 1;
            if (current_Weapon_Index > 4)
                current_Weapon_Index = 0;

            weapons[current_Weapon_Index].gameObject.SetActive(true);
        }
         

    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];
    }
}
