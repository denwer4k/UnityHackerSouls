using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            currentHealth -= 15;
        }
    }

    IEnumerator DyingLOL()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("YOU FAILED");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            StartCoroutine(DyingLOL());
        }
    }
}
