using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DdosEnemyScript : MonoBehaviour
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


    // All the movement is here, dont look at the function name
    public void Attacks()
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
        yield return new WaitForSeconds(0.6f); //это изменилось
        strikeTrigger.SetActive(true);
        yield return new WaitForSeconds(animationAttackTime - 0.6f);   //это изменилось
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

        if (isCheckingPlayer == true && (((int)transform.position.x + 3612) / 24 == ((int)playerPosition.position.x + 3612) / 24
            && ((int)transform.position.z  + 3612) / 24 == ((int)playerPosition.position.z  + 3612) / 24)) //Here is chack for attacking
        {
            Attacks();
            if (animations.GetBool("Stay") == true)
                animations.Play("DdosRig|Run");
            animations.SetBool("Running", true);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", false);
        }
        else if(isAttacking == true)
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
