using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
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
        yield return new WaitForSeconds(timeOfSwordHitRecharge - 0.2f);
        isSwordHit = true;
    }

    private void Start()
    {
        animka = GetComponent<Animator>();
        sounds = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1) && isBulletMove)
            StartCoroutine(FireOn());
        if (Input.GetMouseButtonDown(0) && isSwordHit)
            StartCoroutine(SwordStrike());
    }

}
