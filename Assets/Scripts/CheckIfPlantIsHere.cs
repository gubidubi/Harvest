using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlantIsHere : MonoBehaviour
{
    [SerializeField] private bool plantIsHere = false;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        plantIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Planta")
        {
            if(plantIsHere == false)

            plantIsHere = true;
        }
    }

    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
       if(other.gameObject.tag == "Planta")
        {
            plantIsHere = false;
            
        }
    }

}
