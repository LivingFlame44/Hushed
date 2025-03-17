using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,
        WALKING,
        HIDING,
        GRABBED,
        STATIONARY
    }

    public PlayerState playerState;

    public Rigidbody rb;
    public float speed;
    //public float jumpingPower;
    public float horizontal;
    public bool isFacingRight;

    //private Animator animator;
    private bool isWalking;

    //public Transform groundCheck;
    //public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != PlayerState.GRABBED)
        {
            Movement();
            //HandleAnimation();
            Flip();
        }
        

        switch(playerState)
        {
            case PlayerState.IDLE:
                speed = 6f;
                break;
            case PlayerState.WALKING:
                break;
            case PlayerState.HIDING:
                speed = 2;
                break;
            case PlayerState.GRABBED:
                speed = 0;
                break;
            case PlayerState.STATIONARY:
                break;
            default:
                speed = 6f;
                break;
        }
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        //}
        isWalking = horizontal != 0 ? true : false;

    }

    //void HandleAnimation()
    //{
    //    animator.SetBool("isWalking", isWalking);
    //}


    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //private bool IsGrounded()
    //{
    //    return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    //}

    private void LateUpdate()
    {

    }
}
