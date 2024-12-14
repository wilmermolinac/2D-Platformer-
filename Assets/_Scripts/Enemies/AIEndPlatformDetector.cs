using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta el final de una plataforma para agentes de inteligencia artificial.
/// Pertenece al espacio de nombres "WAMCSTUDIOS.AI".
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class AIEndPlatformDetector : MonoBehaviour
    {
        /// <summary>
        /// Colisionador utilizado para detectar el área del agente.
        /// </summary>
        public BoxCollider2D detectorCollider;

        /// <summary>
        /// Capa que identifica el suelo. 
        /// Se utiliza para filtrar las detecciones del raycast.
        /// </summary>
        public LayerMask groundMask;

        /// <summary>
        /// Longitud del raycast que se lanza hacia abajo para detectar el suelo.
        /// </summary>
        public float groundRaycastLength = 2;

        /// <summary>
        /// Retraso en segundos antes de lanzar el raycast hacia el suelo.
        /// </summary>
        [Range(0, 1)] public float groundRaycastDelay = 0.1f;

        /// <summary>
        /// Indica si el camino está bloqueado (por ejemplo, si no hay suelo).
        /// </summary>
        public bool PathBlocked { get; private set; }

        /// <summary>
        /// Evento que se invoca cuando se detecta que el camino está bloqueado.
        /// </summary>
        public event Action OnPathBlocked;

        [Header("Gizmo Parameters")]
        /// <summary>
        /// Color del gizmo que muestra el área del colisionador en la escena.
        /// </summary>
        public Color colliderColor = Color.magenta;

        /// <summary>
        /// Color del gizmo que representa el raycast lanzado hacia el suelo.
        /// </summary>
        public Color groundRaycastColor = Color.blue;

        /// <summary>
        /// Controla si los gizmos deben mostrarse en la vista de escena.
        /// </summary>
        public bool showGizmos = true;

        private void Awake()
        {
            // si es igual a nulo 
            if (detectorCollider == null)
            {
                // Lo inicializamos por codigo buscando el componente en este mismo GameObject
                detectorCollider = GetComponent<BoxCollider2D>();
            }
        }

        /// <summary>
        /// Método llamado al iniciar el script.
        /// Comienza a verificar periódicamente si hay suelo debajo del agente.
        /// </summary>
        private void Start()
        {
            // Inicia la corrutina que verifica el suelo bajo el agente.
            StartCoroutine(CheckGroundCoroutine());
        }

        /// <summary>
        /// Corrutina que verifica si hay suelo debajo del agente usando un raycast.
        /// </summary>
        IEnumerator CheckGroundCoroutine()
        {
            // Espera un tiempo antes de ejecutar la verificación.
            yield return new WaitForSeconds(groundRaycastDelay);

            // Lanza un raycast desde el centro del colisionador hacia abajo para detectar suelo.
            var hit = Physics2D.Raycast(detectorCollider.bounds.center, Vector2.down, groundRaycastLength, groundMask);

            // Si no se detecta suelo, invoca el evento "OnPathBlocked".
            if (hit.collider == null)
            {
                OnPathBlocked?.Invoke();
            }

            // Actualiza el estado de "PathBlocked" dependiendo del resultado del raycast.
            PathBlocked = hit.collider == null;

            // Repite la verificación llamando nuevamente a la corrutina.
            StartCoroutine(CheckGroundCoroutine());
        }

        /// <summary>
        /// Método llamado cuando un colisionador entra en contacto con el detector.
        /// </summary>
        /// <param name="collision">Información del colisionador que activó este método.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Invoca el evento "OnPathBlocked" si se detecta una colisión relevante.
            if (collision.gameObject.layer == LayerMask.NameToLayer("ClimbingStuff"));
            {
               return;
            }
            
            OnPathBlocked?.Invoke();
        }

        // Configuramos para poder ver los gizmos en el editor de Unity
        private void OnDrawGizmos()
        {
            if (showGizmos && detectorCollider != null)
            {
                Gizmos.color = groundRaycastColor;
                Gizmos.DrawRay(detectorCollider.bounds.center, Vector2.down * groundRaycastLength);
                Gizmos.color = colliderColor;
                Gizmos.DrawCube(detectorCollider.bounds.center, detectorCollider.bounds.size);
            }
        }
    }
}