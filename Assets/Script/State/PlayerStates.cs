using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for all player states.
/// Each state (Idle, Run, Jump, Attack, etc.) should inherit from this class
/// and override the relevant methods to define specific behavior.
/// </summary>
public abstract class PlayerStates
{
    // Reference to the PlayerController for accessing player properties and methods
    protected PlayerController player;

    // Reference to the Animator for controlling animations
    protected Animator animator;

    /// <summary>
    /// Constructor for PlayerStates.
    /// Stores references to the PlayerController and Animator.
    /// </summary>
    public PlayerStates(PlayerController player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    /// <summary>
    /// Called once when the state is first entered.
    /// Use this for initialization (e.g., play an animation).
    /// </summary>
    public virtual void Enter() { }

    /// <summary>
    /// Called once when the state is exited.
    /// Use this to clean up or reset values.
    /// </summary>
    public virtual void Exit() { }

    /// <summary>
    /// Called every frame (Update).
    /// Use this for handling input or non-physics logic.
    /// </summary>
    public virtual void Tick() { }

    /// <summary>
    /// Called every physics frame (FixedUpdate).
    /// Use this for physics-related logic.
    /// </summary>
    public virtual void FixedTick() { }
}
