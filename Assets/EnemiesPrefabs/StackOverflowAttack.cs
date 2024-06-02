using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOverflowAttack : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    [SerializeField] private bool isAttacking = false;

    private bool isAttack = false;

    private Transform playerPosition;
    [SerializeField] private Transform bulletSpawnPosition;

    [SerializeField] private CharacterController controller;

    private Vector3 velocity;

    [SerializeField] Animator animations;

    [SerializeField] private float moveSpeed = 2f;
    private float gravity = 9.81f;
    [SerializeField] private float animationAttackTime = 0.792f;
    [SerializeField] private float timeOfDestroy;

    private Vector3 SpawnPosition;

    

    private void Start()
    {
        animations = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerPosition = GameObject.FindWithTag("Player").transform;

        SpawnPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aim"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Aim"))
        {
            isAttacking = false;
        }
    }

    IEnumerator FireOn()
    {
        isAttack = true;
        animations.Play("Armature|Throw");
        Vector3 BulPos = bulletSpawnPosition.transform.position;
        Quaternion rotterdam = bulletSpawnPosition.transform.rotation;
        GameObject spawnBullet = Instantiate(projectile, BulPos, rotterdam);
        yield return new WaitForSeconds(animationAttackTime);
        isAttack = false;
        yield return new WaitForSeconds(timeOfDestroy);
        Destroy(spawnBullet);
    }

    public void MoveToPlayer()
    {
        Vector3 direction = playerPosition.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        controller.Move(transform.forward * Time.deltaTime * moveSpeed);


    }
    public void NotAttacking()
    {
        if (Mathf.Abs(transform.position.x - SpawnPosition.x) > 0.1 && Mathf.Abs(transform.position.z - SpawnPosition.x) > 0.1)
        {
            if (animations.GetBool("Stay") == true)
                animations.Play("DdosRig|Run");
            animations.SetBool("Stay", false);
            Vector3 direction = SpawnPosition - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            controller.Move(transform.forward * Time.deltaTime * moveSpeed);
        }
    }


    private void Update()
    {
        if (controller.isGrounded == true && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (isAttacking == false && (((int)transform.position.x + 3612) / 24 == ((int)playerPosition.position.x + 3612) / 24
            && ((int)transform.position.z + 3612) / 24 == ((int)playerPosition.position.z + 3612) / 24)) //Here is chack for attacking
        {
            MoveToPlayer();
            if (animations.GetBool("Stay") == true)
                animations.Play("DdosRig|Run");
            animations.SetBool("Running", true);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", false);
            if (isAttack == false)
                StartCoroutine(FireOn());
        }
        else if (isAttacking == true)
        {
            if (isAttack == false)
                StartCoroutine(FireOn());
            animations.SetBool("Running", false);
            animations.SetBool("Stay", true);
            Vector3 direction = playerPosition.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            NotAttacking();
            animations.SetBool("Running", false);
            animations.SetBool("Stay", true);
            animations.SetBool("Attack", false);
        }

    }
}
