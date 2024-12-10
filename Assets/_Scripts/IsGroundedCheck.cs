using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Comprueba si el personaje está en el suelo y realiza una acción si la condición es válida.
/// </summary>
public class IsGroundedCheck : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector; // Referencia a un detector que verifica si el personaje está en el suelo.

    public UnityEvent OnConditionValidAction; // Evento que se invoca cuando la condición es válida (está en el suelo).

    /// <summary>
    /// Intenta realizar una acción si el personaje está en el suelo.
    /// </summary>
    public void TryPerformingAction()
    {
        // Comprueba si el detector indica que el personaje está en el suelo.
        if (_groundDetector.isGrounded)
        {
            // Invoca el evento asociado si la condición es válida.
            OnConditionValidAction?.Invoke();
        }
    }
}
