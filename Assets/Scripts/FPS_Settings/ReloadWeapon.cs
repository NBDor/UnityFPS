using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{

    public ActiveWeapon activeWeapon;
    public AmmoWidget ammoWidget;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <=0)
            {
                RefillMagazine();
            }
            if (weapon.isFiring)
            {
                ammoWidget.Refresh(weapon.ammoCount);
            }
        }
    }

    void RefillMagazine()
    {
        //Debug.Log("Weapon is being refilled");
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.ammoCount = weapon.clipSize;
        ammoWidget.Refresh(weapon.ammoCount);
        //Debug.Log("Weapon is ready");
    }
}
