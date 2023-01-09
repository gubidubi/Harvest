using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : StaticShooting
{
    public bool turret;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        delay = 2f;
        numBullets = 20;
        //verificar qual o modo da arma
        if(turret)
        {
            beingHandled = false;
        }

        else
        {
            beingHandled = true;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if(!turret)
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
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && numBullets > 0)
        {
            numBullets--;
            Shoot();
        }
    }
}
