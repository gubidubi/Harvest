using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTransformation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefeb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void seedTransformation()
    {
        Destroy(gameObject);
        Instantiate(prefeb);
    }

}
