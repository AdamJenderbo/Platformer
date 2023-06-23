using UnityEngine;

public class Player : WorldObject
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
