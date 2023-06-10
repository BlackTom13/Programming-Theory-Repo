using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward2 : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody enemyRb;
    private Animator enemyAnimator;
    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetFloat("Speed_f", 0.3f);
    }

    private void FixedUpdate()
    {
        // Рухаємось в напрямку -Z
        Vector3 movement = new Vector3(0, 0, -speed * Time.deltaTime);
        enemyRb.MovePosition(transform.position + movement);
    }


}
