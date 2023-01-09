using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTransformation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab;
    public Health health;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health.health == health.maxHealth)
        {
            seedTransformation();
        }
    }

    public void seedTransformation()
    {
        GameObject plant = Instantiate(prefab, transform.position, Quaternion.identity);
        float seedWater = gameObject.GetComponent<Water>().water;
        plant.GetComponent<Water>().SetWater(seedWater);
        var seedCell = GameManager.instance.grid.WorldToCell(gameObject.transform.position);
        GameManager.instance.plantSpots[seedCell] = plant;
        Destroy(gameObject);
    }
}
