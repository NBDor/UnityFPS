using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Grenade_WorldPickUp")
        {
            other.gameObject.GetComponent<AiGrenadeThrower>().enabled = true;
            Destroy(gameObject);
        }
        else
        {
            AiWeapons AiWeapons = other.gameObject.GetComponent<AiWeapons>();
            if (AiWeapons && (other.gameObject.name == "Player" || other.gameObject.name == "AI_Commander"))
            {
                // Commander
                RaycastWeapon newCommanderWeapon = Instantiate(weaponPrefab);
                AiWeapons.Equip(newCommanderWeapon);
                // Supporter
                RaycastWeapon newSupporterWeapon = Instantiate(weaponPrefab);
                other.gameObject.GetComponent<AiAgent>().supporter.GetComponent<AiWeapons>().Equip(newSupporterWeapon);
                Destroy(gameObject);
            }
        }
    }
}
