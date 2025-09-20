## Character State Machine

This project implements a **State Machine Pattern** in Unity to control a 2D character.  
Each behavior (Idle, Run, Jump, Attack, Hurt, Die) is handled by a dedicated state class, making the code modular and easy to extend.

### States
- **IdleState** → Default state, waiting for input.
- **RunState** → Handles horizontal movement and flipping.
- **JumpState** → Applies jump force and checks landing.
- **AttackState** → Plays attack animation, then transitions back.
- **HurtState** → Handles knockback and health reduction.
- **DieState** → Final state when health reaches zero.
- **HealthManager** → Updates UI to display current health.

### Flow
Idle → Run → Jump → Attack → Hurt → Die

### Notes
- Clean separation of logic per state.  
- Easy to extend with new states (e.g., Dash, Climb).  
- `HealthManager` keeps UI updates separate from gameplay logic.
