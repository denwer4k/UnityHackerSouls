﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    [SerializeField] private Slider healthBar;
    public float currentHealth = 180;
    public float maxHealth = 180;
    private AudioSource HealthAudSource;
    [SerializeField] private AudioClip Die;
    [SerializeField] private AudioClip Hurt;
    private bool hasDied = false;

    public bool isInBossfight = false;

    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        HealthAudSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyWeapon"))
        {
            currentHealth -= 15;
            if(!hasDied) HealthAudSource.PlayOneShot(Hurt);
        }
    }

    IEnumerator DyingLOL()
    {
        hasDied = true;
        HealthAudSource.Stop();
        HealthAudSource.PlayOneShot(Die);
        yield return new WaitForSeconds(1.6f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator DyingLOLInBossfight()
    {
        hasDied = true;
        HealthAudSource.Stop();
        HealthAudSource.PlayOneShot(Die);
        yield return new WaitForSeconds(1.6f);

        SceneManager.LoadScene("DungeonLevel");
    }
    void Update()
    {
        healthBar.value = currentHealth;
        if (currentHealth <= 0 && hasDied == false && isInBossfight == false)
        {
            StartCoroutine(DyingLOL());
        } else if(currentHealth <= 0 && hasDied == false && isInBossfight == true)
        {
            StartCoroutine(DyingLOLInBossfight());
        }
    }
}
