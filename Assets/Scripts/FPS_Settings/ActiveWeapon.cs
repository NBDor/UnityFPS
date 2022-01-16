using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1
    }
    
    public Transform crossHairTarget;
    public Transform[] weaponSlots;

    public AmmoWidget ammoWidget;

    RaycastWeapon[] equipped_weapons =  new RaycastWeapon[2];
    int activeWeaponIndex;
    bool isHolstered = false;
    

    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon ExistingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (ExistingWeapon)
        {
            Equip(ExistingWeapon);
        }
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if (index < 0 || index >= equipped_weapons.Length)
        {
            return null;
        }
        return equipped_weapons[index];
    }

    // Update is called once per frame
    void Update()
    {
        bool canFire = !isHolstered;
        var weapon = GetWeapon(activeWeaponIndex);
        
        if (weapon)
        {
            if (Input.GetButtonDown("Fire1") && canFire && !weapon.isFiring)
            {
                weapon.StartFiring();
            }

            if (Input.GetButtonUp("Fire1") || !canFire)
            {
                weapon.StopFiring();
            }
            weapon.UpdateWeapon(Time.deltaTime, crossHairTarget.position);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.Primary);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        equipped_weapons[weaponSlotIndex] = weapon;
        
        SetActiveWeapon(newWeapon.weaponSlot);
        //ammoWidget.Refresh(weapon.ammoCount);

    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holserIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;
        if (holserIndex == activateIndex)
        {
            holserIndex = -1;
        }
        StartCoroutine(SwitchWeapon(holserIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterindex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterindex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        isHolstered = false;
        activeWeaponIndex = activateIndex;

    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            weapon.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        var weapon = GetWeapon(index);
        if (weapon)
        {
            weapon.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

}
