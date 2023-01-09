using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject selection;
    public GameObject[] seeds;
    public GameObject test;
    public float rangePlantSeed = 10.0f;
    public float changeScroll;
    public int selectedIndex;

    private float totalScroll;

    // Start is called before the first frame update
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateSelectedPosition();
        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int seedCell = GameManager.instance.grid.WorldToCell(mousePos);
            if (CanPlant(seedCell))
            {
                Plant(seedCell);
            }
            else
            {
                Debug.Log("failed to plant!");
            }
        }
    }

    void UpdateSelectedPosition()
    {
        totalScroll += Input.mouseScrollDelta.y;
        int timesScrolled = Mathf.RoundToInt(totalScroll / changeScroll);
        if (timesScrolled != 0)
        {
            // change selectedIndex
            totalScroll = Mathf.Repeat(totalScroll, changeScroll); // "resto" do totalScroll
            selectedIndex = Mod(selectedIndex + timesScrolled, seeds.Length);

            //change selection position
            selection.transform.position = seeds[selectedIndex].transform.position;
        }
    }

    int Mod(int a, int b)
    {
        a = a % b;
        if (a < 0)
        {
            a += b;
        }
        return a;
    }

    bool CanPlant(Vector3Int seedCell)
    {
        if (seeds[selectedIndex].GetComponent<ItemStack>().quantity != 0 && !GameManager.instance.gridPositions.ContainsKey(seedCell))
        {
            Vector3 seedPosition = GameManager.instance.grid.GetCellCenterWorld(seedCell);
            float distance = Vector3.Distance(seedPosition, player.transform.position);
            if (distance < rangePlantSeed)
            {
                return true;
            }
        }
        return false;
    }

    void Plant(Vector3Int seedCell)
    {
        ItemStack seedStack = seeds[selectedIndex].GetComponent<ItemStack>();
        Vector3 seedPosition = GameManager.instance.grid.GetCellCenterWorld(seedCell);
        GameObject placedSeed = Instantiate(seedStack.prefab, seedPosition, Quaternion.identity);
        GameManager.instance.gridPositions.Add(seedCell, placedSeed);
        seedStack.ChangeItemQuantity(-1);
    }
}
