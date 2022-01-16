using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float maxTime = 0.5f;
    public float maxDistance = 1.0f;
    public float dieForce = 10.0f;
    public float findWeaponSpeed = 5;
    public float commanderAttackDistance = 10.0f;
    public float supporterAttackDistance = 5.0f;
    public float followDistance = 4;
}
