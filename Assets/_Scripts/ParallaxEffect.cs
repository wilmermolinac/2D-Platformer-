using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase encargada de aplicar un efecto de parallax a un objeto, moviéndolo a una velocidad relativa
/// en función de la posición de la cámara principal.
/// </summary>
public class ParallaxEffect : MonoBehaviour
{
    /// <summary>
    /// Referencia a la cámara principal. Se utiliza para calcular la posición del efecto de parallax.
    /// </summary>
    public Camera mainCamera;

    /// <summary>
    /// Velocidad relativa del movimiento del parallax. 
    /// Un valor más cercano a 0 hace que el objeto se mueva más lentamente respecto a la cámara,
    /// simulando una mayor distancia del fondo.
    /// El atributo [Range(0, 1)] limita el valor en el inspector entre 0 y 1.
    /// </summary>
    [Range(0, 1)] public float movementSpeed = 0.3f;

    /// <summary>
    /// Método llamado cuando el objeto se inicializa en la escena. 
    /// Garantiza que la referencia a la cámara principal esté configurada.
    /// </summary>
    private void Awake()
    {
        // Comprueba si `mainCamera` no está configurada manualmente en el inspector.
        // Si está vacía, se asigna automáticamente a la cámara principal de la escena mediante `Camera.main`.
        mainCamera = mainCamera == null ? Camera.main : mainCamera;
    }

    /// <summary>
    /// Método llamado a intervalos fijos, ideal para cálculos relacionados con físicas o actualizaciones constantes.
    /// Mueve el objeto en función de la posición de la cámara, creando el efecto de parallax.
    /// </summary>
    private void FixedUpdate()
    {
        // Calcula la nueva posición del objeto. 
        // Solo el eje X es afectado, multiplicando la posición X de la cámara por `movementSpeed`.
        // Esto crea la ilusión de profundidad, donde los objetos más lejanos se mueven más lentamente.
        transform.position = new Vector2(mainCamera.transform.position.x * movementSpeed, 0);
    }
}
