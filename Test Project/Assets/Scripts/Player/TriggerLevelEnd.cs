using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerLevelEnd : MonoBehaviour
{
    public int level = 1;
    public Vector3 spawnPos = new Vector3(6.75f,-4.5f,0f);
    // When touched by a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // When triggered by the level end
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            level += 1;
            SceneManager.LoadScene("Level1");
        }
    }
}
