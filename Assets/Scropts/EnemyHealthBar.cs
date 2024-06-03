using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionLol;

    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private string damagerTag = "PlayerBullet";
    [SerializeField] private float  getdamage = 20;
    private AudioSource sounds;
    [SerializeField] private AudioClip DamSound;


    [SerializeField] private bool isDdos;
    public GameObject HealthKit;

    private void Start()
    {
        sounds = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(damagerTag))
        {
            sounds.PlayOneShot(DamSound);
            currentHP -= getdamage;
        }
            
        if (currentHP <= 0)
        {
            if (isDdos)
            {
                Instantiate(HealthKit, transform.position, transform.rotation);
                
            }
            Instantiate(ExplosionLol, transform.position, transform.rotation);
            Destroy(gameObject);
        }
            
    }

}
