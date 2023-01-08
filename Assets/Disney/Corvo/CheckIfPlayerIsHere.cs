using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerIsHere : MonoBehaviour
{
    public bool playerIsHere = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerIsHere = true;
    }

    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        playerIsHere = false;
    }


}
