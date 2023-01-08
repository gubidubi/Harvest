using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawAI : MonoBehaviour
{
    // Components
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer sprite;

    // Enemy parameters (Craw)

    // State Machine:
    private enum state
    {
        fly, dive, flee
    }
   state currentState;
    string[] strStates = {"Enemy_Fly","Enemy_Dive","Enemy_Flee"}; //vetor de string states
    
    // Start is called before the first frame update
    void Start()
    {
        // getting components
		rb = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        // setting initial status
		currentState = state.fly;
		animator.Play(strStates[(int)state.fly]);
    }

    // Update is called once per frame
    void Update()
    {
		DoTheFlip();
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

    }

    void MoveEnemy()
    {
        
    }
}
