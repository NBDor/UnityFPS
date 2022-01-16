using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    public void Enter(AiAgent agent)
    {
        agent.weapons.DeactivateWeapon();
        agent.navMeshAgent.ResetPath();
    }

    public void Exit(AiAgent agent)
    {

    }

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }

    public void Update(AiAgent agent)
    {

    }
}
