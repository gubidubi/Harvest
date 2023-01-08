using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;
    SpriteRenderer sprite;
    private Vector2 moveDirection;

    public bool facingRight = false;

    //private GameObject cam;

    void Start()
    {
        //Components
        //cam = GameObject.FindWithTag("MainCamera");
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        int x = ProcessInput();

        UpdateAnimation(x);
        /*
        gather input
        if(notImpedido)
            move char
        change sprite
        */
    }

    void UpdateAnimation(int direction = 0)
    {
        var gm = GameManager.instance;
        if (!gm.isPaused && gm.isGameActive && direction != 0)
        {
            //proceeds to flip animation, if needed
            if (direction == 1)
            {
                sprite.flipX = true;
                //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                //cam.transform.rotation *= Quaternion.Euler(0, 180, 0);
            }
            else
            {
                sprite.flipX = false;
                //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                //cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    int ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Speed", moveDirection.magnitude);

        if (moveX != 0)
        {
            if (moveX > 0)
                return 1;
            else
                return -1;
        }
        return 0;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);
    }
}
