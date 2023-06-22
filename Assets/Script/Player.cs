using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int MaxHealth;
    public int Health { get; private set; }

    bool invincible;

    protected override void Start()
    {
        base.Start();
        Health = MaxHealth;
        invincible = false;
    }

    protected override void Update()
    {
        base.Update();

        Stop();

        if (Input.GetKey(KeyCode.A))
            MoveLeft();

        if (Input.GetKey(KeyCode.D))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.Space))
            WallJump();

        WallSlide();
    }

    public void Damage()
    {
        if (invincible)
            return;

        Health--;
        Flash();
    }

    public void Hit(Vector2 direction)
    {
        Damage();
    }
}
