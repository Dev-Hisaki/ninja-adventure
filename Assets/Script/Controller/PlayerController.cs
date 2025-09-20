using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensures the object always has a Rigidbody2D
[RequireComponent(typeof(Animator))]    // Ensures the object always has an Animator
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;              // Horizontal movement speed
    public float jumpForce = 8f;              // Force applied when jumping
    public LayerMask groundLayer;             // Layer mask to identify ground
    public Transform groundCheck;             // Transform used to check if player is touching ground
    public float groundCheckRadius = 0.1f;    // Radius for ground check overlap
    public bool IsGrounded = false;           // Flag to indicate if player is currently grounded

    [Header("Attributes")]
    public int maxHealth = 3;                 // Maximum health value
    public int currentHealth;                 // Current health value

    [HideInInspector] public Rigidbody2D Rigidbody;  // Reference to player's Rigidbody2D
    [HideInInspector] public Animator Animator;      // Reference to Animator component

    [HideInInspector] public float HorizontalInput;  // Horizontal input (-1, 0, 1)
    [HideInInspector] public bool JumpPressed;       // True if Jump button was pressed this frame
    [HideInInspector] public bool AttackPressed;     // True if Attack button was pressed this frame

    // State machine references
    [HideInInspector] public IdleState idleState;
    [HideInInspector] public RunState runState;
    [HideInInspector] public JumpState jumpState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public HurtState hurtState;
    [HideInInspector] public DieState dieState;

    [HideInInspector] public string currentAnimationState; // Keeps track of the current animation clip

    private PlayerStates currentState;        // Current active state
    public bool IsDead { get; set; } = false; // Property to check if player is dead

    void Awake()
    {
        // Get references to required components
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        // Initialize all possible states and pass references
        idleState = new IdleState(this, Animator);
        runState = new RunState(this, Animator);
        jumpState = new JumpState(this, Animator);
        attackState = new AttackState(this, Animator);
        hurtState = new HurtState(this, Animator);
        dieState = new DieState(this, Animator);
    }

    void Start()
    {
        // Set default state to Idle
        ChangeState(idleState);

        // Initial ground check
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Initialize health
        currentHealth = maxHealth;

        // Update health UI on game start
        GameObject.FindObjectOfType<HealthManager>().UpdateHealthDisplay(currentHealth);
    }

    void Update()
    {
        // Handle player input each frame
        ReadInput();

        // Update the active state
        currentState?.Tick();
    }

    void FixedUpdate()
    {
        // Physics updates for the current state
        currentState?.FixedTick();
    }

    /// <summary>
    /// Reads input from Unity's Input system.
    /// </summary>
    void ReadInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal"); // -1 (left), 0 (idle), 1 (right)
        JumpPressed = Input.GetButtonDown("Jump");        // Spacebar by default
        AttackPressed = Input.GetButtonDown("Fire1");     // Left mouse button / Ctrl by default
    }

    /// <summary>
    /// Changes the current player state.
    /// </summary>
    /// <param name="newState">The new state to enter.</param>
    public void ChangeState(PlayerStates newState)
    {
        if (newState == null || newState == currentState) return;

        currentState?.Exit();     // Clean up old state
        currentState = newState;  // Switch reference
        currentState.Enter();     // Initialize new state
    }

    /// <summary>
    /// Applies vertical velocity to perform a jump.
    /// </summary>
    public void ApplyJump()
    {
        Vector2 v = Rigidbody.velocity;
        v.y = jumpForce;          // Set Y velocity directly for instant jump
        Rigidbody.velocity = v;
    }

    /// <summary>
    /// Called when player collides with another collider.
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            Debug.Log("Collided with obstacle");

            // Switch to hurt state when colliding with obstacle
            ChangeState(hurtState);
        }
    }
}
