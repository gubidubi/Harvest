using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemStack : MonoBehaviour
{
    public TextMeshProUGUI quantityText;
    public int quantity;
    public bool destroyOnEmptyStack;

    // Start is called before the first frame update
    void Start()
    {
        UpdateQuantityText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateQuantityText()
    {
        if (quantityText != null)
        {
            quantityText.text = "x" + quantity;
        }
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
