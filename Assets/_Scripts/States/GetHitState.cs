using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estado que representa cuando el agente recibe un golpe.
/// Hereda de la clase base "State".
/// </summary>
public class GetHitState : State
{
    /// <summary>
    /// Se ejecuta cuando el estado de "GetHit" comienza.
    /// </summary>
    protected override void EnterState()
    {
        // Reproduce la animación de recibir daño.
        agent.animationManager.PlayAnimation(AnimationType.Hit);

        // Se suscribe al evento de fin de animación para transicionar al estado "Idle".
        agent.animationManager.OnAnimationEnd.AddListener(TransitionIdle);
    }

    /// <summary>
    /// Transiciona al estado "Idle" una vez finaliza la animación.
    /// </summary>
    private void TransitionIdle()
    {
        // Elimina la suscripción al evento de fin de animación.
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionIdle);

        // Transiciona al estado "Idle" usando el "StateFactory".
        agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
    }

    /// <summary>
    /// Previene que el agente ataque mientras está en este estado.
    /// </summary>
    protected override void HandleAttack()
    {
        // Bloquea la acción de ataque en este estado.
    }

    /// <summary>
    /// Previene que el agente salte mientras está en este estado.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // Bloquea la acción de salto en este estado.
    }

    /// <summary>
    /// Lógica de actualización del estado.
    /// Este estado no realiza acciones específicas en el "Update".
    /// </summary>
    public override void StateUpdate()
    {
        // Bloquea cualquier lógica de actualización en este estado.
    }

    /// <summary>
    /// Previene que el agente reciba un golpe adicional mientras ya está en este estado.
    /// </summary>
    public override void GetHit()
    {
        // Bloquea recibir golpes adicionales.
    }
}

