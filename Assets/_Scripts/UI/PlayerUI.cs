using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador de la interfaz de usuario del jugador que maneja la salud y los puntos.
/// </summary>
public class PlayerUI : MonoBehaviour
{
    // Referencia a los componentes UI que muestran la salud y los puntos del jugador.
    private HealthUI _healthUI;
    private PointsUI _pointsUI;

    /// <summary>
    /// Método llamado al inicializar el objeto. Busca los componentes de salud y puntos dentro de los hijos del GameObject.
    /// </summary>
    private void Awake()
    {
        _healthUI = GetComponentInChildren<HealthUI>(); // Obtiene el componente HealthUI dentro de los hijos.
        _pointsUI = GetComponentInChildren<PointsUI>(); // Obtiene el componente PointsUI dentro de los hijos.
    }

    /// <summary>
    /// Configura la salud máxima en el componente HealthUI.
    /// </summary>
    /// <param name="maxHealth">La cantidad máxima de salud.</param>
    public void InitializeMaxHealth(int maxHealth)
    {
        _healthUI.Initialize(maxHealth);
    }

    /// <summary>
    /// Actualiza la salud actual en el componente HealthUI.
    /// </summary>
    /// <param name="currentHealth">La salud actual del jugador.</param>
    public void SetHealth(int currentHealth)
    {
        _healthUI.SetHealth(currentHealth);
    }

    /// <summary>
    /// Actualiza los puntos actuales en el componente PointsUI.
    /// </summary>
    /// <param name="currentPoints">La cantidad actual de puntos del jugador.</param>
    public void SetPoints(int currentPoints)
    {
        _pointsUI.SetPoints(currentPoints);
    }
}
