using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossPhase1Script : MonoBehaviour
{
    public bool isCheckingPlayer = false;
    public bool isAttacking = false;

    private bool isAttack1 = false;

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

    // All the movement is here, dont look at the function name
    public void Attacks()
    {
        Vector3 direction = playerPosition.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        controller.Move(transform.forward * Time.deltaTime * moveSpeed);


    }
    /*public void NotAttacking() BECAUSE BOSS
    {
        if (Mathf.Abs(transform.position.x - SpawnPosition.x) > 0.1 && Mathf.Abs(transform.position.z - SpawnPosition.x) > 0.1)
        {
            if (animations.GetBool("Stay") == true)
                animations.Play("KnightRig|Walking");
            animations.SetBool("Running", true);
            animations.SetBool("Stay", false);
            animations.SetBool("Attack", false);
            Vector3 direction = SpawnPosition - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            controller.Move(transform.forward * Time.deltaTime * moveSpeed);
        }
    }*/

    IEnumerator Attack1()
    {
        isAttack1 = true;
        //это изменилось
        yield return new WaitForSeconds(0.5f);
        strikeTrigger.SetActive(true);
        yield return new WaitForSeconds(0.25f);   //это изменилось
        strikeTrigger.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isAttack1 = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) < 2.2f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (controller.isGrounded == true && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerPosition.position) > 2.2f) //Here is chack for attacking
        {
            Attacks();
            if (animations.GetBool("Stay") == true)
                animations.Play("KnightRig|Walking");
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
            //NotAttacking(); BECAUSE BOSS
            animations.SetBool("Running", false);
            animations.SetBool("Stay", true);
            animations.SetBool("Attack", false);
        }

    }
}
