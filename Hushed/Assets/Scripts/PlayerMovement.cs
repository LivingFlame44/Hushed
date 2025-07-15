using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE,
        WALKING,
        WALKHOLDING,
        HIDING,
        GRABBED,
        PREPARING,
        STATIONARY,
        SITTING,
        RESET
    }

    public PlayerState playerState;

    public Rigidbody rb;
    public float speed;
    //public float jumpingPower;
    public float horizontal;
    public bool isFacingRight;

    private Animator animator;
    private bool isWalking;

    public bool hasCoffee = false;

    public List<string> playerItems;

    public Vector3 movePos;
    //public Transform groundCheck;
    //public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                ResetAnimator();
                speed = 6f;
                break;
            case PlayerState.WALKING:
                ResetAnimator();
                speed = 6f;
                break;
            case PlayerState.WALKHOLDING:
                speed = 6f;
                //ResetAnimator();
                break;
            case PlayerState.HIDING:
                speed = 2;
                break;
            case PlayerState.GRABBED:
                speed = 0;
                break;
            case PlayerState.STATIONARY:
                speed = 0;
                break;
            case PlayerState.PREPARING:
                speed = 0;
                animator.SetBool("isInteracting", true);
                break;
            case PlayerState.SITTING:
                speed = 0f;
                animator.SetBool("isSitting", true);
                break;
            default:
                speed = 6f;
                break;
        }

        if (speed == 0)
        {
            
        }
        else
        {
            
            Movement();
            //HandleAnimation();
            Flip();
            //ResetAnimation();
        }
        

        
    }

    public void MoveMika()
    {
        this.transform.parent.gameObject.transform.localPosition = movePos;
    }
    public void ResetAnimation()
    {
        playerState = PlayerState.IDLE;
        animator.SetBool("isInteracting", false);
        animator.SetBool("isSitting", false);
    }

    public void ResetAnimator()
    {
        animator.SetBool("isInteracting", false);
        animator.SetBool("isSitting", false);
    }

    public void MikaHoldItem()
    {
        playerState = PlayerState.WALKHOLDING;
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //if (Input.GetButtonDown("Jump") && IsGrounded())
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        //}
        isWalking = horizontal != 0 ? true : false;
        animator.SetFloat("Speed", horizontal != 0 ? 1 : 0);

        switch (playerState)
        {
            case PlayerState.WALKHOLDING:
                ResetAnimator();
                animator.SetFloat("Horizontal", horizontal != 0 ? 2 : 0);
                break;
            default:
                ResetAnimator();
                animator.SetFloat("Horizontal", horizontal != 0 ? 1 : 0);
                break;
        }
                
    }

    public void BackToIdle()
    {
        playerState = PlayerState.IDLE;
    }

    public void GetItem(string item)
    {
        playerItems.Add(item);
    }

    public void RemoveItem(string item)
    {
        playerItems.Remove(item);
    }

    void HandleAnimation()
    {
        animator.SetFloat("Speed", 1);
    }


    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
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
