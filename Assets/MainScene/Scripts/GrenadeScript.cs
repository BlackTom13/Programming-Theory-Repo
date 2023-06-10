using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private GameManager2 gameManager;
    public ParticleSystem explosionParticle;
    private MeshRenderer mesh;
    private Rigidbody rb;
    private int enemyHit = 1;
    public bool inDroneZone = false;
    public float power=300;
    public float radius=20;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
        rb = GetComponent<Rigidbody>();
        mesh = this.GetComponent<MeshRenderer>();
        StartCoroutine(GrenadeBlowUp());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroneZone"))
        {
            Debug.Log("In DroneZone");
            inDroneZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DroneZone"))
        {
            Debug.Log("Out DroneZone");
            inDroneZone = false;
        }
    }

    IEnumerator GrenadeBlowUp()
    {
        yield return new WaitForSeconds(3);
        Explosion();
        explosionParticle.Play();
        mesh.enabled = false;
        if (inDroneZone)
        {
            gameManager.AddDroneLives(-enemyHit);
        }
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }

    void Explosion()
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                
                if (rb.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player Bang");
                    rb.AddExplosionForce(power * 50, explosionPos, radius);
                    gameManager.AddPlayerLives(-1);
                }
                if (rb.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy Bang");
                    rb.AddExplosionForce(power * 50, explosionPos, radius);
                    rb.gameObject.GetComponent<EnemyLives>().ShootEnemy(1);
                }
                rb.AddExplosionForce(power, explosionPos, radius);

            }
                
        }
    }

}
