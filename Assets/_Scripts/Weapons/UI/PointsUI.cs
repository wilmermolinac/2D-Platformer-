using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controla la visualización de los puntos del jugador en la interfaz.
/// </summary>
public class PointsUI : MonoBehaviour
{
    private TextMeshProUGUI _pointsText; // Referencia al texto de la UI que muestra los puntos.

    public UnityEvent OnTextChange; // Evento que se activa cuando el texto cambia.

    /// <summary>
    /// Inicializa el componente al despertar.
    /// </summary>
    private void Awake()
    {
        _pointsText = GetComponentInChildren<TextMeshProUGUI>(); // Busca el componente de texto en los hijos.
    }

    /// <summary>
    /// Establece un valor inicial para los puntos.
    /// </summary>
    private void Start()
    {
        SetPoints(99); // Asigna un valor inicial de 99 puntos.
    }

    /// <summary>
    /// Actualiza el texto de los puntos y dispara el evento OnTextChange.
    /// </summary>
    /// <param name="value">El nuevo valor de los puntos.</param>
    public void SetPoints(int value)
    {
        _pointsText.SetText(value.ToString()); // Actualiza el texto con el valor proporcionado.
        OnTextChange?.Invoke(); // Dispara el evento de cambio de texto.
    }
}
