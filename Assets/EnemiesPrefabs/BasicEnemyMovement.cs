using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    private bool isAttacking = false;
    private bool isAttack = false;
    private bool isSeePlayer = false;

    [SerializeField] GameObject strikeTrigger;

    private Transform playerPosition;

    [SerializeField] private CharacterController controller;

    private Vector3 velocity;

    [SerializeField] Animator animations;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float animationAttackTime = 0.792f;


    private void Start()
    {
        animations = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aim"))
            isSeePlayer = true;
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
            isSeePlayer = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Aim"))
            isSeePlayer = false;
        if (other.CompareTag("Player"))
        {
            isSeePlayer = true;
            isAttacking = false;
        }
    }

    public void MoveToPlayer()
    {
        Vector3 direction = playerPosition.position - transform.position;
        direction.y = 0; 
        transform.rotation = Quaternion.LookRotation(direction);
        controller.Move(transform.forward * Time.deltaTime * moveSpeed);

    }

    IEnumerator Attack()
    {
        isAttack = true;
        animations.Play("KnightRig|Attack");
        yield return new WaitForSeconds(0.7f);
        strikeTrigger.SetActive(true);
        yield return new WaitForSeconds(animationAttackTime - 0.7f);
        strikeTrigger.SetActive(false);
        isAttack = false;
    }

    private void Update()
    {
        if (controller.isGrounded == true && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (isSeePlayer == true)
        {
            MoveToPlayer();
            if (animations.GetBool("isStay") == true)
                animations.Play("KnightRig|RunCycle");
            animations.SetBool("isRunning", true);
            animations.SetBool("isStay", false);
            animations.SetBool("Attack1", false);
        }
        else if (isAttacking == true)
        {
            if (isAttack == false)
                StartCoroutine("Attack");
            animations.SetBool("isRunning", false);
            animations.SetBool("isStay", false);
            Vector3 direction = playerPosition.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        else
        {
            animations.SetBool("isRunning", false);
            animations.SetBool("isStay", true);
            animations.SetBool("Attack1", false);
        }

    }
}
