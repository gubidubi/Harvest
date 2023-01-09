using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemies : MonoBehaviour
{

    private GameObject[] arrayEnemies;

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject lookAtEnemy() // find nearest enemy
    {
        float distance;
        float shorterDistance = float.MaxValue;
        GameObject nearestEnemy = null;

        arrayEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < arrayEnemies.Length; i++)
        {
            // Debug.Log("arrayEnemies: " + arrayEnemies[i]);
            distance = Vector3.Distance(gameObject.transform.position, arrayEnemies[i].transform.position);
            if (distance <= shorterDistance)
            {
                shorterDistance = distance;
                nearestEnemy = arrayEnemies[i];
            }
        }

        return nearestEnemy;
    }

}
