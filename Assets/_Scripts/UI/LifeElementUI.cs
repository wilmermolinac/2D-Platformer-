using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Representa un elemento de vida en la interfaz de usuario, como un corazón.
/// Permite cambiar el sprite según el estado (lleno o vacío).
/// </summary>
public class LifeElementUI : MonoBehaviour
{
    // Referencia al componente Image usado para mostrar el sprite.
    private Image _image;

    /// <summary>
    /// Evento que se dispara cuando el sprite cambia.
    /// </summary>
    public UnityEvent OnSpriteChange;

    /// <summary>
    /// Inicializa el componente Image al despertar.
    /// </summary>
    private void Awake()
    {
        _image = GetComponent<Image>(); // Obtiene el componente Image del objeto.
    }

    /// <summary>
    /// Cambia el sprite actual del elemento si es diferente al nuevo.
    /// </summary>
    /// <param name="sprite">El nuevo sprite que se establecerá.</param>
    public void SetSprite(Sprite sprite)
    {
        // Verifica si el sprite actual es diferente al nuevo antes de cambiarlo.
        if (_image.sprite != sprite)
        {
            OnSpriteChange?.Invoke(); // Dispara el evento de cambio de sprite.
            _image.sprite = sprite;  // Actualiza el sprite del componente Image.
        }
    }
}
