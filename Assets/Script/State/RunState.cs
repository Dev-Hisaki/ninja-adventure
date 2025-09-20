using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RunState : PlayerStates
{
    // Stores the original scale of the player for handling direction flips
    private Vector3 initialScale;

    // Constructor passes the player and animator to the base PlayerStates class
    public RunState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Run"); // Play the "Run" animation when entering this state
        initialScale = player.transform.localScale; // Save initial scale for flipping left/right
    }

    public override void Exit()
    {
        base.Exit(); // No special exit logic for running state
    }

    public override void Tick()
    {
        base.Tick();

        // If no horizontal input, switch to Idle state
        if (Mathf.Abs(player.HorizontalInput) < 0.1f)
        {
            player.ChangeState(player.idleState);
            return;
        }

        // If jump button pressed while grounded, switch to Jump state
        if (player.JumpPressed && player.IsGrounded)
        {
            player.ChangeState(player.jumpState);
            return;
        }

        // If attack button pressed while grounded, switch to Attack state
        if (player.AttackPressed && player.IsGrounded)
        {
            player.ChangeState(player.attackState);
            return;
        }

        // Apply horizontal movement based on input and move speed
        float targetSpeed = player.HorizontalInput * player.moveSpeed;
        player.Rigidbody.velocity = new Vector2(targetSpeed, player.Rigidbody.velocity.y);

        // Flip player sprite depending on input direction
        if (player.HorizontalInput > 0.1f)
            player.transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Face right
        else if (player.HorizontalInput < -0.1f)
            player.transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z); // Face left
    }
}
