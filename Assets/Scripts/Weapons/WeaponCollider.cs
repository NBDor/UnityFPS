using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponCollider : MonoBehaviour
{

    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("OnCollisionEnter called _collision is: " + _collision);

        if(_collision.gameObject.tag == "Enemy" || _collision.gameObject.tag == "Enemy Commander")
        {
            NPC npc = _collision.gameObject.GetComponent<NPC>();
            npc.PickUpWeapon(gameObject);
        }
    }
}
