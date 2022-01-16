using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapons : MonoBehaviour
{
    public enum WeaponState
    {
        Holstered,
        Active,
        Reloading
    }


    public RaycastWeapon currentWeapon;
    Animator animator;
    MeshSockets sockets;
    WeaponIk weaponIk;
    Transform currentTarget;
    WeaponState weaponState = WeaponState.Holstered;
    public float inaccuracy = 0.0f;

    public bool IsActive()
    {
        return weaponState == WeaponState.Active;
    }

    public bool IsHolstered()
    {
        return weaponState == WeaponState.Holstered;
    }

    public bool IsReloading()
    {
        return weaponState == WeaponState.Reloading;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sockets = GetComponent<MeshSockets>();
        weaponIk = GetComponent<WeaponIk>();
    }

    private void Update()
    {
        if (currentTarget && currentWeapon && IsActive())
        {
            Vector3 target = currentTarget.position + weaponIk.targetOffset;
            target += Random.insideUnitSphere * inaccuracy;
            currentWeapon.UpdateWeapon(Time.deltaTime, target);
        }
    }

    public void SetFiring(bool enabled)
    {
        if (enabled)
        {
            currentWeapon.StartFiring();
        }
        else
        {
            currentWeapon.StopFiring();
        }
    }

    public void Equip(RaycastWeapon weapon)
    {
        currentWeapon = weapon;
        //sockets.Attach(weapon.transform, MeshSockets.SocketId.Spine);
        sockets.Attach(currentWeapon.transform, MeshSockets.SocketId.RightHand);
    }

    public void ActivateWeapon()
    {
        StartCoroutine(EquipWeaponAnimation());
    }

    public void DeactivateWeapon()
    {
        SetTarget(null);
        SetFiring(false);
        StartCoroutine(HolsterWeaponAnimation());
    }

    public void ReloadWeapon()
    {
        if(IsActive())
            StartCoroutine(ReloadWeaponAnimation());
    }

    IEnumerator EquipWeaponAnimation()
    {
        animator.SetBool("Equip", true);
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }

        weaponIk.SetAimTransform(currentWeapon.raycastOrigin);
        weaponState = WeaponState.Active;
    }

    IEnumerator HolsterWeaponAnimation()
    {
        weaponState = WeaponState.Holstered;
        animator.SetBool("Equip", false);
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }

        weaponIk.SetAimTransform(currentWeapon.raycastOrigin);
        
    }

    IEnumerator ReloadWeaponAnimation()
    {
        weaponState = WeaponState.Reloading;
        animator.SetTrigger("reload_weapon");
        weaponIk.enabled = false;
        yield return new WaitForSeconds(0.5f);
        while (animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            yield return null;
        }
        RefillMagazine();
        weaponIk.enabled = true;
        weaponState = WeaponState.Active;
    }

    public void DropWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.transform.SetParent(null);
            currentWeapon.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentWeapon.gameObject.AddComponent<Rigidbody>();
            currentWeapon = null;
        }
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }

    public void OnAnimationEvent(string eventName)
    {
        if (eventName == "equipWeapon")
        {
            sockets.Attach(currentWeapon.transform, MeshSockets.SocketId.RightHand);
        }
    }

    public void SetTarget(Transform target)
    {
        weaponIk.SetTargetTransform(target);
        currentTarget = target;
    }

    void RefillMagazine()
    {
        //Debug.Log("Weapon is being refilled");
        RaycastWeapon weapon = currentWeapon;
        weapon.ammoCount = weapon.clipSize;
        //Debug.Log("Weapon is ready");
    }
}
