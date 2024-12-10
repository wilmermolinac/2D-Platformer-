using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Clase base abstracta para los estados del agente.
// Las clases abstractas obligan a las clases derivadas a implementar los métodos abstractos de la clase padre.
// Podemos sobreescribir y proporcionar una implementación a los métodos abstractos.
// También podemos sobrescribir métodos virtuales si es necesario.
// Si tenemos métodos protected virtual, no es obligatorio implementarlos en las clases derivadas.

/// <summary>
/// Clase base abstracta para los estados del agente en un sistema de estados.
/// Define la estructura y el comportamiento común de todos los estados.
/// </summary>
public abstract class State : MonoBehaviour
{
    // Referencia al agente asociado con este estado.
    protected Agent agent;

    // Eventos que se disparan al entrar y salir del estado.
    public UnityEvent OnEnter, OnExit;

    /// <summary>
    /// Inicializa el estado asignándole una referencia al agente que lo utilizará.
    /// </summary>
    /// <param name="agent">El agente asociado a este estado.</param>
    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    /// <summary>
    /// Método que se llama cuando el agente entra en este estado.
    /// </summary>
    public void Enter()
    {
        // Suscribe los métodos correspondientes a los eventos del sistema de entrada del agente.
        this.agent.agentInput.OnAttack += HandleAttack;
        this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
        this.agent.agentInput.OnMovement += HandleMovement;

        // Invoca el evento OnEnter si está configurado.
        OnEnter?.Invoke();

        // Llama a la lógica específica del estado al entrar.
        EnterState();
    }

    /// <summary>
    /// Método virtual que puede ser sobrescrito para agregar lógica específica al entrar al estado.
    /// </summary>
    protected virtual void EnterState()
    {
    }

    /// <summary>
    /// Maneja la entrada de movimiento del jugador.
    /// </summary>
    /// <param name="input">Vector de entrada de movimiento.</param>
    protected virtual void HandleMovement(Vector2 input)
    {
    }

    /// <summary>
    /// Método llamado cuando se libera el botón de salto.
    /// </summary>
    protected virtual void HandleJumpReleased()
    {
    }

    /// <summary>
    /// Método llamado cuando se presiona el botón de salto.
    /// </summary>
    protected virtual void HandleJumpPressed()
    {
        // Prueba si se debe transitar al estado de salto.
        TestJumpTransition();
    }

    /// <summary>
    /// Comprueba si el agente debería transitar al estado de salto.
    /// </summary>
    private void TestJumpTransition()
    {
        if (agent.groundDetector.isGrounded)
        {
            // Si el agente está tocando el suelo, transita al estado de salto.
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Jump));
        }
    }

    /// <summary>
    /// Método llamado cuando se presiona el botón de ataque.
    /// </summary>
    protected virtual void HandleAttack()
    {
        TestAttackTransition();
    }

    /// <summary>
    /// Método que se llama en cada frame para actualizar la lógica del estado.
    /// </summary>
    public virtual void StateUpdate()
    {
        // Comprueba si el agente debería transitar al estado de caída.
        IsTestFallTransition();
    }

    /// <summary>
    /// Comprueba si el agente debería transitar al estado de caída.
    /// </summary>
    /// <returns>Devuelve true si se realizó la transición al estado de caída.</returns>
    protected bool IsTestFallTransition()
    {
        if (!agent.groundDetector.isGrounded)
        {
            // Si el agente no está tocando el suelo, transita al estado de caída.
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Método virtual llamado para actualizar la lógica física del estado.
    /// </summary>
    public virtual void StateFixedUpdate()
    {
    }
    
    protected virtual void TestAttackTransition()
    {
        if (agent.agentWeaponManager.CanIUseWeapon(agent.groundDetector.isGrounded))
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Attack));
        }
    }

    public virtual void GetHit()
    {
        // Hacemos la trasicion al estado GetHit
        agent.TransitionToState(agent.stateFactory.GetState(StateType.GetHit));
    }

    public virtual void Die()
    {
        // Hacemos la trancision al estado Die
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Die));
    }
    

    /// <summary>
    /// Método que se llama cuando el agente sale de este estado.
    /// </summary>
    public void Exit()
    {
        // Elimina la suscripción de los eventos del sistema de entrada del agente.
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
        this.agent.agentInput.OnMovement -= HandleMovement;

        // Invoca el evento OnExit si está configurado.
        OnExit?.Invoke();

        // Llama a la lógica específica al salir del estado.
        ExitState();
    }

    /// <summary>
    /// Método virtual que puede ser sobrescrito para agregar lógica específica al salir del estado.
    /// </summary>
    protected virtual void ExitState()
    {
    }
    
}