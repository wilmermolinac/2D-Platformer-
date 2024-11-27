using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Estado de movimiento del agente.
public class MovementState : State
{
    [SerializeField] protected MovementData movementData; // Datos del movimiento.
 
    public State IdleState; // Referencia al estado de reposo.

    public float acceleration, deacceleration, maxSpeed; // Parámetros de velocidad y aceleración.

    private void Awake()
    {
        movementData = GetComponentInParent<MovementData>(); // Inicializa los datos de movimiento.
    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run); // Reproduce la animación de correr.

        // Reinicia los datos de movimiento.
        movementData.horizontalMovementDirection = 0;
        movementData.currentSpeed = 0f;
        movementData.currentVelocity = Vector2.zero;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CalculateVelocity(); // Calcula la velocidad del agente.
        SetPlayerVelocity(); // Aplica la velocidad calculada.

        // Si la velocidad es casi cero, transita al estado de reposo.
        if (Mathf.Abs(agent.rb.velocity.x) < 0.01f)
        {
            agent.TransitionToState(IdleState);
        }
    }

    private void SetPlayerVelocity()
    {
        agent.rb.velocity = movementData.currentVelocity; // Aplica la velocidad al Rigidbody2D del Agent para darle el movimiento.
    }

    private void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementVector, movementData); // Calcula la velocidad.
        CalculateHorizontalDirection(movementData); // Calcula la dirección del movimiento.

        // Actualiza la velocidad actual del movimiento del personaje.
        // Esto se calcula multiplicando:
        // 1. Un Vector3 que apunta hacia la derecha (Vector3.right),
        // 2. Por la dirección del movimiento horizontal (movementData.horizontalMovementDirection),
        // 3. Por la velocidad actual del movimiento (movementData.currentSpeed).
        movementData.currentVelocity = 
            Vector3.right * movementData.horizontalMovementDirection * movementData.currentSpeed;

        
        movementData.currentVelocity.y = agent.rb.velocity.y; // Mantiene la velocidad vertical.
    }

    private void CalculateHorizontalDirection(MovementData movementDat)
    {
        // Determina la dirección horizontal según el input del jugador.
        if (agent.agentInput.MovementVector.x > 0)
        {
            //  Si es mayor a 0, significa que nos movemos hacia la derecha
            movementDat.horizontalMovementDirection = 1;
        }
        else if (agent.agentInput.MovementVector.x < 0)
        {
            //  Si es menor a 0, significa que nos movemos hacia la izquierda
            movementDat.horizontalMovementDirection = -1;
        }
    }

    private void CalculateSpeed(Vector2 movementVector, MovementData movementDat)
    {
        // Incrementa o decrementa la velocidad según el input del jugador.
        if (Mathf.Abs(movementVector.x) > 0)
        {
            movementDat.currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            movementData.currentSpeed -= deacceleration * Time.deltaTime;
        }

        // Limita la velocidad entre 0 y el valor máximo.
        movementData.currentSpeed = Mathf.Clamp(movementDat.currentSpeed, 0, maxSpeed);
    }
}
