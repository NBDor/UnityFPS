using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f;
    public GameObject grenadePrefab;
    private int grenadeCounter = 0;
    public int maxNumOfGrenades = 3;
    private GameObject[] UIGrenadesIcons;

    private void Start()
    {
        UIGrenadesIcons = GameObject.FindGameObjectsWithTag("GrenadeIcon");
        foreach (GameObject grenadeIcon in UIGrenadesIcons)
        {
            grenadeIcon.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && grenadeCounter > 0)
        {
            ThrowGrenade();
            grenadeCounter--;
            UIGrenadesIcons[grenadeCounter].SetActive(false);
        }       
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    public bool IncrementNumGrenades()
    {
        if (grenadeCounter < maxNumOfGrenades)
        {
            UIGrenadesIcons[grenadeCounter].SetActive(true);
            grenadeCounter++;
            return true;
        }
        return false;
        
        
    }

}
