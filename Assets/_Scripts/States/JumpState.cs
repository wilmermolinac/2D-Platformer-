using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Clase que representa el estado de salto de un agente dentro de un sistema de estados.
/// Hereda de MovementState.
/// </summary>
public class JumpState : MovementState
{
    // Variable para indicar si el botón de salto sigue presionado.
    private bool jumpPressed = false;

    /// <summary>
    /// Método que se ejecuta al entrar en el estado de salto.
    /// </summary>
    protected override void EnterState()
    {
        // Activa la animación de salto.
        agent.animationManager.PlayAnimation(AnimationType.Jump);

        // Obtiene la velocidad actual del agente.
        movementData.currentVelocity = agent.rb.velocity;

        // Ajusta la componente vertical de la velocidad para iniciar el salto.
        movementData.currentVelocity.y = agent.agentData.jumpForce;

        // Asigna la nueva velocidad al Rigidbody para que el agente salte.
        agent.rb.velocity = movementData.currentVelocity;

        // Marca que el botón de salto está presionado.
        jumpPressed = true;
    }

    /// <summary>
    /// Método que se ejecuta cuando se detecta que el botón de salto ha sido presionado.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // Cambia el estado de la variable para indicar que el salto sigue activo.
        jumpPressed = true;
    }

    /// <summary>
    /// Método que se ejecuta cuando se detecta que el botón de salto ha sido liberado.
    /// </summary>
    protected override void HandleJumpReleased()
    {
        // Cambia el estado de la variable para indicar que el salto ha terminado.
        jumpPressed = false;
    }

    /// <summary>
    /// Método que actualiza la lógica del estado en cada frame.
    /// </summary>
    public override void StateUpdate()
    {
        // Controla la altura del salto en función de si el botón sigue presionado.
        ControlJumpHeight();

        // Calcula la nueva velocidad del agente en base a la lógica del movimiento.
        CalculateVelocity();

        // Aplica la nueva velocidad al Rigidbody del agente.
        SetPlayerVelocity();

        // Si la velocidad vertical es cero o negativa, transiciona al estado de caída.
        if (agent.rb.velocity.y <= 0)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall)); // Cambia al estado de caída.
        }
        else if (agent.climbingDetector.CanClimb && Mathf.Abs(agent.iaAgentInput.MovementVector.y) > 0)
        {
            //Si podemos escalar y si estamos precionando las teclas verticales 
            // Hacemos transicion al ClimbingState
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Climbing));
        }
    }

    /// <summary>
    /// Método que ajusta la altura del salto cuando el botón de salto no está presionado.
    /// </summary>
    private void ControlJumpHeight()
    {
        // Si el botón de salto no está presionado, reduce la altura del salto.
        if (jumpPressed == false)
        {
            // Obtiene la velocidad actual del agente.
            movementData.currentVelocity = agent.rb.velocity;

            // Ajusta la velocidad vertical utilizando un multiplicador que reduce la altura del salto.
            movementData.currentVelocity.y += agent.agentData.lowJumpMultiplier * Physics2D.gravity.y * Time.deltaTime;

            // Asigna la nueva velocidad al Rigidbody del agente.
            agent.rb.velocity = movementData.currentVelocity;
        }
    }
}