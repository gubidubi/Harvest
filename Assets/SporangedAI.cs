using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporangedAI : MonoBehaviour
{
        // Components
    private GameObject thePlayer;
    private Rigidbody2D player;
    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer sprite;

    // Atributes
    [Header("Atributos de Movimentação")]
    public float axialWalkForce; // Força centrípeta para o player
    public float tangWalkForce; // Força para deslocamento tangente
    public float minDist;
    public float maxDist;
    public float pickSideCooldown;
    public float pickDistCooldown;


    [Header("Mecânica de Tiro")]
    public float fireCooldown;
    public float instatiateBulletTimeRate;
    public float bulletSporangedDist; //distancia entre o projétil e o Sporanged ao atirar.
    public GameObject bullet; 

    [Header("Apenas Visualização")]
    [SerializeField] private float currentDist;
    [SerializeField] private float targetDist;
    [SerializeField] private side currentSide;
    [SerializeField] private bool fireInCooldown = false;

    //Atributos sem visualização:
    private Vector2 direction;
    private bool jaAtirou = false;


    // Sides:
    private enum side
    {
        left, right
    }

    // State Machine:
    private enum state
    {
        walk, fire
    }
    state currentState;
    string[] strStates = { "Sporanged_Walk", "Sporanged_Fire" }; //vetor de string states

    // Start is called before the first frame update
    void Start()
    {
        // getting components
        rb = gameObject.GetComponent<Rigidbody2D>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        player = thePlayer.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        // setting initial status
        currentState = state.walk;
        animator.Play(strStates[(int)state.walk]);
        fireInCooldown = false;
        jaAtirou = false;
        //Só para evitar que bugue:
        currentSide = side.left;
        targetDist = maxDist;

        // start infinite Coroutines:
        StartCoroutine(PickADist());
        StartCoroutine(PickASide());
        Debug.Log("Terminou o start");
    }

    // Update is called once per frame
    void Update()
    {
        DoTheFlip();
    }

    private void FixedUpdate()
    {
        moveEnemy();
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

    }
    void DoTheFlip()
    {
        if (rb.velocity.x > 0)
            sprite.flipX = false;
        else if (rb.velocity.x < 0)
            sprite.flipX = true;
    }

    void moveEnemy()
    {
        if(currentState == state.walk)
        {
            if(Vector2.Distance(rb.position, player.position) < 2 * maxDist && !fireInCooldown)
                ChangeState(state.fire);
            else
            {
                direction = (player.position - rb.position).normalized;
                //Força axial:
                if(((player.position - rb.position) - direction*targetDist).magnitude > 0.02)
                    rb.AddForce(direction * axialWalkForce);
                
                //força tangencial
                if(currentSide == side.left)
                    rb.AddForce(ortonormal(Vector2.left, direction) * tangWalkForce);
                else 
                    rb.AddForce(ortonormal(Vector2.right, direction) * tangWalkForce); 
            }
        }
        else if(currentState == state.fire)
        {
            //Se está na animação ainda, verifica em que parte está para instanciar o projetil
            if (IsAnimationPlaying(animator,"Sporanged_Fire"))
            {
                if( !jaAtirou && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= instatiateBulletTimeRate)
                {
                    direction = (player.position - rb.position).normalized;
                    //OBS: Talvez a rotação esteja errada.
                    Instantiate(bullet, rb.position + direction * bulletSporangedDist, Quaternion.identity);
                    jaAtirou = true;
                }
            }
            else
            {
                Debug.Log("Fire Animation terminou.");
                StartCoroutine(FireCooldown());
                ChangeState(state.walk);
                jaAtirou = false;
            }
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

    IEnumerator PickASide()
    {
        while(true)
        {
            currentSide = (side)Random.Range(0,2);
            yield return new WaitForSeconds(pickSideCooldown);
        }
    }
    IEnumerator PickADist()
    {
        while(true)
        {
            targetDist = Random.Range(minDist,maxDist);
            yield return new WaitForSeconds(pickSideCooldown);
        }
    }

    IEnumerator FireCooldown()
    {
        fireInCooldown = true;
        Debug.Log("Tiro entra em cooldown");
        yield return new WaitForSeconds(fireCooldown);
        Debug.Log("Tiro sai de cooldown");
        fireInCooldown = false;
    }

    //MAT-27
    //retorna o produto escalar de dois vetores
    float escalar(Vector2 v1, Vector2 v2)
    {
        return v1.x*v2.x + v1.y*v2.y;
    }
    //retorna a projeção ortogonal de um vetor v1 sobre o outro (v2):
    Vector2 proj(Vector2 v1, Vector2 v2)
    {
        return v2 * escalar(v1,v2)/(v2.magnitude*v2.magnitude);
    }
    // "Complemento ortonormal": subtrai um vetor v1 pela sua projeção sobre v2 e normaliza
    Vector2 ortonormal(Vector2 v1, Vector2 v2)
    {
        return (v1 - proj(v1,v2)).normalized;
    }


}
