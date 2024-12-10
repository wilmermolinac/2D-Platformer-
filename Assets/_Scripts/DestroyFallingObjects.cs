using System;
using System.Collections;
using System.Collections.Generic;
using RespawnSystem;
using UnityEngine;

/// <summary>
/// Detecta y elimina objetos que caen dentro de un área específica.
/// </summary>
public class DestroyFallingObjects : MonoBehaviour
{
    public LayerMask objectToDestroyLayerMask; // Máscara de capas para los objetos que deben eliminarse.
    public Vector2 size; // Tamaño del área de detección.

    [Header("Gizmos parameters")]
    public Color gizmoColor = Color.red; // Color del Gizmo que muestra el área en el editor.

    public bool showGizmo = true; // Controla si el Gizmo se muestra en el editor.

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

            // Obtenemos el componente Damagable
            var damagable = agent.GetComponent<Damagable>();
            // si es diferente de nulo
            if (damagable != null)
            {                                                                                                                    
                // Descontamos una vida
                damagable.GetHit(1);
                
                // Si la vida actual es igual a 0 y si agente collisionado es un Player
                if (damagable.CurrentHealth == 0 && agent.CompareTag("Player"))
                {
                    // Obtenemos el componente RespawnHelper en el player
                    // Reaparecemos el player en la ultimo RespawnPoint
                    agent.GetComponent<RespawnHelper>().RespawnPlayer();
                }
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