using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerShoot : MonoBehaviour
{
    public GameObject target;
    public GameObject projectile;
    public float range = 3f;
    public float fireRate = 0.5f;
    public GameObject bottom;

    // Loop fire method
    void Start()
    {
        InvokeRepeating("Fire", 2f, 1f/fireRate);
        Instantiate(bottom,new Vector3(transform.position.x,transform.position.y,transform.position.z+0.15f),transform.rotation);
    }
    // Shoot
    void Fire()
    {
        GameObject[ ] friendlies = GameObject.FindGameObjectsWithTag("Friendly");
        if (friendlies.Length > 0)
        {
            target = friendlies[0];
            foreach(GameObject checkObject in friendlies)
            {
                if (Vector3.Distance(checkObject.transform.position,transform.position) < Vector3.Distance(target.transform.position,transform.position))
                {
                    target = checkObject;
                }
            }
        }
        if (Vector3.Distance(target.transform.position, transform.position) < range)
        {
            float angle = Vector2.SignedAngle(Vector2.right, target.transform.position - transform.position);

            Vector3 targetRotation = new Vector3(0, 0, angle);
            Instantiate(projectile, new Vector3(transform.position.x,transform.position.y,transform.position.z-0.1f), Quaternion.Euler(targetRotation));
            transform.rotation = Quaternion.Euler(targetRotation);
        }
    }
}
