using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeBehavior : MonoBehaviour
{
    //Componentes
    private GameObject thePlayer;
    private Rigidbody2D player;
    private Rigidbody2D rb;
    SpriteRenderer sprite;

    [Header("Atributos")]
    public float sporeSpeed;
    public float sporeRange;
    public float targetError;
    public float angularVelocity;

    //atributos privados
    private Vector2 target; 
    private float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        // getting components
        rb = gameObject.GetComponent<Rigidbody2D>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        player = thePlayer.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        FindTarget();
        //setting constants
        destroyTime = sporeRange/sporeSpeed;
        rb.angularVelocity = angularVelocity;
        rb.velocity = (target - rb.position).normalized * sporeSpeed;

        //Start autodestruction
        StartCoroutine(Autodestruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FindTarget()
    {
        target = player.position + new Vector2(Random.Range(-targetError, targetError),Random.Range(-targetError, targetError));
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            {
                //Dar Dano.
                //Animação
                Destroy(gameObject);
            }
    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(destroyTime);
        //Animação?
        Destroy(gameObject);
    }
}
