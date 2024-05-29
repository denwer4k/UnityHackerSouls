using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public float currentHealth = 100;
    public float maxHealth = 100;

    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            Debug.Log("DGMLRGNL");
            currentHealth -= 5;
        }
    }
    void Update()
    {
        healthBar.value = currentHealth;
    }
}
