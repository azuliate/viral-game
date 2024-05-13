using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Controller")]
    public CharacterController controller;
    public float mass = 3f;

    private float speed;
    [Header("Movement")]
    public float crouchSpeed = 6.5f;
    public float walkSpeed = 12f;
    public float sprintSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float crouchYScale;
    private float startYScale;
    private float startYPosition;

    [Header("Gravity")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;


    Vector3 velocity;
    bool isGrounded;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    void Start()
    {
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {   
        //Check if we are touching the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask.value);

        //Reset velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        //Getting w,a,s,d inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        StateHandler();

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if(Input.GetKeyDown(crouchKey))
        {
            //Make character small
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            //Character doesn't flaot
            controller.Move(new Vector3(0, -1, 0));
        }
        if(Input.GetKeyUp(crouchKey))
        {
            //Make character tall
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            //Character doesn't get stuck in ground
            controller.Move(new Vector3(0, 1, 0));
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void StateHandler()
    {
        //Movement - Crouching
        if(Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            speed = crouchSpeed;
        }
        // Movement - Sprinting
        else if(isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }

        // Movement - Walking
        else if(isGrounded)
        {
            state = MovementState.walking;
            speed = walkSpeed;
        }

        //Move - Air
        else
        {
            state = MovementState.air;
        }
    }
}
