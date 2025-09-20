using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerStates
{
    // Constructor to pass PlayerController and Animator to the base class
    public AttackState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Attack"); // Play the attack animation
        // Stop horizontal movement while attacking to keep animation consistent
        player.Rigidbody.velocity = new Vector2(0, player.Rigidbody.velocity.y);
    }

    public override void Exit()
    {
        base.Exit(); // No special behavior when exiting the attack state
    }

    public override void Tick()
    {
        base.Tick();

        // Check if the player is currently grounded
        player.IsGrounded = Physics2D.OverlapCircle(
            player.groundCheck.position,
            player.groundCheckRadius,
            player.groundLayer
        );

        // Get information about the currently playing animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // If the "Attack" animation is currently playing
        if (stateInfo.IsName("Attack"))
        {
            // If animation has finished (normalizedTime > 1 means 100% complete)
            if (stateInfo.normalizedTime > 1.0f)
            {
                TransitionAfterAttack(); // Decide which state to go to next
            }
        }
        else
        {
            // If "Attack" is not the current animation, immediately transition
            TransitionAfterAttack();
        }

        // Debug log to monitor animation progress
        Debug.Log("Attack normalizedTime: " + stateInfo.normalizedTime.ToString("F2"));
    }

    // Helper function to decide the next state after the attack animation ends
    private void TransitionAfterAttack()
    {
        if (!player.IsGrounded) // If player is airborne, go to JumpState
        {
            player.ChangeState(player.jumpState);
        }
        else if (Mathf.Abs(player.HorizontalInput) > 0.1f) // If there is movement input, go to RunState
        {
            player.ChangeState(player.runState);
        }
        else // Otherwise, return to IdleState
        {
            player.ChangeState(player.idleState);
        }
    }
}
