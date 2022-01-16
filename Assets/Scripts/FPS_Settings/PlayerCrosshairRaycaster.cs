using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshairRaycaster : MonoBehaviour
{
    public float hitRange = 4f;

    public RaycastWeapon riflePrefab;
    public RaycastWeapon laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //raycast forward from main camera
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, hitRange))
        {
            if (hit.transform.tag.Contains("Pickup"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.transform.tag.Contains("Rifle"))
                    {
                        ActiveWeapon activeWeapon = gameObject.GetComponent<ActiveWeapon>();
                        if (activeWeapon)
                        {
                            RaycastWeapon newWeapon = Instantiate(riflePrefab);
                            activeWeapon.Equip(newWeapon);
                            // Supporter
                            RaycastWeapon newSupporterWeapon = Instantiate(riflePrefab);
                            gameObject.GetComponent<PlayerController>().supporter.GetComponent<AiWeapons>().Equip(newSupporterWeapon);
                        }
                    }
                    else if (hit.transform.tag.Contains("Laser"))
                    {
                        ActiveWeapon activeWeapon = gameObject.GetComponent<ActiveWeapon>();
                        if (activeWeapon)
                        {
                            RaycastWeapon newWeapon = Instantiate(laserPrefab);
                            activeWeapon.Equip(newWeapon);
                            // Supporter
                            RaycastWeapon newSupporterWeapon = Instantiate(laserPrefab);
                            gameObject.GetComponent<PlayerController>().supporter.GetComponent<AiWeapons>().Equip(newSupporterWeapon);
                        }
                    } else if (hit.transform.tag.Contains("Grenade"))
                    {
                        GetComponent<GrenadeThrower>().IncrementNumGrenades();
                    }
                    hit.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
