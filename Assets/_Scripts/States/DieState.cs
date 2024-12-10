using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Estado que representa la muerte del agente.
/// Hereda de la clase base "State".
/// </summary>
public class DieState : State
{
    /// <summary>
    /// Tiempo que espera antes de realizar la acción asociada a la muerte.
    /// </summary>
    public float timeToWaitBeforeDieAction = 2;

    /// <summary>
    /// Se ejecuta cuando el estado de "Die" comienza.
    /// </summary>
    protected override void EnterState()
    {
        // Reproduce la animación de muerte.
        agent.animationManager.PlayAnimation(AnimationType.Die);

        // Se suscribe al evento de fin de animación para esperar antes de la acción de muerte.
        agent.animationManager.OnAnimationEnd.AddListener(WaitBeforeDieAction);
    }

    /// <summary>
    /// Inicia una espera antes de ejecutar la acción de muerte.
    /// </summary>
    private void WaitBeforeDieAction()
    {
        // Elimina la suscripción al evento de fin de animación.
        agent.animationManager.OnAnimationEnd.RemoveListener(WaitBeforeDieAction);

        // Inicia una corrutina para esperar un tiempo antes de realizar la acción de muerte.
        StartCoroutine(WaitCoroutine());
    }

    /// <summary>
    /// Corrutina que espera un tiempo definido antes de invocar la acción de muerte.
    /// </summary>
    private IEnumerator WaitCoroutine()
    {
        // Espera el tiempo configurado.
        yield return new WaitForSeconds(timeToWaitBeforeDieAction);

        // Invoca el evento de muerte del agente.
        agent.OnAgentDie?.Invoke();
    }

    /// <summary>
    /// Se ejecuta al salir del estado de muerte.
    /// </summary>
    protected override void ExitState()
    {
        // Detiene todas las corrutinas activas asociadas a este estado.
        StopAllCoroutines();

        // Restaura los eventos de la animación para evitar comportamientos indeseados.
        agent.animationManager.ResetEvents();
    }

    /// <summary>
    /// Previene que el agente salte mientras está en el estado de muerte.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // Bloquea la acción de salto.
    }

    /// <summary>
    /// Previene que el agente reciba un golpe mientras ya está en el estado de muerte.
    /// </summary>
    public override void GetHit()
    {
        // Bloquea recibir golpes adicionales.
    }

    /// <summary>
    /// Previene que el agente ataque mientras está en el estado de muerte.
    /// </summary>
    protected override void HandleAttack()
    {
        // Bloquea la acción de ataque.
    }

    /// <summary>
    /// Previene que el agente realice un salto liberado mientras está en el estado de muerte.
    /// </summary>
    protected override void HandleJumpReleased()
    {
        // Bloquea la acción de salto liberado.
    }

    /// <summary>
    /// Lógica de actualización del estado.
    /// Durante el estado de muerte, el agente se mantiene estático en el eje X.
    /// </summary>
    public override void StateUpdate()
    {
        // Fija la velocidad en X a 0 para evitar movimiento lateral.
        agent.rb.velocity = new Vector2(0, agent.rb.velocity.y);
    }

    /// <summary>
    /// Previene que el agente entre en el estado de muerte nuevamente.
    /// </summary>
    public override void Die()
    {
        // Bloquea acciones redundantes de muerte.
    }
}
