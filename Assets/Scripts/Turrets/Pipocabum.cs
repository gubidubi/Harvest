using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipocabum : MonoBehaviour
{
    public bool mine;
    private GameObject player;
    public float force = 20f;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int numBullets;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(!mine)
        {
            transform.position = player.transform.position;
            if(player.GetComponent<SpriteRenderer>().flipX == true && 
                gameObject.transform.rotation != Quaternion.Euler(new Vector3(0, 180, 0)))
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            else if(player.GetComponent<SpriteRenderer>().flipX == false && 
                gameObject.transform.rotation != Quaternion.Euler(new Vector3(0, 0, 0)))
            {
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            if(Input.GetKeyDown(KeyCode.E) && numBullets > 0)
            {
                numBullets--;
                Shoot();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy" && mine)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(gameObject.transform.parent);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * force, ForceMode2D.Impulse);
    }
}

