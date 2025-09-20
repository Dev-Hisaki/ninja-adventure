using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : PlayerStates
{
    private float hurtDuration = 1.0f; // Duration the player stays in "Hurt" state
    private float hurtTimer;           // Timer to track how long the player has been hurt
    private bool isGrounded;           // Stores whether the player is touching the ground

    // Constructor to connect PlayerController and Animator
    public HurtState(PlayerController player, Animator animator) : base(player, animator)
    {
    }

    public override void Enter()
    {
        base.Enter();

        animator.Play("Hurt"); // Play "Hurt" animation

        // Apply knockback force opposite to the player's facing direction
        player.Rigidbody.AddForce(
            new Vector2(-player.transform.localScale.x * 5f, 2f),
            ForceMode2D.Impulse
        );

        hurtTimer = 0f; // Reset timer when entering this state

        TakeDamage(1); // Reduce health whenever hurt is triggered
    }

    public override void Exit()
    {
        base.Exit(); // No special behavior on exiting HurtState
    }

    public override void Tick()
    {
        base.Tick();

        // Increase timer with elapsed time
        hurtTimer += Time.deltaTime;

        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(
            player.groundCheck.position,
            player.groundCheckRadius,
            player.groundLayer
        );

        // After hurt duration ends, decide the next state
        if (hurtTimer >= hurtDuration)
        {
            if (isGrounded) // If grounded, return to IdleState
            {
                player.ChangeState(player.idleState);
            }
            else // If still in the air, go to JumpState
            {
                player.ChangeState(player.jumpState);
            }
        }
    }

    public override void FixedTick()
    {
        base.FixedTick(); // No additional physics logic in HurtState
    }

    // Function to reduce player health
    public void TakeDamage(int damage)
    {
        if (player.IsDead) return; // If player is already dead, skip damage

        player.currentHealth--; // Decrease health
        Debug.Log("Player took damage! Current Health: " + player.currentHealth);

        // Update health UI through HealthManager
        GameObject.FindObjectOfType<HealthManager>().UpdateHealthDisplay(player.currentHealth);

        // If health reaches zero, switch to DieState
        if (player.currentHealth == 0)
        {
            player.ChangeState(player.dieState);
            return;
        }
    }
}
