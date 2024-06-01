using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //данный скрипт отличается тем и только тем, что не использует анимации и спавнит пульки в позиции объекта, к которому прикреплен

    [SerializeField] private GameObject projectile;

    private bool isBulletMove = true;
    [SerializeField] private float timeOfRecharge;
    [SerializeField] private float timeOfDestroy;

    private AudioSource Sounds;
    [SerializeField] private AudioClip FireClip;
    IEnumerator FireOn()
    {
        isBulletMove = false;
        Vector3 BulPos = transform.position;
        Quaternion rotterdam = transform.rotation;
        GameObject spawnBullet = Instantiate(projectile, BulPos, rotterdam);
        Sounds.PlayOneShot(FireClip);
        yield return new WaitForSeconds(timeOfRecharge);
        isBulletMove = true;
        yield return new WaitForSeconds(timeOfDestroy);
        Destroy(spawnBullet);
    }

    private void Start()
    {
        Sounds = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && isBulletMove)
            StartCoroutine(FireOn());
    }
}
