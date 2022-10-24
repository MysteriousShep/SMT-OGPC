using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefeated : MonoBehaviour
{
    public int life = 10;

    void FixedUpdate()
    {
        life -= 1;
        if (life < 0)
        {
            Destroy(gameObject);
        }
    }
}
