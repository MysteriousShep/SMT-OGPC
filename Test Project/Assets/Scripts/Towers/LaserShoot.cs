using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject warnObject;
    public float range = 3f;
    public float fireRate = 0.5f;
    public int fireCount = 4;
    private GameObject lastLaser;
    // Loop fire method
    void Start()
    {
        InvokeRepeating("Warn", 2f, 1f/fireRate);
    }
    // Warn player before shooting
    void Warn()
    {
        for (int i = 0; i < fireCount; i++) 
        {
            lastLaser = Instantiate(warnObject, transform.position, transform.rotation);
            Destroy(lastLaser,0.5f);
            transform.Rotate(new Vector3(0,0,360/fireCount));
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        Invoke("Fire",0.5f);
    }
    // Shoot
    void Fire()
    {
        for (int i = 0; i < fireCount; i++) 
        {
            lastLaser = Instantiate(projectile, transform.position, transform.rotation);
            Destroy(lastLaser,0.5f);
            transform.Rotate(new Vector3(0,0,360/fireCount));
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
    }
}
