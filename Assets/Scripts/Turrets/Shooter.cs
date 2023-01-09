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
    
    // FixedUpdate descartado, agora a direção da arma é controlada pelo script AtiradeiraHead

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && numBullets > 0)
        {
            numBullets--;
            Shoot();
        }
    }
}
