using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController2 : MonoBehaviour
{
     float horizontalInput;
     float forwardInput;
    public float playerSpeed = 20;

    private float xRange = 20;
    private float zForwardBound = 30;
    private float zBackBound = -10;

    //public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;

    public bool canShoot = true;
    public bool isReloading = false;
    public int bulletsShot;
    public float lastShotTime;

    public float shootDelay = 2f;
    public int maxBullets = 5;
    public float reloadTime = 5f;

    public TextMeshProUGUI bulletsText;
    private TextMeshProUGUI reloadMessageText;
    public int bulletsAmmo;

    public GameObject reloadMessage;

    private Animator playerAnimator;

    private GameManager2 gameManager2;
    void Start()
    {
        bulletsShot = 0;
        lastShotTime = 0f;
        bulletsAmmo = maxBullets;
        reloadMessageText = reloadMessage.gameObject.GetComponent<TextMeshProUGUI>();
        playerAnimator = GetComponent<Animator>();
        gameManager2 = GameObject.Find("GameManager").GetComponent<GameManager2>();
    }
    void Update()
    {
        if (!gameManager2.isGameOver)
        {
            PlayerMovement();
            PlayerShooting();
            bulletsText.text = bulletsAmmo.ToString();
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * playerSpeed * forwardInput * Time.deltaTime);
        transform.Translate(Vector3.right * playerSpeed * horizontalInput * Time.deltaTime);
        if (horizontalInput!=0 || forwardInput!=0)
        {
            playerAnimator.SetFloat("Speed_f", 1);
        }
        else
        {
            playerAnimator.SetFloat("Speed_f", 0);
        }
        CheckBounds();
    }

    void CheckBounds()
    {
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zForwardBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zForwardBound);
        }
        else if (transform.position.z < zBackBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBackBound);
        }
    }

    void PlayerShooting()
    {
        if (isReloading)
        {
            if (Time.time - lastShotTime >= reloadTime)
            {
                isReloading = false;
                bulletsShot = 0;
                canShoot = true;
                bulletsAmmo = maxBullets;
                reloadMessage.SetActive(false);
            }
        }
        else
        {
            if (canShoot)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShootBullet();
                }
                else if (Input.GetMouseButton(0))
                {
                    if (Time.time - lastShotTime >= shootDelay)
                    {
                        ShootBullet();
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    canShoot = true;
                }
            }
            else
            {
                if (Time.time - lastShotTime >= shootDelay)
                {
                    canShoot = true;
                }
            }

            if (bulletsShot >= maxBullets)
            {// empty ammo
                canShoot=false;
                reloadMessageText.text = "Press R to reload";
                reloadMessage.SetActive(true);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    reloadMessageText.text = "Reloading";
                    isReloading = true;
                    lastShotTime = Time.time;
                }
            }
           
                if (Input.GetKeyDown(KeyCode.R))
                {
                    canShoot = false;
                    reloadMessageText.text = "Reloading";
                    reloadMessage.SetActive(true);
                    isReloading = true;
                    lastShotTime = Time.time;
                }
        }
    }

    void ShootBullet()
    {
        //GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        GameObject bullet = BulletsPooler.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = bulletSpawn.position;

            //
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed;
            bulletsShot++;
            bulletsAmmo--;
            lastShotTime = Time.time;
            canShoot = false;
        }
    }

    public void DieAnimation()
    {
        playerAnimator.SetBool("Death_b", true);
    }
}
