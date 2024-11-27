using UnityEngine;
using UnityEngine.Events;

// Clase base abstracta para los estados del agente.
// Las clases abstractas obligan a las clases derivadas a implementar los métodos abstractos de la clase padre.
// Podemos sobreescribir y proporcionar una implementación a los métodos abstractos.
// También podemos sobrescribir métodos virtuales si es necesario.
// Si tenemos métodos protected virtual, no es obligatorio implementarlos en las clases derivadas.
public abstract class State : MonoBehaviour
{
    protected Agent agent; // Referencia al agente que utiliza este estado.

    // Eventos que se invocan al entrar y salir del estado.
    public UnityEvent OnEnter, OnExit;

    // Inicializa el estado con una referencia al agente.
    public void InitializeState(Agent agent)
    {
        this.agent = agent;
    }

    // Método que se llama al entrar al estado.
    public void Enter()
    {
        // Suscribe métodos a eventos del sistema de entrada del agente.
        this.agent.agentInput.OnAttack += HandleAttack;
        this.agent.agentInput.OnJumpPressed += HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased += HandleJumpReleased;
        this.agent.agentInput.OnMovement += HandleMovement;

        // Invoca el evento OnEnter, si está configurado.
        OnEnter?.Invoke();

        EnterState(); // Llama a la lógica específica del estado.
    }

    // Métodos protegidos para que los estados hijos implementen lógica específica.
    protected virtual void EnterState()
    {
        
    }

    protected virtual void HandleMovement(Vector2 input)
    {
        
    }
    protected virtual void HandleJumpReleased() { }
    protected virtual void HandleJumpPressed() { }
    protected virtual void HandleAttack() { }

    // Métodos para actualizar la lógica del estado.
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }

    // Método que se llama al salir del estado.
    public void Exit()
    {
        // Elimina la suscripción de eventos.
        this.agent.agentInput.OnAttack -= HandleAttack;
        this.agent.agentInput.OnJumpPressed -= HandleJumpPressed;
        this.agent.agentInput.OnJumpReleased -= HandleJumpReleased;
        this.agent.agentInput.OnMovement -= HandleMovement;

        // Invoca el evento OnExit, si está configurado.
        OnExit?.Invoke();

        ExitState(); // Llama a la lógica específica al salir del estado.
    }

    protected virtual void ExitState()
    {
        
    }
}