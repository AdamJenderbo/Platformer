using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : WorldObject
{
    [SerializeField]
    public float moveSpeed;
    protected Vector2 moveDirection;

    Rigidbody2D rb;

    protected Vector3 position { get { return transform.position; } }

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        moveDirection.Normalize();
        SetVelocity(moveDirection * moveSpeed);
    }

    protected void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
}
