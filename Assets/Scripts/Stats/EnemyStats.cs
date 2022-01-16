using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Threading.Tasks;

public class EnemyStats : CharacterStats
{
    public Animator animator;


    public override void TakeDamage(float damage)
    {
        animator.SetBool("gotHit", true);
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        animator.SetBool("isDead", true);
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        FindObjectOfType<GameController>().checkVictory();
    }  
}
