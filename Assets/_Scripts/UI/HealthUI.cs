using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la visualización de los elementos de salud del jugador.
/// </summary>
public class HealthUI : MonoBehaviour
{
    // Lista de elementos visuales que representan la salud del jugador.
    private List<LifeElementUI> _healthImages;

    [SerializeField] private Sprite _fullHealth, _emptyHealth; // Sprites para vida llena y vacía.
    [SerializeField] private LifeElementUI _healthPrefab;      // Prefab para crear elementos de vida.

    /// <summary>
    /// Inicializa la interfaz de salud con la cantidad máxima de vida.
    /// </summary>
    /// <param name="maxHealth">La cantidad máxima de vida.</param>
    public void Initialize(int maxHealth)
    {
        _healthImages = new List<LifeElementUI>(); // Inicializa la lista de elementos de vida.

        // Elimina todos los elementos existentes en el contenedor.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Crea elementos visuales para cada punto de vida máximo.
        for (int i = 0; i < maxHealth; i++)
        {
            var life = Instantiate(_healthPrefab); // Instancia un nuevo elemento de vida.
            life.transform.SetParent(transform, false); // Lo agrega al contenedor actual.
            _healthImages.Add(life); // Lo añade a la lista de imágenes de vida.
        }
    }

    /// <summary>
    /// Actualiza la visualización de la salud actual.
    /// </summary>
    /// <param name="currentHealth">La salud actual del jugador.</param>
    public void SetHealth(int currentHealth)
    {
        // Itera sobre cada elemento de vida y actualiza su sprite.
        for (int i = 0; i < _healthImages.Count; i++)
        {
            if (i < currentHealth)
            {
                _healthImages[i].SetSprite(_fullHealth); // Muestra como lleno si está dentro del rango de vida actual.
            }
            else
            {
                _healthImages[i].SetSprite(_emptyHealth); // Muestra como vacío si está fuera del rango.
            }
        }
    }
}
