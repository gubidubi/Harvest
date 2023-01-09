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
    Rigidbody2D player;
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
    [Header("Convenções de Distâncias")]
    public float slowDistance;
    public float changeTargetDistance;
    public float canDiveDistance;

    [Header("Mecânica de Dive")]
    public float setDiveXVelocity;
    public float setDiveTime;
    public float diveLinearDrag;
    public float diveHeight;

    [Header("Ponto de Primeira Aproximação (relativo ao Dive Point)")]

    public Vector2 firstApproachRelPoint;
    private Vector2 firstApproachAbsPoint;
    [Header("Apenas Visualização:")]
    [SerializeField] private Vector2 diveVelocity;
    [SerializeField] private float diveTime;
    [SerializeField] private side diveSide;
    [SerializeField] private float diveRange;
    [SerializeField] private float diveForce;
    [SerializeField] private Vector2 divePoint;    

    [Header("Algumas variáveis auxiliares")]
    [SerializeField] private float currentForce;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float distance;


    private enum side
    {
        left, right
    }

    // State Machine:
    private enum state
    {
        fly, flyToDive, dive, flee, FlyAway
    }
    state currentState;
    string[] strStates = { "Crow_Fly", "Crow_Fly", "Crow_Dive", "Crow_Flee", "Crow_Fly" }; //vetor de string states

    // Start is called before the first frame update
    void Start()
    {
        // getting components
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        fearRadius = gameObject.GetComponentInChildren<Rigidbody2D>();
        playerCheck = gameObject.GetComponentInChildren<CheckIfPlayerIsHere>();
        

        // setting initial status
        currentState = state.fly;
        animator.Play(strStates[(int)state.fly]);

        //Setando algumas "constantes"
        diveTime = setDiveTime;
        diveForce = 2*(diveHeight * rb.mass) / (diveTime * diveTime); //Cinemática para uma parábola perfeitinha
        diveVelocity = new Vector2(setDiveXVelocity, diveForce*diveTime); 
        diveRange = (diveVelocity.x * diveTime);
        findTarget();

        Debug.Log("Start finalizado");
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
        animator.Play(strStates[(int)newState]);

        currentState = newState;
        Debug.Log("newState: " + newState);

        if (newState == state.fly || newState == state.flee || newState == state.flyToDive)
            rb.drag = flyLinearDrag;
        else if (newState == state.dive)
        {
            Debug.Log("DIVE!");
            rb.drag = diveLinearDrag;
            rb.velocity = new Vector2((-1 * ((int)diveSide * 2 - 1)) * diveVelocity.x, -1 * diveVelocity.y);
        }
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

       IEnumerator Flee()
    {
        Debug.Log("Inicio do Flee");
        ChangeState(state.flee);
        yield return new WaitForSeconds(fleeTime);
        Debug.Log("Final do Flee. Mudando Estado para fly");
        findTarget();
        ChangeState(state.fly);
    }
    
    void MoveEnemy()
    {
        if (currentState == state.flee || currentState == state.FlyAway)
        {
            direction = -1*(player.position - rb.position).normalized;
            rb.AddForce(direction * fleeForce);
        }
        else
        {
            //Se, na realidade o player está ali, deve corrigir seu estado e iniciar a Co-rotina de fuga
            if (playerCheck.playerIsHere)
            {
                Debug.Log("O corvo vê que o player está aqui.");
                StartCoroutine(Flee());
                Debug.Log("Em tese já dei Flee");
            }
            else
            {
                if (currentState == state.fly)
                {
                    distance = (firstApproachAbsPoint - rb.position).magnitude;
                    if (distance > slowDistance)
                        currentForce = flyForce;
                    else if (distance > changeTargetDistance)
                        currentForce = flyForce / 1.5f;
                    else
                        ChangeState(state.flyToDive);

                    //se não trocou de estado, realizar o resto:
                    if (currentState == state.fly)
                    {
                        direction = (firstApproachAbsPoint - rb.position).normalized;
                        rb.AddForce(direction * currentForce);
                    }
                }
                else if (currentState == state.flyToDive)
                {
                    currentForce = flyForce;
                    distance = (divePoint - rb.position).magnitude;
                    if (distance > canDiveDistance)
                    {
                        Debug.Log("Can NOT Dive!");
                        direction = (divePoint - rb.position).normalized;
                        rb.AddForce(direction * currentForce);
                    }
                    else
                    {
                        Debug.Log("Can Dive!");
                        ChangeState(state.dive);
                    }

                }
                else if (currentState == state.dive)
                {
                    //se ainda nao completou o dive, continuar aplicando força:
                    if (rb.position.y < divePoint.y + canDiveDistance +  0.01f)
                        rb.AddForce(Vector2.up * diveForce);
                    else
                    {
                        findTarget();
                        ChangeState(state.fly);
                    }
                }
            }
        }

    }

 

    bool findTarget()
    {
        // find plants
        plants = GameObject.FindGameObjectsWithTag("Planta");
        Debug.Log("Array Plantas: "); 
        foreach(GameObject plant in plants)
            Debug.Log(plant.name); 

        if (plants.Length == 0)
        {
            Debug.Log("No plants found. Fly away");
            StartCoroutine(FlyAway());
            return false;
        }
        else
        {
            // find target plant
            randomIndex = Random.Range(0, plants.Length);
            Debug.Log("Plants Lenght: " + plants.Length);
            Debug.Log("randomIndex: " + randomIndex);

            targetPlant = plants[randomIndex];
            Debug.Log("targetPlant: " + targetPlant);

            diveSide = (side)Random.Range(0, 2);
            divePoint = targetPlant.GetComponent<Rigidbody2D>().position + new Vector2(diveRange * ((int)diveSide * 2 - 1), diveHeight);
            Debug.DrawRay(divePoint, Vector2.down*0.02f, Color.red, 15f);

            if (diveSide == side.left)
                firstApproachAbsPoint = divePoint + new Vector2(-1*firstApproachRelPoint.x,firstApproachRelPoint.y);
            else
                firstApproachAbsPoint = divePoint + new Vector2(firstApproachRelPoint.x,firstApproachRelPoint.y);
            Debug.DrawRay(firstApproachAbsPoint, Vector2.down*0.02f, Color.white, 15f);
            return true;
        }
    }

    //Dar dano na planta
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Planta" && currentState == state.dive)
            Debug.Log("Deu dano na planta");
        //Dar dano na planta
    }

    IEnumerator FlyAway()
    {
        currentState = state.FlyAway;
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(2f);
            if(findTarget())
            {
                currentState = state.fly;    
                yield break;
            }
        }
        Destroy(gameObject); //sumiu no horizonte.
    }
}
