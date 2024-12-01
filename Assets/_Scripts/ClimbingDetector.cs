using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que detecta si el personaje está en contacto con superficies escalables.
/// </summary>
public class ClimbingDetector : MonoBehaviour
{
    /// <summary>
    /// Máscara de capas que define qué objetos son escalables. 
    /// Solo los objetos en estas capas serán considerados para la detección.
    /// </summary>
    [SerializeField] private LayerMask climbingLayerMask;

    /// <summary>
    /// Bandera que indica si el personaje puede escalar actualmente.
    /// </summary>
    [SerializeField] private bool canClimb;

    /// <summary>
    /// Propiedad pública para acceder al estado de escalada. 
    /// La propiedad es de solo lectura desde fuera de la clase.
    /// </summary>
    public bool CanClimb
    {
        get { return canClimb; }
        private set { canClimb = value; } // Solo puede modificarse internamente.
    }

    /// <summary>
    /// Método llamado automáticamente por Unity cuando el personaje entra en un trigger 2D.
    /// Verifica si el objeto con el que colisionó pertenece a una capa escalable.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Convierte la capa del objeto colisionado en una máscara de capa.
        // La operación de desplazamiento de bits (1 << collision.gameObject.layer) crea
        // una máscara donde solo el bit correspondiente a la capa del objeto colisionado está activado.
        LayerMask collisionsLayerMask = 1 << collision.gameObject.layer;

        // Comprueba si la capa del objeto colisionado coincide con las capas escalables.
        // La operación de bit a bit (collisionsLayerMask & climbingLayerMask) devuelve
        // un valor distinto de cero si hay alguna coincidencia en las capas.
        if ((collisionsLayerMask & climbingLayerMask) != 0)
        {
            // Si es escalable, permite que el personaje pueda escalar.
            canClimb = true;
        }
    }

    /// <summary>
    /// Método llamado automáticamente por Unity cuando el personaje sale de un trigger 2D.
    /// Verifica si el objeto con el que dejó de colisionar pertenece a una capa escalable.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Convierte la capa del objeto colisionado en una máscara de capa.
        // Similar al método OnTriggerEnter2D, convierte la capa del objeto
        // con el que el personaje dejó de colisionar en una máscara de capa.
        LayerMask collisionsLayerMask = 1 << collision.gameObject.layer;

        // Comprueba si la capa del objeto colisionado coincide con las capas escalables.
        // Usa la misma operación de bit a bit para verificar la coincidencia.
        if ((collisionsLayerMask & climbingLayerMask) != 0)
        {
            // Si deja de estar en contacto con una superficie escalable, desactiva la escalada.
            canClimb = false;
        }
    }
}
