using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySupporter : NPC
{
     // Start is called before the first frame update
    public GameObject theCommander; 
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject[] enemies;
    public float minDistance = 10;
    public float pickUpRange = 20f;


    void Start()
    {
        theCommander = GameObject.FindWithTag("Enemy Commander");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = theCommander.transform.position;
        animator.SetBool("isWalking", true);
        enemies = GameObject.FindGameObjectsWithTag("Allies");
    
    }

    void Update () {
        // check for weapon else check to pickup weapon
        if(hasWeapon)
        {
            animator.SetBool("hasGun", true);
            GameObject closestEnemy = FindClosestEnemy();
            if(closestEnemy)
            {
                attack(closestEnemy);             
            }
        }
        else
        {
            GameObject weapon = WeaponInRange(pickUpRange);
            if(weapon)
            {
                PickUpWeapon(weapon);
            }
        }
        if(theCommander)
        {
            transform.LookAt(theCommander.transform);
            agent.destination = theCommander.transform.position;
        }

    }

     public GameObject FindClosestEnemy()
    {
        GameObject closest = null;
        float distance = minDistance +1;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            if(enemy != null)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < minDistance)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
}