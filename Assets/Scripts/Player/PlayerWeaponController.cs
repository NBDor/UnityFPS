using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
  
    private GameObject[] playerWeapons;
    private bool[] playerPickedWeapons;
    private bool hasAtleastOneWeapon;
    bool isTriggerHit;
    public GameObject CrosshairDefault;
    public GameObject CrosshairPickUp;
    public GameObject CrosshairWeapon;
    public float hitRange = 4f;
    private GameObject CanvasAmmo;
    public Image[] WeaponAmmoIcons;

    // Start is called before the first frame update
    void Start()
    {
        CanvasAmmo = GameObject.FindGameObjectWithTag("CanvasAmmo");
        CanvasAmmo.SetActive(false);
        
        playerWeapons = GameObject.FindGameObjectsWithTag("PlayerWeapon");
        foreach(GameObject weapon in playerWeapons) {
            weapon.SetActive(false);
        }

        playerPickedWeapons = new bool [playerWeapons.Length];
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwapping();
        RaycastHit hit;
        //raycast forward from main camera
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, hitRange))
        {
            if (hit.transform.gameObject.tag == "PickUpGun" || hit.transform.gameObject.tag == "PickUpGrenade")
            {
                if (!isTriggerHit && !hasAtleastOneWeapon)
                {
                    isTriggerHit = true;
                    CrosshairDefault.SetActive(false);
                    CrosshairWeapon.SetActive(false);
                    CrosshairPickUp.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.transform.gameObject.tag == "PickUpGun")
                    {
                        CrosshairDefault.SetActive(false);
                        CrosshairPickUp.SetActive(false);
                        CrosshairWeapon.SetActive(true);

                        PickUpWeapon(hit.transform.gameObject);
                        hit.transform.gameObject.SetActive(false);
                    } 

                    else if(hit.transform.gameObject.tag == "PickUpGrenade")
                    {
                        if (GetComponent<GrenadeThrower>().IncrementNumGrenades())
                        {
                            hit.transform.gameObject.SetActive(false);
                        }
                    }

                    
                }
            }
            else
            {
                if (isTriggerHit && !hasAtleastOneWeapon)
                {
                    isTriggerHit = false;
              
                    CrosshairWeapon.SetActive(false);
                    CrosshairPickUp.SetActive(false);
                    CrosshairDefault.SetActive(true);

                }

            }
        }
    }

    private void WeaponSwapping()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && playerPickedWeapons[0])
        {
            PlayerRifleShooting rifle = GetComponent<PlayerRifleShooting>();
            rifle.enabled = true;
            rifle.UpdateAmmoTextCanvas();
            ShowWeapon(playerWeapons[0]);
            GetComponent<PlayerLaserShooting>().enabled = false;
            WeaponAmmoIcons[0].enabled = true;
            WeaponAmmoIcons[1].enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerPickedWeapons[1])
        {
            GetComponent<PlayerRifleShooting>().enabled = false;
            PlayerLaserShooting laser = GetComponent<PlayerLaserShooting>();
            laser.enabled = true;
            laser.UpdateAmmoTextCanvas();
            ShowWeapon(playerWeapons[1]);
            WeaponAmmoIcons[0].enabled = false;
            WeaponAmmoIcons[1].enabled = true;
        }
    }

    private void ShowWeapon(GameObject weaponToShow)
    {
        foreach (GameObject weapon in playerWeapons)
        {
            if (weaponToShow.name == weapon.name)
            {
                weapon.SetActive(true);
                
            }
            else
            {
                weapon.SetActive(false);
               
            }
        }
    }

    private void PickUpWeapon(GameObject target)
    {
        CanvasAmmo.SetActive(true);
        string pickupNameToTag = target.name.Contains("Rifle_PickUp") ? "Rifle" : "Laser";
        foreach (GameObject weapon in playerWeapons)
        {
            if (pickupNameToTag == weapon.name)
            {
                if (pickupNameToTag == "Rifle")
                {
                    PlayerRifleShooting rifle = GetComponent<PlayerRifleShooting>();
                    rifle.enabled = true;
                    rifle.SetAmmo(PlayerRifleShooting.RIFLE_MAX_AMMO);
                    playerPickedWeapons[0] = true;
                    WeaponAmmoIcons[0].enabled = true;
                    WeaponAmmoIcons[1].enabled = false;
                }
                else 
                {
                    PlayerLaserShooting laser = GetComponent<PlayerLaserShooting>();
                    laser.enabled = true;
                    laser.SetAmmo(PlayerLaserShooting.LASER_MAX_AMMO);
                    playerPickedWeapons[1] = true;
                    WeaponAmmoIcons[0].enabled = false;
                    WeaponAmmoIcons[1].enabled = true;
                }
                
                weapon.SetActive(true);
                hasAtleastOneWeapon = true;
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }
}
