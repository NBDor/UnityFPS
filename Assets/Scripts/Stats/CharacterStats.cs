using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    private int currentStamina;

    public HealthBar healthbar;

    // void Awake()
    // {
    //     currentHealth = maxHealth;
    //     healthbar.SetMaxValue(maxHealth);
    // }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxValue(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamage(float damage)
    {

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " Died.");
    }

}