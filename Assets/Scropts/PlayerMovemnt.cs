using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovemnt : MonoBehaviour
{
    public bool IsAllowedToDoubleJump = false;
    public bool IsAllowedToSlide = false;
    public bool IsAllowedToDash = false;

    private Transform transform;

    [SerializeField] private CharacterController controller;

    public float walkSpeed = 10f;
    public float runSpeed = 14f;
    public float gravity = 9.81f;
    public float jumpHeight = 5f;
    public float dashForce = 10000f;
    public float timeOfDashRecharge = 4f;
    [SerializeField] private float dashTime = 1.2f;
    [SerializeField] private float slideSpeed = 20f;

    [SerializeField] private float heighOnSlide = 0.5f;
    [SerializeField] private float standartHeight = 2f;

    public float speed;

    private Vector3 velocity;
    private Vector3 dash;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask groundMask2;
    private bool isGrounded = false;

    private bool isReadyToDash = true;
    private bool DoubleJump = true;
    private bool isDashed = false;
    public bool isSlided = false;

    private float x;
    private float z;

    // Compass image in UI
    public Image Compass;

    IEnumerator goDash()
    {
        isDashed = true;
        yield return new WaitForSeconds(dashTime);
        isDashed = false;
        StartCoroutine("dashRecharge");
    }

    IEnumerator dashRecharge()
    {
        yield return new WaitForSeconds(timeOfDashRecharge);
        isReadyToDash = true;
    }

    void Slide()
    {
        isSlided = true;
        controller.height = heighOnSlide;
        speed = slideSpeed;
    }

    private void Start()
    {
        speed = walkSpeed;
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded && IsAllowedToSlide)
        {
            Slide();
        }
        else
        {
            isSlided = false;
            controller.height = standartHeight;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded == false) isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask2);


        if (isGrounded)
        {
            DoubleJump = true;
        }

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        if (isSlided == false)
        {
            z = Input.GetAxis("Vertical");
        }
        else
        {
            z = 1;
        }

        if (isSlided == false)
        {
            x = Input.GetAxis("Horizontal");
        }
        else
        {
            x = 0;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        


        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded  && DoubleJump && IsAllowedToDoubleJump)
        {
            DoubleJump = false;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * -gravity);
        }

        velocity.y -= gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        //Compass code
        Compass.transform.localEulerAngles = new Vector3(0, 0, transform.eulerAngles.y);

        //Dash moved from FixedUpdat
        float d = Input.GetAxis("Dash");

        if (isReadyToDash == true && d != 0 && IsAllowedToDash == true)
        {
            isReadyToDash = false;
            if (d > 0)
            {
                dash = transform.right;
            }
            else
            {
                dash = -transform.right;
            }
            StartCoroutine("goDash");
        }

        if (isDashed)
        {
            controller.Move(dash * dashForce * Time.deltaTime * 50f);
        }
    }

    private void FixedUpdate()
    {
        /*float d = Input.GetAxis("Dash");

        if (isReadyToDash == true && d != 0 && IsAllowedToDash == true)
        {
            isReadyToDash = false;
            if (d > 0)
            {
                dash = transform.right;
            }
            else
            {
                dash = -transform.right;
            }
            StartCoroutine("goDash");
        }

        if (isDashed)
        {
            controller.Move(dash * dashForce);
        }

        */
    }
}
