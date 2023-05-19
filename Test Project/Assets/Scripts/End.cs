using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    
    void Update()
    {

        if (GetComponent<SpriteRenderer>().color.a < 1)
        {
            GetComponent<SpriteRenderer>().color = new Color(0,0,0,GetComponent<SpriteRenderer>().color.a+0.001f);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }

}
