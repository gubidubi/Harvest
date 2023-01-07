using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticShooting : MonoBehaviour
{
    [HideInInspector]public bool beingHandled;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float force = 20f;
    [HideInInspector]public float delay; //tempo entre os disparos
    public int numBullets;
    public float water;

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(!beingHandled)
        {
            StartCoroutine(StaticShoot());
        }
    }

    IEnumerator StaticShoot()
    {
        beingHandled = true;

        Shoot();
        
        yield return new WaitForSeconds(delay);
        beingHandled = false;
    }

    void OnEnable()
    {
        beingHandled = false;
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(gameObject.transform.parent);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * force, ForceMode2D.Impulse);
    }
}
