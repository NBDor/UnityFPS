using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : NPC
{
    private float timeToChangeDirection =5f;
    private UnityEngine.AI.NavMeshAgent agent;
    UnityEngine.AI.NavMeshPath path;
    public float timeForNewPath = 5f;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    public float minDistance = 10;
    public float pickUpRange = 10f;
    private GameObject[] enemies;
    // WeaponCollider weaponCollider;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        path = new UnityEngine.AI.NavMeshPath();
        enemies = GameObject.FindGameObjectsWithTag("Allies");
        // weaponCollider = GetComponent<WeaponCollider>();
        // if(weaponCollider != null)
        // {
        //     weaponCollider.onWeaponCollisionEvent += OnWeaponCollision;
        // }

    }
    // public void OnWeaponCollision(IEventSource _source,NPC _npc)
    public void OnWeaponCollision(GameObject weapon)
    {
        Debug.Log("OnWeaponCollision called with weapon is: " + weapon);
        PickUpWeapon(weapon);
    }
    // private void OnDestroy()
    // {
    //     if(weaponCollider != null)
    //     {
    //         weaponCollider.onWeaponCollisionEvent -= OnWeaponCollision;
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        if(hasWeapon)
        {
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
        
        if(!inCoRoutine)
        {
            StartCoroutine(DoSomthing());
        }

    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20,20);
        float z = Random.Range(-20,20);
        Vector3 pos = new Vector3(x,0,z);
        return pos;
    }
    IEnumerator DoSomthing()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        validPath = agent.CalculatePath(target,path);
        while(!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = agent.CalculatePath(target,path);
        }
        inCoRoutine = false;
    }
    void GetNewPath()
    {
        target = getNewRandomPosition();
        animator.SetBool("isWalking", true);
        agent.SetDestination(target);
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
