using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Water : MonoBehaviour
{
    public Slider waterBar;
    [SerializeField] float maxWater = 100;
    public float drainRate = 1.0f;

    public float water { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        AddWater(maxWater);
    }

    // Update is called once per frame
    void Update()
    {
        AddWater(-drainRate * Time.deltaTime);
    }

    public void AddWater(float value)
    {
        water += value;
        water = Mathf.Min(water, maxWater);
        waterBar.value = water / maxWater;
        if (water <= 0)
        {
            // Die
            Destroy(gameObject);
        }
    }

    // Empty the gridCell
    private void OnDestroy() {
        if (GameManager.instance == null || GameManager.instance.grid == null)
        {
            return;
        }
        Vector3Int plantCell = GameManager.instance.grid.WorldToCell(transform.position);
        if (GameManager.instance.gridPositions.ContainsKey(plantCell))
        {
            GameManager.instance.gridPositions.Remove(plantCell);
        }
        else
        {
            Debug.Log("Plant was off grid before being deleted!");
        }
    }
}
