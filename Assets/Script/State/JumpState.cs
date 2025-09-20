using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : PlayerStates
{
    // Constructor forwards references to the base PlayerStates class
    public JumpState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Jump"); // Play the "Jump" animation

        player.IsGrounded = false; // Mark player as airborne
        // Apply vertical jump force while keeping current horizontal velocity
        player.Rigidbody.velocity = new Vector2(player.Rigidbody.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit(); // No special exit logic for jump state
    }

    public override void Tick()
    {
        base.Tick();

        // Check if player is touching the ground using OverlapCircle
        player.IsGrounded = Physics2D.OverlapCircle(
            player.groundCheck.position,
            player.groundCheckRadius,
            player.groundLayer
        );

        // If grounded and falling down (velocity.y <= 0), transition back to Idle
        if (player.IsGrounded && player.Rigidbody.velocity.y <= 0)
        {
            player.ChangeState(player.idleState);
            return;
        }

        // Allow horizontal movement in the air (air control)
        float targetSpeed = player.HorizontalInput * player.moveSpeed;
        player.Rigidbody.velocity = new Vector2(targetSpeed, player.Rigidbody.velocity.y);
    }
}
