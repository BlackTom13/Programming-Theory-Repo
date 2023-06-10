using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounds2 : MonoBehaviour
{
    private float zLowerBound = 9;
    private int enemyHit = 1;
    private Animator enemyAnimator;
    private GameManager2 gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager2>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < zLowerBound)
        {
            //Destroy(gameObject);
            GrenadeThrow();
            gameManager.AddDroneLives(-enemyHit);
        }
    }

    public void GrenadeThrow()
    {
        enemyAnimator.SetInteger("WeaponType_int",10);
        enemyAnimator.SetBool("Shoot_b", true);
    }
}
