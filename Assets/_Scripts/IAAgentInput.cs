using System;
using UnityEngine;

// Creamos una Interface con los eventos
public interface IAAgentInput
{
    /// <summary>
    /// Vector que representa la dirección de movimiento del jugador.
    /// Se define como público con acceso de solo lectura desde fuera de la clase.
    /// </summary>
    Vector2 MovementVector { get; }

    // Eventos que se activan cuando ocurren acciones específicas, permitiendo que otros scripts reaccionen a ellas.
    // **Eventos de acciones del jugador**
        
    /// <summary>Se activa cuando el jugador realiza un ataque.</summary>
    event Action OnAttack;
    /// <summary>Se activa cuando el jugador presiona el botón de salto.</summary>
    event Action OnJumpPressed;
    /// <summary>Se activa cuando el jugador suelta el botón de salto.</summary>
    event Action OnJumpReleased;
    /// <summary>Se activa cuando el jugador cambia de arma.</summary>
    event Action OnWeaponChange;

    /// <summary>Notifica cambios en el movimiento del jugador, pasando el vector de movimiento como parámetro.</summary>
    event Action<Vector2> OnMovement;
}