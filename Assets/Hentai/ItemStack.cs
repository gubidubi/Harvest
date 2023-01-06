using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemStack : MonoBehaviour
{
    public int quantity;
    public bool destroyOnEmptyStack;

    // Start is called before the first frame update
    private TextMeshProUGUI quantityText;
    void Start()
    {
        quantityText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        UpdateQuantityText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateQuantityText()
    {
        quantityText.text = "x" + quantity;
    }

    public void ChangeItemQuantity(int value)
    {
        quantity += value;
        if (quantity <= 0 && destroyOnEmptyStack)
        {
            Destroy(gameObject);
        }
        UpdateQuantityText();
    }
}
