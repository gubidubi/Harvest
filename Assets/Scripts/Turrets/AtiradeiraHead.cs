using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtiradeiraHead : MonoBehaviour
{

    public LookAtEnemies lookAt;
    public SpriteRenderer head;
    [SerializeField] GameObject nearestTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        if (nearestTarget == null)
        {
            StartCoroutine(FindEnemy());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nearestTarget != null)
        {
            float angle = Mathf.Atan2(nearestTarget.transform.position.y - gameObject.transform.position.y, nearestTarget.transform.position.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
            // Debug.Log("angle: " + angle);
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
            // Debug.Log("rotation: " + gameObject.transform.rotation);
            if (Mathf.Abs(angle) > 90)
            {
                head.flipY = true;
            }
            else
            {
                head.flipY = false;
            }
        }
        else gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private IEnumerator FindEnemy()
    {
        while (true)
        {
            nearestTarget = lookAt.lookAtEnemy();
            yield return new WaitForSeconds(0.1f);
        }

    }
}
