using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float maxDamage = 100f;
    public GameObject exposionEffect;
    private float countdown;
    private bool hasExploded = false;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            StartCoroutine(Explode());
            hasExploded = true;
        }
    }

    IEnumerator Explode()
    {
        AudioManager.Instance.Play("GrenadeExplosion");
        // Show effect
        Instantiate(exposionEffect, transform.position, transform.rotation);
         // Get nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
             // Add force
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            // Damage
            
            Vector3 distanceVector = (this.transform.position) - (nearbyObject.transform.position);
            float damageToDeal = maxDamage - 15 * (distanceVector).magnitude;
            if (nearbyObject.tag == "Player")
            {
                nearbyObject.GetComponent<PlayerHealth>().TakeDamage(damageToDeal, distanceVector);
            }
            else if (nearbyObject.tag.Contains("Enemy") || nearbyObject.tag == "Allies")
            {
                nearbyObject.GetComponent<Health>().TakeDamage(damageToDeal, distanceVector);
            }
        }

        sr.color = new Color(0f, 0f, 0f, 0f);
        // Remove grenade
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

  
}
