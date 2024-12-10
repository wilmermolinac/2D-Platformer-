using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Estado de reposo (Idle) del agente. 
/// Define la lógica específica cuando el agente no está en movimiento.
/// </summary>
public class IdleState : State
{
    
    /// <summary>
    /// Lógica específica al entrar al estado de reposo.
    /// </summary>
    protected override void EnterState()
    {
        // Reproduce la animación de reposo.
        agent.animationManager.PlayAnimation(AnimationType.Idle);

        // Si el agente está tocando el suelo, detiene su movimiento estableciendo la velocidad en cero.
        if (agent.groundDetector.isGrounded)
        {
            agent.rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Maneja la entrada de movimiento y transita al estado de movimiento si hay input horizontal.
    /// </summary>
    /// <param name="input">Vector de entrada de movimiento.</param>
    protected override void HandleMovement(Vector2 input)
    {
        if (agent.climbingDetector.CanClimb && Mathf.Abs(input.y) > 0)
        {
            // si podemos escalar y el valor absoluto de la entrada de Y (Vertical) es mayor que 0
            // Hacemos la transicion al climbState
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climbing));
        }
        else if (Mathf.Abs(input.x) > 0)
        {
            // Si hay movimiento horizontal, transita al estado de movimiento.
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Move));
        }
    }
}