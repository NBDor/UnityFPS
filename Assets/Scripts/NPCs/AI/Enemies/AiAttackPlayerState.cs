using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    private float stoppingDistance;
    public void Enter(AiAgent agent)
    {
        agent.weapons.ActivateWeapon();
        agent.weapons.SetTarget(agent.targetTransform);
        if (agent.gameObject.name == "AI_Commander")
        {
            stoppingDistance = agent.config.commanderAttackDistance;
        }
        else
        {
            stoppingDistance = agent.config.supporterAttackDistance;
        }
        agent.navMeshAgent.stoppingDistance = stoppingDistance;
    }

    public void Exit(AiAgent agent)
    {
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }

    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.targetTransform.position;
        ReloadWeapon(agent);
        UpdateFiring(agent);

        if (agent.targetTransform.gameObject.name == "Player")
        {
            if (agent.targetTransform.GetComponent<PlayerHealth>().IsDead())
            {
                agent.stateMachine.ChangeState(AiStateId.Idle);
            }
        }
        else if (agent.gameObject.name == "Ally_Supporter")
        {
            GameObject enemy = GameObject.FindWithTag("Enemy");
            GameObject enemyCommander = GameObject.FindWithTag("Enemy Commander");

            if (enemy.GetComponent<Health>().IsDead() && enemyCommander.GetComponent<Health>().IsDead() || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().IsDead())
            {
                agent.stateMachine.ChangeState(AiStateId.Idle);
            }
        }

        else
        {
            agent.navMeshAgent.destination = agent.commanderTransform.position;
        }
    }

    private void UpdateFiring(AiAgent agent)
    {
        if (agent.sensor.IsInSight(agent.targetTransform.gameObject))
        {
            if (agent.GetComponent<AiGrenadeThrower>().enabled)
            {
                agent.GetComponent<AiGrenadeThrower>().ThrowGrenade();
            }
            agent.weapons.SetFiring(true);
        }
        else
        {
            agent.weapons.SetFiring(false);
        }
    }

    void ReloadWeapon(AiAgent agent)
    {
        var weapon = agent.weapons.currentWeapon;
        if (weapon && weapon.ammoCount <= 0)
        {
            agent.weapons.ReloadWeapon();
        }
    }

}
