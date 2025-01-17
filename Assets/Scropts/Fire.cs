﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    /*private Animator animka;
    [SerializeField] private string animationName;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnPos;
    private bool isBulletMove = true;
    [SerializeField] private float timeOfRecharge;
    [SerializeField] private float timeOfDestroy;

    [SerializeField] private AudioSource Sounds;
    [SerializeField] private AudioClip FireClip;
    IEnumerator FireOn()
    {
        isBulletMove = false;
        Vector3 BulPos = spawnPos.transform.position;
        Quaternion rotterdam = spawnPos.transform.rotation;
        GameObject spawnBullet = Instantiate(projectile, BulPos, rotterdam);
        animka.Play(animationName);
        Sounds.PlayOneShot(FireClip);
        yield return new WaitForSeconds(timeOfRecharge);
        isBulletMove = true;
        yield return new WaitForSeconds(timeOfDestroy);
        Destroy(spawnBullet);
    }

    private void Start()
    {
        animka = GetComponent<Animator>();
        Sounds = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && isBulletMove)
            StartCoroutine(FireOn());
    }
    */
    private Animator animka;

    [SerializeField] private string animationName;
    [SerializeField] private string animationNameSword;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject spawnPos;
    [SerializeField] private GameObject hitTrigger;

    private bool isBulletMove = true;
    private bool isSwordHit = true;

    [SerializeField] private float timeOfRecharge;
    [SerializeField] private float timeOfDestroy;
    [SerializeField] private float timeOfSwordHitRecharge;

    private AudioSource sounds;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip strike;

    //For stack overflow check
    public bool ShootAbilityUnlocked = false;
    IEnumerator FireOn()
    {
        isBulletMove = false;
        Vector3 BulPos = spawnPos.transform.position;
        Quaternion rotterdam = spawnPos.transform.rotation;
        GameObject spawnBullet = Instantiate(projectile, BulPos, rotterdam);
        animka.Play(animationName);
        sounds.PlayOneShot(fireClip);
        isSwordHit = false;
        yield return new WaitForSeconds(0.4f);
        isSwordHit = true;
        yield return new WaitForSeconds(timeOfRecharge - 0.4f);
        isBulletMove = true;
        yield return new WaitForSeconds(timeOfDestroy);
        Destroy(spawnBullet);
    }

    IEnumerator SwordStrike()
    {
        isSwordHit = false;
        animka.Play(animationNameSword);
        sounds.PlayOneShot(strike);
        hitTrigger.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitTrigger.SetActive(false);
        yield return new WaitForSeconds(timeOfSwordHitRecharge);
        isSwordHit = true;
    }

    private void Start()
    {
        animka = GetComponent<Animator>();
        sounds = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && isBulletMove && ShootAbilityUnlocked)
            StartCoroutine(FireOn());
        if (Input.GetMouseButtonDown(0) && isSwordHit)
            StartCoroutine(SwordStrike());
    }
}
