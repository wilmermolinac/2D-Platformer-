using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using WeaponSystem; // Hacemos la importaccion del namespace WeaponSystem para usar sus clases

/// <summary>
/// Clase que controla las funciones básicas de un agente (personaje) en Unity.
/// Actúa como núcleo del sistema de estados, manejando transiciones, entrada de jugador, físicas, y animaciones.
/// </summary>
public class Agent : MonoBehaviour
{
    // **Datos compartidos**
    // Referencia al ScriptableObject que almacena datos compartidos entre los estados del agente.
    public AgentDataSO agentData;

    // **Físicas**
    // Referencia al Rigidbody2D del agente para manejar movimientos y fuerzas físicas.
    public Rigidbody2D rb;

    // **Entrada**
    // Hacemos referencia a IAgentInput inteface de nuestro Sistema que gestiona los eventos de la entrada del jugador (movimiento, salto, ataque, etc.).
    public IAAgentInput iaAgentInput;

    // **Animaciones**
    // Gestor que maneja las animaciones del agente.
    public AgentAnimation animationManager;

    // **Renderizado**
    // Gestor responsable de cambiar la orientación visual del agente según la dirección de movimiento.
    public AgentRenderer agentRenderer;

    // **Detección de suelo**
    // Clase que detecta si el agente está en contacto con el suelo.
    public GroundDetector groundDetector;

    // **Detector de escaleras**
    // Hacemos la referencia al detector de escaleras.
    public ClimbingDetector climbingDetector;

    // **Estados**
    // Estado actual del agente y estado previo.
    public State currentState = null, previousState = null;

    // Estado inicial por defecto: reposo (Idle).
    [SerializeField] private State _idleState;

    // Hacemos referencia al AgentWeaponManager
    [HideInInspector] public AgentWeaponManager agentWeaponManager;

    [SerializeField] public StateFactory stateFactory;

    private Damagable _damagable;

    // **Depuración**
    // Almacena el nombre del estado actual para facilitar la depuración en el Inspector.
    [Header("State debugging:")] public string stateName = "";
    
    // Evento que se llama cuando vamos a reaparecer
    [field: SerializeField] 
    private UnityEvent OnRespawnRequired { get; set; }
    
    [field:SerializeField]
    public UnityEvent OnAgentDie { get; set; }
    
    /// <summary>
    /// Método llamado al inicializar el objeto. Configura referencias necesarias.
    /// </summary>
    private void Awake()
    {
        // **Inicialización de referencias**
        // Obtiene el sistema de entrada (IAgentInput) del padre del objeto actual.
        iaAgentInput = GetComponentInParent<IAAgentInput>();

        // Obtiene el Rigidbody2D del objeto actual para manejar las físicas.
        rb = GetComponent<Rigidbody2D>();

        // Obtiene el gestor de animaciones desde un hijo del agente.
        animationManager = GetComponentInChildren<AgentAnimation>();

        // Obtiene el gestor de renderizado desde un hijo del agente.
        agentRenderer = GetComponentInChildren<AgentRenderer>();

        // Obtiene el detector de suelo desde un hijo del agente.
        groundDetector = GetComponentInChildren<GroundDetector>();

        // Obtiene  y inicializa el detector de escaleras desde un hijo de agente
        climbingDetector = GetComponentInChildren<ClimbingDetector>();

        // Busca y asigna el estado de reposo (IdleState) desde un hijo del agente.
        _idleState = GetComponentInChildren<IdleState>();

        // Iniclializamos
        agentWeaponManager = GetComponentInChildren<AgentWeaponManager>();

        // Inicializamos
        stateFactory = GetComponentInChildren<StateFactory>();

        // Inicializamos 
        _damagable = GetComponent<Damagable>();

        // Inicializamos todos los States
        stateFactory.InitializeStates(this);
    }

    /// <summary>
    /// Método llamado al inicio del juego. Configura eventos iniciales y el estado por defecto.
    /// </summary>
    private void Start() 
    {
        // **Suscripción de eventos**
        // Ajusta la dirección visual del agente según el movimiento del jugador.
        iaAgentInput.OnMovement += agentRenderer.FaceDirection;

        // Inicializamos algunos datos
        InitializeAgent();
    }

    private void InitializeAgent()
    {
        // Transición inicial al estado de reposo (Idle).
        TransitionToState(_idleState);
        _damagable.Initialize(agentData.health);
    }

    /// <summary>
    /// Realiza la transición entre estados del agente.
    /// </summary>
    /// <param name="targetState">El estado al que se quiere transitar.</param>
    internal void TransitionToState(State targetState)
    {
        // Si el estado objetivo es nulo, no realiza la transición.
        if (targetState == null) return;

        // Llama al método Exit() del estado actual, si existe.
        if (currentState != null)
        {
            currentState.Exit();
        }

        // **Actualización de estados**
        // Guarda el estado actual como el estado anterior.
        previousState = currentState;

        // Actualiza el estado actual al nuevo estado.
        currentState = targetState;

        // Llama al método Enter() del nuevo estado.
        currentState.Enter();

        // Actualiza el nombre del estado para fines de depuración.
        DisplayState();
    }

    /// <summary>
    /// Actualiza el nombre del estado actual para fines de depuración.
    /// </summary>
    private void DisplayState()
    {
        // Solo actualiza el nombre si el estado actual es diferente al estado anterior.
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString(); // Guarda el nombre del tipo del estado.
        }
    }

    /// <summary>
    /// Método llamado cada frame. Actualiza la lógica del estado actual.
    /// </summary>
    private void Update()
    {
        currentState.StateUpdate();
    }

    /// <summary>
    /// Método llamado en intervalos fijos. Actualiza la lógica física del estado actual.
    /// </summary>
    private void FixedUpdate()
    {
        // Comprueba si el agente está tocando el suelo.
        groundDetector.CheckIsGrounded();

        // Llama a la lógica física específica del estado actual.
        currentState.StateFixedUpdate();
    }

    public void AgentDied()
    {
        if (_damagable.CurrentHealth > 0)
        {
            OnRespawnRequired?.Invoke();
        }
        else
        {
            currentState.Die();
        }
    }

    public void GetHit()
    {
        currentState.GetHit();
    }
}