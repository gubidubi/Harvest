using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradeiraHead : MonoBehaviour
{

    public LookAtEnemies lookAt;
    private GameObject nearestEnemy = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FindEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestEnemy != null)
        {
            float angle = Mathf.Atan2(nearestEnemy.transform.position.y - gameObject.transform.position.y, nearestEnemy.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
            // Debug.Log("angle: " + angle);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            // Debug.Log("rotation: " + gameObject.transform.rotation);
        }
        else gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator FindEnemy()
    {
        while (true)
        {
            nearestEnemy = lookAt.lookAtEnemy();
            Debug.Log("new nearestEnemy: " + nearestEnemy);
            yield return new WaitForSeconds(1);
        }

    }
}
