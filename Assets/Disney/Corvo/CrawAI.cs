using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawAI : MonoBehaviour
{
    // Components
    public Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Rigidbody2D fearRadius;
    GameObject targetPlant;
    GameObject[] plants;
    CheckIfPlayerIsHere playerCheck;
    int randomIndex;

    // Enemy parameters (Craw)
    [Header("Atributos Fundamentais")]
    public int diveDamange;
    [Header("Movimentação")]
    public float flyForce;
    public float flyLinearDrag;
    public float fleeForce;
    public float fleeTime;
    [Header("Mecânica de Dive")]
    public Vector2 diveVelocity;
    public float diveTime;
    public float diveLinearDrag;
    public float diveHeight;
    [Header("Apenas Visualização:")]
    [SerializeField] side diveSide;
    [SerializeField] float diveRange;


    [Header("Ponto de Primeira Aproximação (relativo ao Dive Point)")]

    public Vector2 firstApproachRelPoint;
    private Vector2 firstApproachAbsPoint;
    private Vector2 divePoint;
    private float currentForce;

    private enum side
    {
        left, right
    }

    // State Machine:
    private enum state
    {
        fly,
        flyToDive, dive, flee
    }
    state currentState;
    string[] strStates = { "Enemy_Fly", "Enemy_Fly", "Enemy_Dive", "Enemy_Flee" }; //vetor de string states

    // Start is called before the first frame update
    void Start()
    {
        // getting components
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        fearRadius = gameObject.GetComponentInChildren<Rigidbody2D>();
        playerCheck = gameObject.GetComponentInChildren<CheckIfPlayerIsHere>();

        // setting initial status
        currentState = state.fly;
        animator.Play(strStates[(int)state.fly]);

        plants = GameObject.FindGameObjectsWithTag("Planta");
        findTarget();
    }

    // Update is called once per frame
    void Update()
    {
        DoTheFlip();
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    private void FixedUpdate()
    {
        MoveEnemy();
    }

    // Change State
    private void ChangeState(state newState)
    {
        if (newState == currentState)
            return;
        //if (for diferente de null blablabla) (no nosso nao precisa)
        animator.Play(strStates[(int)state.dive]);

        currentState = newState;

        if (newState == state.fly || newState == state.flee || newState == state.flyToDive)
            rb.drag = flyLinearDrag;
        else if(newState == state.dive)
            rb.drag = diveLinearDrag;
    }

    // Verifica se determinada animação rodou (só serve para animações de uso único, então talvez não seja necessário)
    bool IsAnimationPlaying(Animator animator, string stateName)
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else return false;
    }

    void DoTheFlip()
    {
        if (rb.velocity.x > 0)
            sprite.flipX = false;
        else if (rb.velocity.x < 0)
            sprite.flipX = true;
    }

    void MoveEnemy()
    {
        //Durante o flee chamará uma Coroutine, então ela não será alterada pela máquina de estados
        if (currentState != state.flee)
        {
            if (playerCheck.playerIsHere)
                ChangeState(state.flee);
            else
            {
                
            }
        }

    }

    /// <summary>
    /// Sent each frame where another object is within a trigger collider
    /// attached to this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerStay2D(Collider2D other)
    {

    }

    void findTarget()
    {
        // find target plant
        
        randomIndex = Random.Range(0, plants.Length);
        targetPlant = plants[randomIndex];
        diveRange = (diveVelocity.x * diveTime) / 2;
        diveSide = (side)Random.Range(0, 2);
        divePoint = new Vector2(diveRange * ((int)diveSide * 2 - 1), diveHeight);
        if (diveSide == side.left)
            firstApproachAbsPoint = divePoint - firstApproachRelPoint;
        else
            firstApproachAbsPoint = divePoint + firstApproachRelPoint;
    }

}
