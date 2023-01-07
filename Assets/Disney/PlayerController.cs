using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;
    private Vector2 moveDirection;
    private Vector2 mousePos; //Posição do Mouse em Vector 2
    private Vector2 mouseRelPos; //Posição Relativa do Mouse em relação ao player
    private GameObject cam;

	public enum playerState
	{
		Idle, Walking, Attacking
	}
// Animation
	public playerState currentState;
	string currentAnimation;
	const string PLAYER_IDLE_FRONT = "Player_IdleFront";
	const string PLAYER_IDLE_SIDE = "Player_IdleSide";
	const string PLAYER_IDLE_BACK = "Player_IdleBack";
	const string PLAYER_WALK_FRONT = "Player_WalkFront";
	const string PLAYER_WALK_SIDE = "Player_WalkSide";
	const string PLAYER_WALK_BACK = "Player_WalkBack";

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //Setar a posição relativa do mouse:
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        mouseRelPos = mousePos - rb.position;
        

        if(moveX > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            //cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(moveX < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            //gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            //cam.transform.rotation *= Quaternion.Euler(0, 180, 0);
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    void ChangeState()
    {
        // Geometria analítica kk
        if(mouseRelPos.y > mousePos.x)
        {
            // CIMA
            if(mouseRelPos.x + mouseRelPos.y > 0)
            {
                SetAnimation(PLAYER_IDLE_FRONT);
            }
            // ESQUERDA
            else
            {

            }
        }
        else 
        {
            // DIREITA
            if(mouseRelPos.x + mouseRelPos.y > 0)
            {

            }
            // BAIXO
            else
            {

            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveSpeed * moveDirection.x, moveSpeed * moveDirection.y);
    }

    private void SetAnimation(string newAnimation)
	{
		if (newAnimation != currentAnimation)
		{
			animator.Play(newAnimation);
			currentAnimation = newAnimation;
		}
	}
}
