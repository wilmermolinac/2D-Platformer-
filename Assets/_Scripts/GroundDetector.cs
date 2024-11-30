using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

/// <summary>
/// Clase que detecta si el agente está en contacto con el suelo utilizando un BoxCast.
/// </summary>
public class GroundDetector : MonoBehaviour
{
    // Referencia al Collider2D del agente para calcular el área donde se verifica el contacto con el suelo.
    public Collider2D agentCollider;

    // Máscara de capa para identificar qué objetos se consideran suelo.
    public LayerMask groundMask;

    // Indica si el agente está tocando el suelo o no.
    public bool isGrounded = false;

    // Parámetros visuales para el editor.
    [Header("Gizmos parameters:")]

    // Desplazamiento vertical del BoxCast desde el centro del Collider del agente.
    [Range(-2f, 2f)] public float boxCastYOffSet = -0.1f;

    // Desplazamiento horizontal del BoxCast desde el centro del Collider del agente.
    [Range(-2f, 2f)] public float boxCastXOffSet = -0.1f;

    // Anchura y altura del BoxCast que se usará para detectar el suelo.
    [Range(0, 2)] public float boxCastWidth = 1, boxCastHeight = 1;

    // Colores para los Gizmos en el editor, dependiendo de si el agente está o no en el suelo.
    public Color gizmoColorNotGrounded = Color.red, gizmoColorIsGrounded = Color.green;

    /// <summary>
    /// Método llamado al inicializar el objeto. Se asegura de que el agentCollider esté asignado.
    /// </summary>
    private void Awake()
    {
        // Si no se ha asignado un Collider2D en el editor, intenta obtenerlo automáticamente.
        if (agentCollider == null)
        {
            agentCollider = GetComponent<Collider2D>();
        }
    }

    /// <summary>
    /// Método que verifica si el agente está tocando el suelo utilizando un BoxCast.
    /// </summary>
    public void CheckIsGrounded()
    {
        // Realiza un BoxCast para detectar colisiones bajo el agente.
        // - Origen: centro del Collider del agente desplazado por los offsets.
        // - Tamaño: ancho y alto definidos por boxCastWidth y boxCastHeight.
        // - Rotación: 0 grados (sin rotación).
        // - Dirección: hacia abajo (Vector2.down).
        // - Distancia: 0 (solo verifica en el área inmediata).
        // - Capa: solo detecta objetos en las capas definidas por groundMask.
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            agentCollider.bounds.center + new Vector3(boxCastXOffSet, boxCastYOffSet, 0),
            new Vector2(boxCastWidth, boxCastHeight),
            0, // Rotación del BoxCast (sin rotación).
            Vector2.down, // Dirección del BoxCast (hacia abajo).
            0, // Distancia del BoxCast (verifica el área inmediata).
            groundMask // Máscara de capas a detectar (capas del suelo).
        );

        // Realiza una comprobación para determinar si el agente está en el suelo usando el resultado del BoxCast.
        if (raycastHit.collider != null) // Si el BoxCast detecta un Collider.
        {
            // Verifica si el Collider detectado está en contacto con el Collider del agente.
            if (raycastHit.collider.IsTouching(agentCollider))
            {
                // Si está en contacto, significa que el agente está en el suelo.
                isGrounded = true;
            }
        }
        else
        {
            // Si no se detecta ningún Collider, el agente no está en el suelo.
            isGrounded = false;
        }
    }


    /// <summary>
    /// Método que dibuja Gizmos en el editor para representar el área del BoxCast.
    /// </summary>
    private void OnDrawGizmos()
    {
        // Si no hay un Collider asignado, no dibuja nada.
        if (agentCollider == null)
            return;

        // Establece el color de los Gizmos dependiendo del estado del agente.
        Gizmos.color = gizmoColorNotGrounded; // Color rojo por defecto.
        if (isGrounded)
        {
            Gizmos.color = gizmoColorIsGrounded; // Cambia a verde si está en el suelo.
        }

        // Dibuja un cubo que representa el área del BoxCast.
        Gizmos.DrawWireCube(
            agentCollider.bounds.center + new Vector3(boxCastXOffSet, boxCastYOffSet, 0), // Centro desplazado.
            new Vector3(boxCastWidth, boxCastHeight) // Tamaño del cubo.
        );
    }
}
