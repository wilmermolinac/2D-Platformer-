using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Estado "Idle" (reposo) del agente.
public class IdleState : State
{
    public State MoveState; // Referencia al estado de movimiento.

    protected override void EnterState()
    {    
        agent.animationManager.PlayAnimation(AnimationType.idle); // Reproduce la animación de reposo.
    }

    protected override void HandleMovement(Vector2 input)
    {
        // Si detecta movimiento horizontal, transita al estado de movimiento.
        if (Mathf.Abs(input.x) > 0)
        {
            agent.TransitionToState(MoveState);
        }
    }
}