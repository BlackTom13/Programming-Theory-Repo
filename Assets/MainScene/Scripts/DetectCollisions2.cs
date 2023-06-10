using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions2 : MonoBehaviour
{
    private GameManager2 gameManager;
    public ParticleSystem bloodParticle;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (gameObject.CompareTag("Obstacle") && other.gameObject.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
        }

        if (gameObject.CompareTag("Obstacle") && other.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("PhysicObstacles") && collision.gameObject.CompareTag("Projectile"))
        {
            collision.gameObject.SetActive(false);
        }

        if (gameObject.CompareTag("PhysicObstacles") && collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
        }

        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Projectile"))
        {
            bloodParticle.gameObject.SetActive(true);
            bloodParticle.Play();
            gameObject.GetComponent<EnemyLives>().ShootEnemy(1);
            collision.gameObject.SetActive(false);
        }

        if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("EnemyProjectile"))
        {
            gameManager.AddPlayerLives(-1);
            Destroy(collision.gameObject);
            bloodParticle.gameObject.SetActive(true);
            bloodParticle.Play();
            //Debug.Log("Player/Projectile");
        }

        if(gameObject.CompareTag("Projectile") && collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }

        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("EnemyProjectile"))
        {
            bloodParticle.gameObject.SetActive(true);
            bloodParticle.Play();
            gameObject.GetComponent<EnemyLives>().ShootEnemy(1);
            Destroy(collision.gameObject);
        }
    }
}
