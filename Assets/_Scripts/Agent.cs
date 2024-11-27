using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;

// Clase principal que controla las funciones básicas de un agente (personaje) en Unity.
public class Agent : MonoBehaviour
{
    // Referencia al Rigidbody2D para manejar las físicas del agente.
    public Rigidbody2D rb;

    // Referencia al sistema de entrada del jugador.
    public PlayerInput agentInput;

    // Referencia al gestor de animaciones del agente.
    public AgentAnimation animationManager;

    // Referencia al gestor de renderizado del agente, que maneja su orientación visual.
    public AgentRenderer agentRenderer;

    // Estado actual del agente, y estado anterior.
    public State currentState = null, previousState = null;

    // Estado de reposo por defecto.
    public State IdleState;

    // Variable para depurar el nombre del estado actual en el Inspector.
    [Header("State debugging:")] public string stateName = "";

    private void Awake()
    {
        // Inicializa las referencias necesarias al iniciar el objeto.
        agentInput = GetComponentInParent<PlayerInput>(); // Busca PlayerInput en el padre del objeto.
        rb = GetComponent<Rigidbody2D>(); // Obtiene el Rigidbody2D del objeto actual.
        animationManager = GetComponentInChildren<AgentAnimation>(); // Busca AgentAnimation en un hijo.
        agentRenderer = GetComponentInChildren<AgentRenderer>(); // Busca AgentRenderer en un hijo.
        IdleState = GetComponentInChildren<IdleState>();// Buscamos el IdleState en un hijo.

        // Obtiene todos los estados hijos de este agente y los inicializa.
        State[] states = GetComponentsInChildren<State>();
        foreach (var state in states)
        {
            state.InitializeState(this); // Pasa la referencia del agente a cada estado.
        }
    }

    private void Start()
    {
        // Suscribe métodos a eventos definidos en PlayerInput.
        agentInput.OnMovement += agentRenderer.FaceDirection; // Ajusta la dirección visual del agente.
        TransitionToState(IdleState); // Transición inicial al estado "Idle".
    }

    // Método para realizar la transición entre estados.
    internal void TransitionToState(State targetState)
    {
        if (targetState == null) return; // Si el estado objetivo es nulo, no hace nada.

        // Llama al método Exit() del estado actual, si existe.
        if (currentState != null)
        {
            currentState.Exit();
        }

        // Actualiza los estados.
        previousState = currentState; // Guarda el estado actual como anterior.
        currentState = targetState; // Actualiza el estado actual.
        currentState.Enter(); // Llama al método Enter() del nuevo estado.

        DisplayState(); // Actualiza el nombre del estado para depuración.
    }

    // Método para mostrar el nombre del estado actual (usado para depuración).
    private void DisplayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString(); // Guarda el nombre del tipo de estado.
        }
    }

    private void Update()
    {
        // Actualiza el estado actual en cada frame.
        currentState.StateUpdate();
    }

    private void FixedUpdate()
    {
        // Actualiza el estado actual en intervalos fijos (usado para cálculos de físicas).
        currentState.StateFixedUpdate();
    }
}