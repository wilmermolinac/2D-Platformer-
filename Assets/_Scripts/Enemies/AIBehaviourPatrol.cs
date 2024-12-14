using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Comportamiento de patrullaje para un agente AI.
/// Permite que el agente patrulle mientras verifica el fin de la plataforma.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class AIBehaviourPatrol : AiBehaviour
    {
        /// <summary>
        /// Detector que identifica si el agente está al borde de una plataforma.
        /// </summary>
        public AIEndPlatformDetector changeDirectionDetector;

        /// <summary>
        /// Vector de movimiento del agente durante el patrullaje.
        /// </summary>
        private Vector2 _movementVector = Vector2.zero;

        /// <summary>
        /// Método llamado al inicializar el script. 
        /// Se asegura de que el detector esté configurado correctamente.
        /// </summary>
        private void Awake()
        {
            if (changeDirectionDetector == null)
                changeDirectionDetector = transform.parent.GetComponentInParent<AIEndPlatformDetector>();
        }

        /// <summary>
        /// Método llamado al iniciar el script.
        /// Configura el detector y define una dirección de movimiento inicial.
        /// </summary>
        private void Start()
        {
            // Vincula el evento "OnPathBlocked" a la función de cambio de dirección.
            changeDirectionDetector.OnPathBlocked += HandlePathBlocked;

            // Asigna un valor aleatorio para determinar si comienza moviéndose hacia la derecha o la izquierda.
            int movementX = Random.value > 0.5f ? 1 : -1;
            _movementVector = new Vector2(movementX, 0);
        }

        /// <summary>
        /// Maneja el cambio de dirección cuando el camino está bloqueado.
        /// Invierte la dirección del movimiento.
        /// </summary>
        private void HandlePathBlocked()
        {
            _movementVector *= new Vector2(-1, 0); // Invierte la dirección en el eje X.
        }

        /// <summary>
        /// Realiza la acción de patrullaje, aplicando el movimiento al agente.
        /// </summary>
        /// <param name="enemyAI">Instancia del agente enemigo que ejecuta este comportamiento.</param>
        public override void PerformAction(AiEnemy enemyAI)
        {
            // Actualiza el vector de movimiento del agente y notifica el cambio.
            enemyAI.MovementVector = _movementVector;
            enemyAI.CallOnMovement(_movementVector);
        }
    }
}
