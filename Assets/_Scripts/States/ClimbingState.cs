using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que define el estado de escalada del personaje.
/// Gestiona la transición y las acciones específicas mientras se escala.
/// </summary>
public class ClimbingState : State
{
    /// <summary>
    /// Almacena el valor original de la escala de gravedad del Rigidbody del personaje
    /// para restaurarlo al salir del estado de escalada.
    /// </summary>
    private float previousGravityScale = 0;

    /// <summary>
    /// Método que se ejecuta al entrar en el estado de escalada.
    /// </summary>
    protected override void EnterState()
    {
        // Reproduce la animación de escalada.
        agent.animationManager.PlayAnimation(AnimationType.climb);

        // Detiene cualquier otra animación que estuviera activa.
        agent.animationManager.StopAnimation();

        // Almacena la escala de gravedad actual para restaurarla después.
        previousGravityScale = agent.rb.gravityScale;

        // Desactiva la gravedad mientras el personaje está escalando.
        agent.rb.gravityScale = 0;

        // Detiene cualquier movimiento existente al entrar en el estado.
        agent.rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Maneja la entrada del jugador para saltar mientras está en el estado de escalada.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // Transición al estado de salto si el jugador presiona el botón de salto.
        agent.TransitionToState(jumpState);
    }

    /// <summary>
    /// Actualiza las acciones del personaje cada frame mientras está en el estado de escalada.
    /// </summary>
    public override void StateUpdate()
    {
        // Si el jugador proporciona entrada de movimiento.
        if (agent.agentInput.MovementVector.magnitude > 0)
        {
            // Reanuda la animación de escalada.
            agent.animationManager.StartAnimation();

            // Actualiza la velocidad del personaje según la dirección y las velocidades configuradas.
            agent.rb.velocity = new Vector2(
                agent.agentInput.MovementVector.x * agent.agentData.climbHorizontalSpeed,
                agent.agentInput.MovementVector.y * agent.agentData.climbVerticalSpeed
            );
        }
        else
        {
            // Detiene la animación y el movimiento si no hay entrada.
            agent.animationManager.StopAnimation();
            agent.rb.velocity = Vector2.zero;
        }

        // Comprueba si el personaje ya no está en contacto con superficies escalables.
        if (agent.climbingDetector.CanClimb == false)
        {
            // Transición al estado de reposo (Idle).
            agent.TransitionToState(agent.idleState);
        }
    }

    /// <summary>
    /// Método que se ejecuta al salir del estado de escalada.
    /// </summary>
    protected override void ExitState()
    {
        // Restaura la escala de gravedad original del Rigidbody.
        agent.rb.gravityScale = previousGravityScale;

        // Reanuda cualquier animación que estuviera activa.
        agent.animationManager.StartAnimation();
    }
}
