using System;
using UnityEngine;

/// <summary>
/// Clase base abstracta para enemigos controlados por AI.
/// Implementa la interfaz IAAgentInput para manejar las entradas.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public abstract class AiEnemy : MonoBehaviour, IAAgentInput
    {
        /// <summary>
        /// Vector de movimiento del agente.
        /// </summary>
        public Vector2 MovementVector { get; set; }

        // **Eventos implementados de la interfaz IAAgentInput**
        public event Action OnAttack;
        public event Action OnJumpPressed;
        public event Action OnJumpReleased;
        public event Action OnWeaponChange;
        public event Action<Vector2> OnMovement;

        /// <summary>
        /// Invoca el evento de ataque.
        /// </summary>
        public void CallOnAttack()
        {
            OnAttack?.Invoke();
        }

        /// <summary>
        /// Invoca el evento de presión del salto.
        /// </summary>
        public void CallOnJumpPressed()
        {
            OnJumpPressed?.Invoke();
        }

        /// <summary>
        /// Invoca el evento de liberación del salto.
        /// </summary>
        public void CallOnJumpReleased()
        {
            OnJumpReleased?.Invoke();
        }

        /// <summary>
        /// Invoca el evento de movimiento, notificando el vector de entrada.
        /// </summary>
        /// <param name="input">Vector de movimiento.</param>
        public void CallOnMovement(Vector2 input)
        {
            OnMovement?.Invoke(input);
        }

        /// <summary>
        /// Invoca el evento de cambio de arma.
        /// </summary>
        public void CallOnWeaponChange()
        {
            OnWeaponChange?.Invoke();
        }
    }
}
