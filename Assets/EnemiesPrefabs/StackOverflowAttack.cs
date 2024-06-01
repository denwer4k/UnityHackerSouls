using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackOverflowAttack : MonoBehaviour
{
    [SerializeField] private bool isCheckingPlayer = false;
    [SerializeField] private bool isAttacking = false;

    private bool isAttack1 = false;

    private Transform playerPosition;

    [SerializeField] private CharacterController controller;

    private Vector3 velocity;

    [SerializeField] Animator animations;

    [SerializeField] private float moveSpeed = 2f;
    private float gravity = 9.81f;
    [SerializeField] private float animationAttackTime = 0.792f;
    [SerializeField] private float timeOfDestroy;

    private Vector3 SpawnPosition;

    //IEnumerator FireOn()
    //{
    //    Vector3 BulPos = spawnPos.transform.position;
    //    Quaternion rotterdam = spawnPos.transform.rotation;
    //    GameObject spawnBullet = Instantiate(projectile, BulPos, rotterdam);
    //    yield return new WaitForSeconds(timeOfDestroy);
    //    Destroy(spawnBullet);
    //}

    private void Start()
    {
        animations = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerPosition = GameObject.FindWithTag("Player").transform;

        SpawnPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isCheckingPlayer = true;
        if (other.CompareTag("Aim"))
        {
            isAttacking = true;
            isCheckingPlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isCheckingPlayer = false;
        if (other.CompareTag("Aim"))
        {
            isAttacking = false;
            isCheckingPlayer = true;
        }
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
            animations.SetBool("Running", true);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", false);
            Vector3 direction = SpawnPosition - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            controller.Move(transform.forward * Time.deltaTime * moveSpeed);
        }
    }

    IEnumerator Attack1()
    {
        isAttack1 = true;
        yield return new WaitForSeconds(animationAttackTime);
        isAttack1 = false;
    }

    private void Update()
    {
        if (controller.isGrounded == true && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (isCheckingPlayer == true && (((int)transform.position.x + 3612) / 24 == ((int)playerPosition.position.x + 3612) / 24
            && ((int)transform.position.z + 3612) / 24 == ((int)playerPosition.position.z + 3612) / 24)) //Here is chack for attacking
        {
            MoveToPlayer();
            if (animations.GetBool("Stay") == true)
                animations.Play("DdosRig|Run");
            animations.SetBool("Running", true);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", false);
        }
        else if (isAttacking == true)
        {
            if (isAttack1 == false)
                StartCoroutine("Attack1");
            animations.SetBool("Running", false);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", true);
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
