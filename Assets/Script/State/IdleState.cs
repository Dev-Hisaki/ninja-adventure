using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the Idle state of the player.
/// This state is active when the player is standing still.
/// </summary>
public class IdleState : PlayerStates
{
    /// <summary>
    /// Constructor for IdleState. 
    /// Passes references to PlayerController and Animator to the base class.
    /// </summary>
    public IdleState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    /// <summary>
    /// Called once when entering Idle state.
    /// Plays the "Idle" animation.
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        animator.Play("Idle");
    }

    /// <summary>
    /// Called every frame while in Idle state.
    /// Checks for input to transition to other states:
    /// - Jump → JumpState
    /// - Horizontal movement (A/D) → RunState
    /// - Attack input → AttackState
    /// </summary>
    public override void Tick()
    {
        base.Tick();

        // Transition to Jump state
        if (player.JumpPressed)
        {
            player.ChangeState(player.jumpState);
            return;
        }
        // Transition to Run state when pressing A or D
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            player.ChangeState(player.runState);
            return;
        }
        // Transition to Attack state
        else if (player.AttackPressed)
        {
            player.ChangeState(player.attackState);
            return;
        }
    }
}
