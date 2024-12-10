using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fábrica de estados que proporciona instancias de estados específicos para un agente.
/// </summary>
public class StateFactory : MonoBehaviour
{
    /// <summary>
    /// Referencias a los estados posibles del agente. Estas deben configurarse en el inspector de Unity.
    /// </summary>
    [SerializeField] private State _idle, _move, _jump, _fall, _climbing, _attack, _getHit, _die;

    /// <summary>
    /// Obtiene una instancia del estado solicitado según el tipo de estado especificado.
    /// </summary>
    /// <param name="stateType">El tipo de estado que se desea obtener.</param>
    /// <returns>El estado correspondiente al tipo solicitado.</returns>
    /// <exception cref="System.Exception">Lanza una excepción si el tipo de estado no está definido.</exception>
    public State GetState(StateType stateType) => stateType switch  
    {
        StateType.Idle => _idle,            // Devuelve el estado "Idle" (reposo).
        StateType.Move => _move,            // Devuelve el estado "Move" (movimiento).
        StateType.Jump => _jump,            // Devuelve el estado "Jump" (salto).
        StateType.Fall => _fall,            // Devuelve el estado "Fall" (caída).
        StateType.Climbing => _climbing,    // Devuelve el estado "Climbing" (escalada).
        StateType.Attack => _attack,        // Devuelve el estado "Attack" (ataque).
        StateType.GetHit => _getHit,
        StateType.Die => _die,
        _ => throw new System.Exception($"State not defined: {stateType.ToString()}") // Excepción para estados no definidos.
    };

    /// <summary>
    /// Inicializa todos los estados asociados al agente.
    /// </summary>
    /// <param name="agent">El agente para el que se inicializan los estados.</param>
    public void InitializeStates(Agent agent)
    {
        // Obtiene todos los componentes que heredan de la clase "State" asociados a este objeto.
        State[] states = GetComponents<State>();

        // Inicializa cada estado pasando como referencia al agente.
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}


/// <summary>
/// Enumera los tipos de estados que un agente puede tener.
/// </summary>
public enum StateType
{
    Idle,      // Estado de reposo.
    Move,      // Estado de movimiento.
    Jump,      // Estado de salto.
    Fall,      // Estado de caída.
    Climbing,  // Estado de escalada.
    Attack,     // Estado de ataque.
    GetHit,     // Estado de ser golpeado
    Die         // Estado de muerte del jugador
}
