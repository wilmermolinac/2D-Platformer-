using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que representa el estado de caída del agente en un sistema de estados.
/// Hereda de MovementState para implementar la lógica específica de caída.
/// </summary>
public class FallState : MovementState
{
    /// <summary>
    /// Método que se ejecuta cuando el agente entra en el estado de caída.
    /// </summary>
    protected override void EnterState()
    {
        // Activa la animación de caída cuando el agente comienza a caer.
        agent.animationManager.PlayAnimation(AnimationType.fall);
    }

    /// <summary>
    /// Método para manejar la acción de salto mientras el agente está cayendo.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // No se implementa ninguna acción al presionar el botón de salto durante la caída.
        // Este método está aquí para posibles ampliaciones futuras.
    }

    /// <summary>
    /// Lógica que se ejecuta cada frame mientras el agente está en el estado de caída.
    /// </summary>
    public override void StateUpdate()
    {
        // Actualiza la velocidad actual del agente.
        movementData.currentVelocity = agent.rb.velocity;

        // Aumenta la velocidad vertical debido a la gravedad personalizada.
        movementData.currentVelocity.y += agent.agentData.gravityModifier * Physics2D.gravity.y * Time.deltaTime;

        // Asigna la nueva velocidad al Rigidbody del agente.
        agent.rb.velocity = movementData.currentVelocity;

        // Calcula la nueva velocidad y la dirección del movimiento.
        CalculateVelocity();

        // Aplica la nueva velocidad calculada al agente.
        SetPlayerVelocity();

        // Si el agente detecta que está tocando el suelo, cambia al estado de reposo.
        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(idleState);
        }
    }
}
