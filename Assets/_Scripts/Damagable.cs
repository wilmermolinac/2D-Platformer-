using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

/// <summary>
/// Clase que representa un objeto que puede recibir daño y gestionar su salud.
/// Implementa la interfaz "IHittable" para interactuar con sistemas de daño.
/// </summary>
public class Damagable : MonoBehaviour, IHittable
{
    /// <summary>
    /// Salud máxima del objeto.
    /// Configurable desde el inspector de Unity.
    /// </summary>
    [SerializeField] private int _maxHealth;

    /// <summary>
    /// Salud actual del objeto.
    /// Configurable desde el inspector de Unity.
    /// </summary>
    [SerializeField] private int _currentHealth;

    /// <summary>
    /// Evento que se activa cuando el objeto recibe daño pero no muere.
    /// </summary>
    public UnityEvent OnGetHit;

    /// <summary>
    /// Evento que se activa cuando el objeto muere (su salud llega a cero o menos).
    /// </summary>
    public UnityEvent OnDie;

    /// <summary>
    /// Evento que se activa cuando el objeto recupera salud.
    /// </summary>
    public UnityEvent OnAddHealth;

    /// <summary>
    /// Evento que se activa cuando cambia el valor de la salud actual.
    /// Pasa como parámetro el nuevo valor de salud.
    /// </summary>
    public UnityEvent<int> OnHealthValueChange;

    /// <summary>
    /// Evento que se activa al inicializar la salud máxima.
    /// Pasa como parámetro el valor de la salud máxima inicializada.
    /// </summary>
    public UnityEvent<int> OnInitializeMaxHealth;

    /// <summary>
    /// Propiedad para obtener o establecer la salud actual.
    /// Al establecer la salud, activa el evento "OnHealthValueChange".
    /// </summary>
    public int CurrentHealth
    {
        get => _currentHealth; // Devuelve el valor actual de la salud.

        set
        {
            _currentHealth = value; // Establece el nuevo valor de salud.
            OnHealthValueChange?.Invoke(_currentHealth); // Invoca el evento con el nuevo valor.
        }
    }

    /// <summary>
    /// Método llamado cuando el objeto es golpeado.
    /// Recibe como parámetros el objeto atacante y el daño del arma.
    /// </summary>
    /// <param name="agentGameObject">El objeto que realizó el ataque.</param>
    /// <param name="weaponDamage">El daño infligido por el arma.</param>
    public void GetHit(GameObject agentGameObject, int weaponDamage)
    {
        GetHit(weaponDamage); // Llama al método que procesa el daño.
    }

    /// <summary>
    /// Aplica daño al objeto y gestiona la lógica de salud y eventos.
    /// </summary>
    /// <param name="weaponDamage">El daño infligido al objeto.</param>
    public void GetHit(int weaponDamage)
    {
        CurrentHealth -= weaponDamage; // Reduce la salud actual por el daño recibido.
        
        if (CurrentHealth <= 0) // Si la salud llega a cero o menos.
        {
            OnDie?.Invoke(); // Invoca el evento de muerte.
        }
        else
        {
            OnGetHit?.Invoke(); // Invoca el evento de daño recibido.
        }
    }

    /// <summary>
    /// Incrementa la salud del objeto, asegurándose de no exceder los límites.
    /// </summary>
    /// <param name="value">Cantidad de salud a añadir.</param>
    public void AddHealth(int value)
    {
        // Suma la salud actual con el valor proporcionado,
        // asegurándose de que esté entre 0 y la salud máxima.
        CurrentHealth = Mathf.Clamp(_currentHealth + value, 0, _maxHealth);

        OnAddHealth?.Invoke(); // Invoca el evento de añadir salud.
    }

    /// <summary>
    /// Inicializa la salud del objeto, configurando la salud máxima y actual.
    /// </summary>
    /// <param name="health">Valor inicial de la salud máxima.</param>
    public void Initialize(int health)
    {
        _maxHealth = health; // Configura la salud máxima.
        OnInitializeMaxHealth?.Invoke(_maxHealth); // Invoca el evento de inicialización de salud máxima.
        CurrentHealth = _maxHealth; // Establece la salud actual como la salud máxima.
    }
}
