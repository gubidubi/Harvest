using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooting : MonoBehaviour
{
    private bool beingHandled = false;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float force = 20f;
    public float delay;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!beingHandled)
        {
            StartCoroutine(StaticShoot());
        }
    }

    IEnumerator StaticShoot()
    {
        beingHandled = true;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(gameObject.transform);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * force, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(delay);
        beingHandled = false;
    }

    void OnEnable()
    {
        beingHandled = false;
    }
}
