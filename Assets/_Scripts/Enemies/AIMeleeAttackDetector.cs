using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Detector para ataques cuerpo a cuerpo.
/// Verifica si el jugador está dentro de un radio especificado.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class AIMeleeAttackDetector : MonoBehaviour
    {
        /// <summary>
        /// Indica si el jugador ha sido detectado.
        /// </summary>
        public bool PlayerDetected { get; private set; }

        /// <summary>
        /// Capa objetivo que debe detectarse (e.g., "Player").
        /// </summary>
        public LayerMask targetLayerMask;

        /// <summary>
        /// Evento que se invoca cuando el jugador es detectado.
        /// Incluye como parámetro el objeto del jugador detectado.
        /// </summary>
        public UnityEvent<GameObject> onPlayerDetected;

        /// <summary>
        /// Radio del área de detección.
        /// </summary>
        [Range(0.1f, 1)] public float radius;

        /// <summary>
        /// Configuraciones para la representación de Gizmos en el editor.
        /// </summary>
        [Header("Gizmo parameters")] 
        public Color gizmoColor = Color.green;
        public bool showGizmos = true;

        /// <summary>
        /// Método llamado en cada frame.
        /// Verifica si un jugador está dentro del área de detección.
        /// </summary>
        private void Update()
        {
            // Realiza un chequeo de colisión dentro de un círculo definido por el radio.
            var collider = Physics2D.OverlapCircle(transform.position, radius, targetLayerMask);

            // Verifica si el objeto detectado pertenece a la capa objetivo ("Player").
            PlayerDetected = collider != null && collider.gameObject.layer == LayerMask.NameToLayer("Player");

            // Si se detecta un jugador, invoca el evento "onPlayerDetected".
            if (PlayerDetected)
            {
                onPlayerDetected?.Invoke(collider.gameObject);
            }
        }

        /// <summary>
        /// Dibuja un Gizmo en el editor para representar el área de detección.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawSphere(transform.position, radius);
            }
        }
    }
}
