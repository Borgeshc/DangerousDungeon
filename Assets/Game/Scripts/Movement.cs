using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour
{
    Animator anim;
    public float speed;
    private Quaternion lookLeft;
    private Quaternion lookRight;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private Rigidbody rb;

    //Jump variables
    public float jumpHeight = 0.5f;
    public float gravity = 20.0f;

    public bool canJump = true;
    public bool canControlDescent = true;

    private float jumpRepeatTime = 0.05f;
    private float jumpTimeout = 0.15f;
    private float groundedTimeout = 0.25f;

    private float verticalSpeed = 0.0f;
    private float moveSpeed = 0.0f;

    private CollisionFlags collisionFlags;

    private bool jumping = false;
    private bool jumpingReachedApex = false;

    private float lastJumpButtonTime = -10.0f;
    private float lastJumpTime = -1.0f;
    private Vector3 wallJumpContactNormal;

    private float lastJumpStartHeight = 0.0f;
    private float touchWallJumpTime = -1.0f;

    private Vector3 inAirVelocity = Vector3.zero;

    private float lastGroundedTime = 0.0f;
    private bool isControllable = true;
    private Vector3 movement;
    private bool ableToJump;
    public bool isMobile;
    public static bool canMove;
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        lookRight = transform.rotation;
        lookLeft = lookRight * Quaternion.Euler(0, 180, 0);
        canMove = true;
    }
    void Update()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.z = Mathf.Clamp(transform.position.z, -2.25f, -2.25f);
        transform.position = clampedPosition;

        controller.Move(moveDirection * Time.deltaTime);
        anim.SetBool("IsRunning", false);

        if(canMove)
        {
            moveDirection = isMobile ? new Vector3((CrossPlatformInputManager.GetAxis("Horizontal")), 0, 0) : new Vector3((Input.GetAxis("Horizontal")), 0, 0);
            //  moveDirection = new Vector3((CrossPlatformInputManager.GetAxis("Horizontal")), 0, 0);

            if ((CrossPlatformInputManager.GetAxis("Horizontal")) < 0 || Input.GetKey(KeyCode.A) || (Input.GetAxis("Horizontal") < 0))
            {
                transform.rotation = lookLeft;
                moveDirection = transform.TransformDirection(-moveDirection);
                moveDirection *= speed;

                anim.SetBool("IsRunning", true);
            }

            if ((CrossPlatformInputManager.GetAxis("Horizontal")) > 0 || Input.GetKey(KeyCode.D) || (Input.GetAxis("Horizontal") < 0))
            {
                transform.rotation = lookRight;
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                anim.SetBool("IsRunning", true);
            }

            /// Jump Code
            if (Input.GetButtonDown("Jump") || MyMobileInput.jump && ableToJump)
            {
                ableToJump = false;
                lastJumpButtonTime = Time.time;
            }
            ApplyGravity();

            ApplyJumping();

            movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0) + inAirVelocity;
            movement *= Time.deltaTime;

            wallJumpContactNormal = Vector3.zero;
            collisionFlags = controller.Move(movement);

            if (IsGrounded())
            {
                anim.SetBool("IsRunJumping", false);
                ableToJump = true;
                lastGroundedTime = Time.time;
                inAirVelocity = Vector3.zero;
                if (jumping)
                {
                    jumping = false;
                }
            }

            if (verticalSpeed > 0f)
            {
                anim.SetBool("IsRunJumping", true);
            }

        }
    }

    bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    void ApplyJumping()
    {
        if (lastJumpTime + jumpRepeatTime > Time.time)
            return;

        if (IsGrounded())
        {
            if (canJump && Time.time < lastJumpButtonTime + jumpTimeout)
            {
                verticalSpeed = CalculateJumpVerticalSpeed(jumpHeight);
            }
        }
    }

    void ApplyGravity()
    {
        if (isControllable)
        {
            bool jumpButton = Input.GetButton("Jump");

            bool controlledDescent = canControlDescent && verticalSpeed <= 0.0 && jumpButton && jumping;

            if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
            {
                jumpingReachedApex = true;
            }
            if (IsGrounded())
                verticalSpeed = 0.0f;
            else
                verticalSpeed -= gravity * Time.deltaTime;
        }
    }

    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2 * targetJumpHeight * gravity);
    }
}
