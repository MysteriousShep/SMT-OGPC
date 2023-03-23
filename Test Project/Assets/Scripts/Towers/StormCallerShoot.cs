using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormCallerShoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject warnObject;
    public float range = 0f;
    public float fireRate = 1f;
    public int fireCount = 4;
    private GameObject lastWarning;
    public float fireStartDelay = 2f;
    private int fireIteration = 0;
    private GameObject cursor;
    public bool playing = false;
    // Loop fire method
    void Start()
    {
        cursor = GameObject.Find("Cursor");
    }
    void Update()
    {
        if (cursor != null)
        {
            if (!playing && !cursor.gameObject.activeSelf)
            {
                InvokeRepeating("Warn", fireStartDelay, 1f/fireRate);
                playing = true;
            }
        }
    }
    // Warn player before shooting
    void Warn()
    {
        fireIteration = 0;
        for (int i = 1; i <= fireCount; i++) 
        {
            Invoke("Fire",0.25f*i);
        }
        
    }
    void Fire()
    {
        lastWarning = Instantiate(warnObject, new Vector3(9,transform.position.y+(fireIteration-fireCount/2f)*0.5f+0.25f,-0.2f), transform.rotation);
        lastWarning.GetComponent<WarningBulletShoot>().projectile = projectile;
        fireIteration += 1;
    }
}
