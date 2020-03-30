using UnityEngine;
using Utilities;

[RequireComponent(typeof(Rigidbody2D), typeof(Collision))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float dashSpeed = 10f;
    
    [Space]
    
    [Header("Acceleration")]
    [Range(1,50)]
    [SerializeField] float accelerationGrounded = 20f;
    [Range(1,50)]
    [SerializeField] float accelerationAirbone = 10f;

    [Space] 
    
    [Header("Multipliers")]
    [Range(0, 1)] 
    [SerializeField] float airboneJumpMultiplier = 0.75f;

    bool facingRight = true;

    public float CurrentSpeed { get; private set; }
    public float TargetSpeed { get; private set; }

    public Rigidbody2D Rb { get; private set; }
    public Collision Col { get; private set; }

    void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collision>();
    }

    public void MoveHorizontally(Vector2 direction)
    {
        TargetSpeed = direction.x * moveSpeed;
        CurrentSpeed = BMath.Smoothstep1D(CurrentSpeed, TargetSpeed, (Col.OnGround ? accelerationGrounded : accelerationAirbone) * Time.deltaTime);
        Rb.velocity = new Vector2(CurrentSpeed, Rb.velocity.y);

        if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight)
            Flip();
    }

    public void Jump()
    {
        Rb.velocity = new Vector2(Rb.velocity.x, jumpSpeed);
    }
    
    public void AirboneJump()
    {
        Rb.velocity = new Vector2(Rb.velocity.x, jumpSpeed * airboneJumpMultiplier);
    }

    public void Dash(Vector2 direction)
    {
        Rb.velocity = direction.normalized * dashSpeed;
    }

    public void ResetCurrentSpeed()
    {
        CurrentSpeed = 0;
    }

    public void AddVelocity(Vector2 velocity)
    {
        Rb.velocity += velocity;
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
}