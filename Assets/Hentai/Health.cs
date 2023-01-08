using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;
    [SerializeField] float maxHealth = 100;
    public float regen;

    public float health { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        AddHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        AddHealth(regen * Time.deltaTime);
    }

    public void AddHealth(float value)
    {
        health += value;
        health = Mathf.Min(health, maxHealth);
        healthBar.value = health / maxHealth;
        if (health <= 0)
        {
            // Die
            Destroy(gameObject);
            if (gameObject.CompareTag("Player"))
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
