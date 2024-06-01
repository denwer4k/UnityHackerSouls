using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    public bool isCheckingPlayer = false;
    public bool isAttacking = false;

    private bool isAttack1 = false;
    private bool isFindPlayer = false;

    [SerializeField] GameObject strikeTrigger;

    private Transform playerPosition;

    [SerializeField] private CharacterController controller;

    private Vector3 velocity;

    [SerializeField] Animator animations;

    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float animationAttackTime = 0.792f;

    private Vector3 spawnPosition;


    private void Start()
    {
        animations = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerPosition = GameObject.FindWithTag("Player").transform;

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


    public void Attacks()
    {
        Vector3 direction = playerPosition.position - transform.position;
        direction.y = 0; 
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerPosition.position.x, transform.position.y, playerPosition.position.z), moveSpeed * Time.deltaTime);
        
    }

    IEnumerator Attack1()
    {
        isAttack1 = true;
        strikeTrigger.SetActive(true);
        yield return new WaitForSeconds(animationAttackTime);
        strikeTrigger.SetActive(false);
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

        if (isCheckingPlayer == true)
        {
            Attacks();
            if (animations.GetBool("isStay") == true)
                animations.Play("KnightRig|RunCycle");
            animations.SetBool("isRunning", true);
            animations.SetBool("isStay", false);
            animations.SetBool("Attack1", false);
        }
        else if(isAttacking == true)
        {
            if (isAttack1 == false)
                StartCoroutine("Attack1");
            animations.SetBool("isRunning", false);
            animations.SetBool("isStay", false);
            animations.SetBool("Attack1", true);
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
