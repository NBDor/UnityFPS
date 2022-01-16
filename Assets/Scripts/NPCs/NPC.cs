using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasWeapon = false;
    public Animator animator;

    public void attack(GameObject enemy)
    {
        // Debug.Log(transform.name + " attacked " + enemy.transform.name);
        transform.LookAt(enemy.transform.position);
        // check if has weapon
        CharacterStats sn = enemy.GetComponent<CharacterStats>();
        animator.SetBool("isShooting", true);
        sn.TakeDamage(5);
        // enemy.TakeDamage(5);
    }
    public GameObject WeaponInRange(float range)
    {
        // Debug.Log(" WeaponInRange called with range: " + range);
        var ray = new Ray(this.transform.position,this.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,range))
        {
            // Debug.Log(" WeaponInRange we hit somthing: " + hit);
            if (hit.transform.gameObject.tag == "PickUpGun" || hit.transform.gameObject.tag == "PickUpGrenade")
            {
                Debug.Log(" WeaponInRange we hit weapon: " + hit);
                return hit.transform.gameObject;
            }
        }
        return null;
    }
    public void PickUpWeapon(GameObject weapon)
    {
        Debug.Log("PickUpWeapon called with weapon: " + weapon);
        hasWeapon = true;
        // Disable pickupWeapon
        weapon.SetActive(false);
        // Enable Ally_handheldWeapon
        animator.SetBool("hasGun", true);
    }
}
