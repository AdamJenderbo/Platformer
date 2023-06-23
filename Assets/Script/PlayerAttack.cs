using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Input.GetKey(KeyCode.W))
                AttackUp();
            else
                Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void AttackUp()
    {
        animator.SetTrigger("AttackUp");
    }
}
