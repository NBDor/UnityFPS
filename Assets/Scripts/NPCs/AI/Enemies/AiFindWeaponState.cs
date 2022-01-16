using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFindWeaponState : AiState
{
    GameObject pickup;
    GameObject[] objects = new GameObject[1];

    public void Enter(AiAgent agent)
    {
        pickup = null;
        agent.navMeshAgent.speed = agent.config.findWeaponSpeed;
    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateId GetId()
    {
        return AiStateId.FindWeapon;
    }

    public void Update(AiAgent agent)
    {
        if (agent.gameObject.name == "AI_Supporter" || agent.gameObject.name == "Ally_Supporter")
        {
            agent.navMeshAgent.stoppingDistance = agent.config.followDistance;
            agent.navMeshAgent.SetDestination(agent.commanderTransform.position);
        }

        // Find Pickup
        if (!pickup)
        {
            pickup = FindPickup(agent);
            if (pickup)
            {
                CollectPickup(agent, pickup);
            }
        }

        if (agent.weapons.HasWeapon()) 
        {
            FindTarget(agent);
        }

        if (agent.gameObject.name == "AI_Commander" && !pickup)
        {
            // Wander
            if (!agent.navMeshAgent.hasPath)
            {
                WorldBounds worldBounds = GameObject.FindObjectOfType<WorldBounds>();
                Vector3 min = worldBounds.min.position;
                Vector3 max = worldBounds.max.position;

                Vector3 randomPosition = new Vector3(
                    Random.Range(min.x, max.x),
                    Random.Range(min.y, max.y),
                    Random.Range(min.z, max.z)
                    );
                agent.navMeshAgent.destination = randomPosition;
            }
        }
    }

    GameObject FindPickup(AiAgent agent)
    {
        int count = agent.sensor.Filter(objects, "Pickup");
        if(count > 0)
        {
            return objects[0];
        }
        return null;
    }

    void FindTarget(AiAgent agent)
    {
        int count = 0;
        if (agent.gameObject.name == "Ally_Supporter")
            count = agent.sensor.Filter(objects, "Enemy");
        else
        {
            count = agent.sensor.Filter(objects, "Character");
        }
        if (count > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.AttackPlayer);
        }
    }

    void CollectPickup(AiAgent agent, GameObject pickup)
    {
        agent.navMeshAgent.destination = pickup.transform.position;
    }
}
