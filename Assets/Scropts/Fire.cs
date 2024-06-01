using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator animka;
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

}
