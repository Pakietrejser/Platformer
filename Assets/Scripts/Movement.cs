using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class Movement : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float accelerationGrounded = .4f;
    [Range(0,1)]
    [SerializeField] float accelerationAirbone = .2f;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float dashSpeed = 16f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    bool canMove = true;
    bool hasDashed;
    bool isDashing;
    bool wallJumped;
    bool groundTouch;
    
    float currentSpeed, targetSpeed;

    Collision col;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        targetSpeed = inputX * moveSpeed;

        if (Input.GetButtonDown("Jump") && col.OnGround)
            Jump();
        
        if (Input.GetButtonDown("Dash") && !hasDashed)
            Dash(new Vector2(inputX, inputY));

        if (inputX > 0 && currentSpeed < 0 || inputX < 0 && currentSpeed > 0)
            ResetSpeed();

        if (col.OnGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (col.OnGround && groundTouch)
        {
            groundTouch = false;
        }
        
        RotatePlayer(inputX);
        
        if (!isDashing)
            BetterJumping();
    }

    void FixedUpdate()
    {
        Move(targetSpeed);
    }

    void Move(float force)
    {
        if (!canMove)
            return;
        
        currentSpeed = BMath.Smoothstep1D(currentSpeed, force, col.OnGround ? accelerationGrounded : accelerationAirbone);
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Dash(Vector2 direction)
    {
        hasDashed = true;
        
        rb.velocity = direction.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        StartCoroutine(GroundDash());
        rb.gravityScale = 0;
        wallJumped = true;
        isDashing = true;
        canMove = false;
        
        yield return new WaitForSeconds(.3f);

        rb.gravityScale = 1;
        canMove = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (col.OnGround)
            hasDashed = false;
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
    }

    void ResetSpeed()
    {
        currentSpeed = 0;
    }

    void RotatePlayer(float direction)
    {
        if (direction < 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        if (direction > 0)
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
    }

    void BetterJumping()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}