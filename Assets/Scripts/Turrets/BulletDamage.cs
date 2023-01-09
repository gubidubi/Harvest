using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public AudioClip clip;
    [HideInInspector] public float damage;
    private bool arealdyCollided;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && !arealdyCollided)
        {
            other.GetComponent<Health>().AddHealth(-damage);
            arealdyCollided = true;
            Destroy(gameObject);
            // sound effect and particles
            AudioSource.PlayClipAtPoint(clip, transform.position, GameManager.instance.soundVolume);
        }
    }
}
