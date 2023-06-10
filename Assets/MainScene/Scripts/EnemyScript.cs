using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody enemyRb;
    private Animator enemyAnimator;
    //private float zLowerBound = 9;
    //private int enemyHit = 1;
    private GameManager2 gameManager;

    public float bulletSpeed = 30;
    public GameObject projectilePrefab;
    public GameObject grenadePrefab;
    public Transform positionOfShot;
    public Transform positionOfGrenade;
    private Transform positionOfDrone;
    public GameObject weapon;

    public float startDelay = 1;
    public float shotInterval = 1;
    public float maxShotinterval;
    public float minShotInterval;
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetFloat("Speed_f", 0.3f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();

        InvokeRepeating("EnemyShot", startDelay, shotInterval);
    }

    private void FixedUpdate()
    {
        // Рухаємось в напрямку -Z
        Vector3 movement = new Vector3(0, 0, -speed * Time.deltaTime);
        enemyRb.MovePosition(transform.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrenadeZone"))
        {
            CancelInvoke("EnemyShot");
            speed = 0;
            enemyAnimator.SetFloat("Speed_f", 0);
            StartCoroutine(Grenade());
        }
    }

    void EnemyShot()
    {
        enemyAnimator.SetInteger("WeaponType_int", 2);
        enemyAnimator.SetBool("Shoot_b", false);
        enemyAnimator.SetBool("Reload_b", false);
        if (this.transform.position.z < 35)
        {
            GameObject bullet = Instantiate(projectilePrefab, positionOfShot.position, positionOfShot.rotation);
            bullet.GetComponent<Rigidbody>().velocity = positionOfShot.forward * bulletSpeed;
        }
    }
    public void GrenadeThrow()
    {
        //CancelInvoke("EnemyShot");
        enemyAnimator.SetInteger("WeaponType_int", 10);
        enemyAnimator.SetBool("Shoot_b", true);
    }

    public void GrenadePause()
    {

    }

    IEnumerator Grenade()
    {
        weapon.SetActive(false);
        positionOfDrone = GameObject.Find("Drone").GetComponent<Transform>();
        transform.LookAt(positionOfDrone);
        GrenadeThrow();
        yield return new WaitForSeconds(2);
        GameObject grenade = Instantiate(grenadePrefab, positionOfGrenade.position, positionOfGrenade.rotation);
        Vector3 direction = (positionOfDrone.position +new Vector3(0,10)) - positionOfGrenade.position;
        float randomCoef = Random.Range(0.5f, 2f);
        grenade.GetComponent<Rigidbody>().AddForce(direction*randomCoef,ForceMode.Impulse);
        enemyAnimator.SetBool("Shoot_b", false);
        yield return new WaitForSeconds(2);
        StartCoroutine(Grenade());
        //gameManager.AddDroneLives(-enemyHit);
    }
}
