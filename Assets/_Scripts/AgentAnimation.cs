using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Clase para manejar las animaciones del agente.
public class AgentAnimation : MonoBehaviour
{
    private Animator _animator; // Referencia al componente Animator que controla las animaciones.

    private void Awake()
    {
        // Obtiene el componente Animator del mismo GameObject.
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationType animationType)
    {
        // Método para reproducir animaciones según el tipo de animación (AnimationType).

        switch (animationType)
        {
            case AnimationType.die: // Si el agente muere.
                break;
            case AnimationType.hit: // Si el agente recibe un golpe.
                break;
            case AnimationType.idle: // Si el agente está en reposo.
                Play("Idle");
                break;
            case AnimationType.attack: // Si el agente ataca.
                break;
            case AnimationType.run: // Si el agente corre.
                Play("Run");
                break;
            case AnimationType.jump: // Si el agente salta.
                Play("Jump");
                break;
            case AnimationType.fall: // Si el agente está cayendo.
                Play("Fall");
                break;
            case AnimationType.climb: // Si el agente está escalando.
                break;
            case AnimationType.land: // Si el agente aterriza tras un salto.
                break;
            default:
                break;
        }
    }

    public void Play(string name)
    {
        // Reproduce la animación especificada por su nombre.
        _animator.Play(name, -1, 0f); // "-1" significa reproducir la animación en su capa base, "0f" reinicia la animación.
    }
}

// Enumeración para los diferentes tipos de animaciones posibles para el agente.
public enum AnimationType
{
    die,
    hit,
    idle,
    attack,
    run,
    jump,
    fall,
    climb,
    land
}