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
    public float fireStartDelay = 1f;
    // Loop fire method
    void Start()
    {
        InvokeRepeating("Warn", fireStartDelay, 1f/fireRate);
    }
    // Warn player before shooting
    void Warn()
    {
        for (int i = 0; i < fireCount; i++) 
        {
            lastLaser = Instantiate(warnObject, new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z-0.15f), transform.rotation);
            Destroy(lastLaser,0.75f);
            transform.Rotate(new Vector3(0,0,360/fireCount));
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        Invoke("Fire",0.75f);
    }
    // Shoot
    void Fire()
    {
        for (int i = 0; i < fireCount; i++) 
        {
            lastLaser = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z-0.15f), transform.rotation);
            Destroy(lastLaser,0.5f);
            transform.Rotate(new Vector3(0,0,360/fireCount));
        }
        transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
    }
}
