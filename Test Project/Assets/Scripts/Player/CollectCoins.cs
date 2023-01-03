using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    public int coins = 0;

    // When touched by a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // When triggered by a coin
        if (other.gameObject.CompareTag("Currency"))
        {
            coins += 1; // Increase coin count by one
            Destroy(other.gameObject); // Destroy the coin
        }
    }
}
