using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : PlayerStates
{
    // Constructor to link PlayerController and Animator
    public DieState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Die");                     // Play death animation
        player.Rigidbody.velocity = Vector2.zero; // Stop all movement immediately
        player.IsDead = true;                     // Mark the player as dead to prevent further actions
    }

    public override void Exit()
    {
        base.Exit();
        // No exit logic needed since death is usually a terminal state
    }

    public override void Tick()
    {
        base.Tick();
        // No updates are processed in the death state
    }

    public override void FixedTick()
    {
        base.FixedTick();
        // No physics updates required in the death state
    }
}
