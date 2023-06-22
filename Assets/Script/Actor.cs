using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : WorldObject
{
    public float moveSpeed;
    public float jumpForce;
    public float wallSlidingSpeed;

    protected Rigidbody2D rb;

    BoxCollider2D boxCollider;

    [SerializeField]
    LayerMask groundLayer;

    bool isWallSliding;

    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;

    bool isWallJumping;
    float wallJumpingDirection;
    float wallJumpingTime = 0.2f;
    float wallJumpingTimer;
    float wallJumpingDuration = 0.4f;
    Vector2 wallJumpingPower = new Vector2(8f, 16f);

    bool facingRight = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        isWallSliding = false;
    }


    protected virtual void Update()
    {
        UpdateWallJump();

        if (facingRight && rb.velocity.x < 0f || !facingRight && rb.velocity.x > 0f)
            Flip();
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if (!IsGrounded())
            return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }


    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    protected void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && rb.velocity.x != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
            isWallSliding = false;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }


    private void UpdateWallJump()
    {
        if (isWallSliding)
        {
            StopWallJumping();
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingTimer = wallJumpingTime;
        }
        else
            wallJumpingTimer -= Time.deltaTime;
    }

    protected void WallJump()
    {
        if(wallJumpingTimer > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingTimer = 0f;
            Invoke(nameof(StopWallJumping), wallJumpingDuration);

            if (transform.localScale.x != wallJumpingDirection)
                Flip();
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }
}
