using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// Clase para manejar las animaciones del agente.
/// <summary>
/// Clase que controla las animaciones del agente.
/// </summary>
public class AgentAnimation : MonoBehaviour
{
    private Animator _animator; // Referencia al componente Animator que controla las animaciones.
    
    public UnityEvent OnAnimationAction; // Evento que se activa durante la animación.
    public UnityEvent OnAnimationEnd; // Evento que se activa al finalizar la animación.

    private void Awake()
    {
        // Obtiene el componente Animator del mismo GameObject.
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Reproduce una animación basada en su tipo.
    /// </summary>
    public void PlayAnimation(AnimationType animationType)
    {
        // Método para reproducir animaciones según el tipo de animación (AnimationType).

        switch (animationType)
        {
            case AnimationType.Die: // Si el agente muere.
                Play("Die");
                break;
            case AnimationType.Hit: // Si el agente recibe un golpe.
                Play("GetHit");
                break;
            case AnimationType.Idle: // Si el agente está en reposo.
                Play("Idle");
                break;
            case AnimationType.Attack: // Si el agente ataca.
                Play("Attack");
                break;
            case AnimationType.Run: // Si el agente corre.
                Play("Run");
                break;
            case AnimationType.Jump: // Si el agente salta.
                Play("Jump");
                break;
            case AnimationType.Fall: // Si el agente está cayendo.
                Play("Fall");
                break;
            case AnimationType.Climb: // Si el agente está escalando.
                Play("Climbing");
                break;
            case AnimationType.Land: // Si el agente aterriza tras un salto.
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Reproduce una animación específica por nombre.
    /// </summary>
    public void Play(string name)
    {
        _animator.Play(name, -1, 0f); // Reproduce la animación desde el inicio.
    }

    /// <summary>
    /// Detiene todas las animaciones.
    /// </summary>
    internal void StopAnimation()
    {
        _animator.enabled = false; // Desactiva el componente Animator.
    }

    /// <summary>
    /// Activa las animaciones.
    /// </summary>
    internal void StartAnimation()
    {
        _animator.enabled = true; // Activa el componente Animator.
    }

    /// <summary>
    /// Resetea todos los eventos registrados en las animaciones.
    /// </summary>
    public void ResetEvents()
    {
        OnAnimationAction.RemoveAllListeners();
        OnAnimationEnd.RemoveAllListeners();
    }

    /// <summary>
    /// Invoca el evento de acción durante la animación.
    /// </summary>
    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    /// <summary>
    /// Invoca el evento al finalizar la animación.
    /// </summary>
    public void InvokeAnimationEnd()
    {
        OnAnimationEnd?.Invoke();
    }
}

/// <summary>
/// Enumeración para identificar los diferentes tipos de animaciones del agente.
/// </summary>
public enum AnimationType
{
    Die,
    Hit,
    Idle,
    Attack,
    Run,
    Jump,
    Fall,
    Climb,
    Land
}