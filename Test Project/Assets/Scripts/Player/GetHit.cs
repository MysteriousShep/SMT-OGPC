using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetHit : MonoBehaviour
{
    public int health = 10; // How many hits this object can take before dying
    public bool dead = false; // Is this object dead?
    public int iFrames = 0;

    // Every frame
    void Update()
    {
        iFrames -= 1;
        // When health is equal to or below zero
        if (health <= 0)
        {
            dead = true; // Mark this object as dead
            SceneManager.LoadScene("Level1");
        }
    }

    // When hit
    public void Hit(int damage)
    {
        if (iFrames < 0) {
            health -= damage; // Decrease health
            iFrames = 30;
        }
    }
}
