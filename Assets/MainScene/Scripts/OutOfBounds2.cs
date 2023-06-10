using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds2 : MonoBehaviour
{
    private float zTopBound = 60;

    void Update()
    {
        if(transform.position.z>zTopBound || transform.position.z<-zTopBound)
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
}
