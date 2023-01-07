using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxDistance : MonoBehaviour
{
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.parent.transform.position, transform.position) >= 20)
        {
            Destroy(gameObject);
        }
    }
}
