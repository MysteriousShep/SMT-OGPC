using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBulletShoot : MonoBehaviour
{
    public GameObject projectile;
    public float fireDelay = 0.75f;
    void Start()
    {
        Destroy(gameObject,fireDelay+0.15f);
        Invoke("Fire",fireDelay);
    }

    void Fire()
    {
        transform.Rotate(new Vector3(0,0,180));
        Instantiate(projectile, transform.position, transform.rotation);
    }
}
