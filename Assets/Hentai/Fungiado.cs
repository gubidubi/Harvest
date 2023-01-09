using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungiado : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float force = 10.0f;
    public float damage = 10.0f;

    // Start is called before the first frame update
    public GameObject target;
    Rigidbody2D projectileRb;
    void Start()
    {
        // Player becomes the target if there is no target
        if (!target)
        {
            target = GameObject.Find("Player");
        }
        projectileRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            // Accelerate in the direction of target
            Vector2 forceDirection = (target.transform.position - transform.position).normalized;
            transform.right = forceDirection;
            projectileRb.AddForce(forceDirection * force);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // If the collider hit is from an entity, damage that entity and destroy the projetile
        if (other.CompareTag("Player"))
        {
            ExplosionEffect();
            Destroy(gameObject);
            other.gameObject.GetComponent<Health>().AddHealth(-damage);
        }
    }

    void ExplosionEffect()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = transform.localScale;
        var main = explosion.GetComponent<ParticleSystem>().main;
        main.emitterVelocity = projectileRb.velocity;
        explosion.GetComponent<ParticleSystem>().Play();
    }
}
