using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


/// <summary>
/// Clase base para los estados de movimiento del agente.
/// Proporciona lógica común para calcular y aplicar el movimiento.
/// </summary>
public class MovementState : State
{
    // Referencia a los datos de movimiento, que incluyen velocidad y dirección.
    [SerializeField] protected MovementData movementData;

    public UnityEvent OnStep;

    /// <summary>
    /// Método que se ejecuta al inicializar el estado de movimiento.
    /// </summary>
    private void Awake()
    {
        // Obtiene la referencia a los datos de movimiento desde el objeto padre.
        movementData = GetComponentInParent<MovementData>();
    }

    /// <summary>
    /// Método que se ejecuta cuando el agente entra en este estado de movimiento.
    /// </summary>
    protected override void EnterState()
    {
        // Activa la animación de correr.
        agent.animationManager.PlayAnimation(AnimationType.Run);

        agent.animationManager.OnAnimationAction.AddListener(() =>
            OnStep?.Invoke()
        );

        // Reinicia los datos de movimiento a sus valores iniciales.
        movementData.horizontalMovementDirection = 0;
        movementData.currentSpeed = 0f;
        movementData.currentVelocity = Vector2.zero;
    }

    /// <summary>
    /// Lógica que se ejecuta cada frame mientras el agente está en este estado.
    /// </summary>
    public override void StateUpdate()
    {
        // Comprueba si el agente debería cambiar al estado de caída.
        if (IsTestFallTransition())
        {
            return;
        }

        // Calcula la velocidad del agente basada en el input y las propiedades del movimiento.
        CalculateVelocity();

        // Aplica la velocidad calculada al Rigidbody del agente.
        SetPlayerVelocity();

        // Si la velocidad horizontal es cercana a cero, cambia al estado de reposo.
        if (Mathf.Abs(agent.rb.velocity.x) < 0.01f)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
    }

    /// <summary>
    /// Aplica la velocidad actual calculada al Rigidbody del agente.
    /// </summary>
    protected void SetPlayerVelocity()
    {
        agent.rb.velocity = movementData.currentVelocity;
    }

    /// <summary>
    /// Calcula la velocidad del agente en función del input y los datos del movimiento.
    /// </summary>
    protected void CalculateVelocity()
    {
        // Calcula la velocidad en función de la dirección y el input horizontal.
        CalculateSpeed(agent.agentInput.MovementVector, movementData);

        // Determina la dirección horizontal según el input.
        CalculateHorizontalDirection(movementData);

        // Actualiza la velocidad horizontal del agente combinando la dirección y la velocidad actual.
        movementData.currentVelocity =
            Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;

        // Mantiene la velocidad vertical para no afectar el movimiento en el eje Y.
        movementData.currentVelocity.y = agent.rb.velocity.y;
    }

    /// <summary>
    /// Determina la dirección horizontal del agente según el input del jugador.
    /// </summary>
    protected void CalculateHorizontalDirection(MovementData movementDat)
    {
        if (agent.agentInput.MovementVector.x > 0)
        {
            // Si el input es positivo, el agente se mueve a la derecha.
            movementDat.horizontalMovementDirection = 1;
        }
        else if (agent.agentInput.MovementVector.x < 0)
        {
            // Si el input es negativo, el agente se mueve a la izquierda.
            movementDat.horizontalMovementDirection = -1;
        }
    }

    /// <summary>
    /// Calcula la velocidad del agente según el input del jugador y acelera/desacelera.
    /// </summary>
    protected void CalculateSpeed(Vector2 movementVector, MovementData movementDat)
    {
        if (Mathf.Abs(movementVector.x) > 0)
        {
            // Incrementa la velocidad si hay input horizontal.
            movementDat.currentSpeed += agent.agentData.acceleration * Time.deltaTime;
        }
        else
        {
            // Reduce la velocidad si no hay input horizontal.
            movementData.currentSpeed -= agent.agentData.deacceleration * Time.deltaTime;
        }

        // Limita la velocidad entre 0 y el valor máximo permitido.
        movementData.currentSpeed = Mathf.Clamp(movementDat.currentSpeed, 0, agent.agentData.maxSpeed);
    }

    protected override void ExitState()
    {
        agent.animationManager.ResetEvents();
    }
}