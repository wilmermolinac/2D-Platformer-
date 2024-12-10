using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Clase que representa el estado de ataque de un agente.
/// </summary>
public class AttackState : State
{
    /// <summary>
    /// Máscara de capa que define qué objetos son atacables.
    /// </summary>
    public LayerMask hittableLayerMask;

    /// <summary>
    /// Dirección del ataque basado en la orientación del agente.
    /// </summary>
    protected Vector2 directionAttack;

    /// <summary>
    /// Evento para reproducir sonidos asociados con el arma del agente.
    /// </summary>
    public UnityEvent<AudioClip> OnWeaponSound;

    /// <summary>
    /// Define si se deben mostrar gizmos para depuración en el editor.
    /// </summary>
    [SerializeField] private bool _showGizmos = false;

    /// <summary>
    /// Método llamado al entrar en el estado de ataque.
    /// </summary>
    protected override void EnterState()
    {
        // Resetea los eventos de animación del agente.
        agent.animationManager.ResetEvents();
        
        // Reproduce la animación de ataque.
        agent.animationManager.PlayAnimation(AnimationType.Attack);
        
        // Registra los eventos de fin y acción de la animación.
        agent.animationManager.OnAnimationEnd.AddListener(TransitionToIdleState);
        agent.animationManager.OnAnimationAction.AddListener(PerformAttack);
        
        // Hace visible el arma del agente.
        agent.agentWeaponManager.ToggleWeaponVisibility(true);

        // Calcula la dirección del ataque basada en la escala del agente.
        directionAttack = agent.transform.right * (agent.transform.localScale.x > 0 ? 1 : -1);

        // Si el agente está en el suelo, detiene su movimiento.
        if (agent.groundDetector.isGrounded)
            agent.rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Realiza el ataque del agente.
    /// </summary>
    private void PerformAttack()
    {
        // Invoca el sonido del arma actual del agente.
        OnWeaponSound?.Invoke(agent.agentWeaponManager.GetCurrentWeapon().weaponSwingSound);

        // Elimina el listener para evitar múltiples ejecuciones.
        agent.animationManager.OnAnimationAction.RemoveListener(PerformAttack);

        // Ejecuta la lógica del ataque del arma.
        agent.agentWeaponManager.GetCurrentWeapon().PerformAttack(agent, hittableLayerMask, directionAttack);
    }

    /// <summary>
    /// Transiciona al estado de reposo (Idle) o caída (Fall).
    /// </summary>
    private void TransitionToIdleState()
    {
        // Elimina el listener del evento de fin de animación.
        agent.animationManager.OnAnimationEnd.RemoveListener(TransitionToIdleState);

        // Determina el próximo estado basado en si el agente está en el suelo.
        if (agent.groundDetector.isGrounded)
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Idle));
        }
        else
        {
            agent.TransitionToState(agent.stateFactory.GetState(StateType.Fall));
        }
    }

    /// <summary>
    /// Método llamado al salir del estado de ataque.
    /// </summary>
    protected override void ExitState()
    {
        // Eliminamos todos los eventos registrados en las animaciones.
        agent.animationManager.ResetEvents();
        // Oculta el arma del agente.
        agent.agentWeaponManager.ToggleWeaponVisibility(false); 
    }

    /// <summary>
    /// Dibuja gizmos para depuración en el editor.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Si el juego no está en ejecución, no dibuja gizmos.
        if (Application.isPlaying == false)
        {
            return;
        }

        // Si la opción de mostrar gizmos está desactivada, no hace nada.
        if (_showGizmos == false)
        {
            return;
        }

        // Dibuja una línea que representa el alcance del arma.
        Gizmos.color = Color.red;
        var weaponPosition = agent.agentWeaponManager.transform.position;
        agent.agentWeaponManager.GetCurrentWeapon().DrawWeaponGizmo(weaponPosition, directionAttack);
    }

    /// <summary>
    /// Maneja el intento de atacar. Bloquea ataques adicionales.
    /// </summary>
    protected override void HandleAttack()
    {
        // No permite atacar nuevamente.
    }

    /// <summary>
    /// Maneja el intento de saltar mientras está en ataque. Bloquea el salto.
    /// </summary>
    protected override void HandleJumpPressed()
    {
        // No permite saltar.
    }

    /// <summary>
    /// Maneja la liberación del salto. Sin implementación.
    /// </summary>
    protected override void HandleJumpReleased()
    {
        // Sin acción.
    }

    /// <summary>
    /// Maneja la entrada de movimiento. Detiene el giro y rotación.
    /// </summary>
    protected override void HandleMovement(Vector2 input)
    {
        // Bloquea el movimiento y el giro.
    }

    /// <summary>
    /// Actualización del estado en cada frame. No realiza ninguna acción.
    /// </summary>
    public override void StateUpdate()
    {
        // Bloquea las actualizaciones.
    }

    /// <summary>
    /// Actualización física del estado. Sin acción.
    /// </summary>
    public override void StateFixedUpdate()
    {
        // Bloquea las actualizaciones físicas.
    }
}
