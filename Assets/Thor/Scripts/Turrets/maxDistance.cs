using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxDistance : MonoBehaviour
{
    Vector3 initialPos;

    private void Start() {
        initialPos = transform.position;
    }

    void FixedUpdate()
    {
        if(Vector3.Distance(initialPos, transform.position) >= 20)
        {
            Destroy(gameObject);
        }
        //destruindo a bala se ela viaja muito longe
    }
}
