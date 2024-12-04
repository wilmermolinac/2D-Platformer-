using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta y elimina objetos que caen dentro de un área específica.
/// </summary>
public class DestroyFallingObjects : MonoBehaviour
{
    public LayerMask objectToDestroyLayerMask; // Máscara de capas para los objetos que deben eliminarse.
    public Vector2 size;                      // Tamaño del área de detección.

    [Header("Gizmos parameters")]
    public Color gizmoColor = Color.red;      // Color del Gizmo que muestra el área en el editor.
    public bool showGizmo = true;             // Controla si el Gizmo se muestra en el editor.

    /// <summary>
    /// Método llamado en cada FixedUpdate para detectar y eliminar objetos.
    /// </summary>
    private void FixedUpdate()
    {
        // Verifica si hay un collider dentro del área definida.
        Collider2D collider = Physics2D.OverlapBox(transform.position, size, 0, objectToDestroyLayerMask);

        if (collider != null)
        {
            // Intenta obtener el componente Agent del objeto.
            Agent agent = collider.GetComponent<Agent>();

            if (agent == null)
            {
                // Si el objeto no es un agente, se destruye.
                Destroy(collider.gameObject);
                return;
            }

            // Si es un agente, llama al método que maneja su destrucción.
            agent.AgentDied();
        }
    }

    /// <summary>
    /// Dibuja un Gizmo en el editor para visualizar el área de detección.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = gizmoColor; // Establece el color del Gizmo.
            Gizmos.DrawCube(transform.position, size); // Dibuja el área como un cubo.
        }
    }
}
