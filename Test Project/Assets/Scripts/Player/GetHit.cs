using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    public int health = 10; // How many hits this object can take before dying
    public bool dead = false; // Is this object dead?

    // Every frame
    void Update()
    {
        // When health is equal to or below zero
        if (health <= 0)
        {
            dead = true; // Mark this object as dead
            Destroy(gameObject);
        }
    }

    // When touched by a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // When triggered by an enemy projectile
        if (other.gameObject.CompareTag("Enemy Projectile"))
        {
            health -= 1; // Decrease health by one
            Destroy(other.gameObject); // Destroy the enemy projectile
        }
    }
}
