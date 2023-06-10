using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float bulletSpeed = 30;
    public GameObject projectilePrefab;
    public Transform positionOfShot;
    public float startDelay = 1;
    public float shotInterval = 1;
    public float maxShotinterval;
    public float minShotInterval;

    void Start()
    {
        InvokeRepeating("EnemyShot", startDelay, shotInterval);
    }

    void EnemyShot()
    {
        if (this.transform.position.z < 25)
        {
            GameObject bullet = Instantiate(projectilePrefab, positionOfShot.position, positionOfShot.rotation);
            bullet.GetComponent<Rigidbody>().velocity = positionOfShot.forward * bulletSpeed;
        }
    }
}
