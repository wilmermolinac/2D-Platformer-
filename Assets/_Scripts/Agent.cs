using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Agent : MonoBehaviour
{
    // Referencia al componente Rigidbody2D para manejar la física del agente.
    public Rigidbody2D rb;

    // Referencia al componente PlayerInput, que captura las entradas del jugador.
    public PlayerInput playerInput;

    // Referencia al administrador de animaciones del agente.
    public AgentAnimation animationManager;

    // Referencia al renderizador del agente, encargado de manejar la dirección visual del agente.
    public AgentRenderer agentRenderer;

    private void Awake()
    {
        // Awake es llamado cuando se instancia el objeto y es ideal para inicializar referencias.
        
        // Obtiene el componente PlayerInput del objeto padre. Esto permite recibir las entradas del jugador.
        playerInput = GetComponentInParent<PlayerInput>();

        // Obtiene el componente Rigidbody2D en el mismo objeto. Esto es necesario para manipular el movimiento del agente.
        rb = GetComponent<Rigidbody2D>();

        // Obtiene el componente AgentAnimation en un objeto hijo. Este componente manejará las animaciones.
        animationManager = GetComponentInChildren<AgentAnimation>();

        // Obtiene el componente AgentRenderer en un objeto hijo. Este se encarga de ajustar la dirección visual del agente.
        agentRenderer = GetComponentInChildren<AgentRenderer>();
    }

    private void Start()
    {
        // Start se llama después de Awake y es ideal para conectar eventos.

        // Suscribe el método HandleMovement al evento OnMovement del PlayerInput.
        // Esto permite que el agente reaccione a los movimientos del jugador.
        playerInput.OnMovement += HandleMovement;

        // También suscribe el método FaceDirection del AgentRenderer al mismo evento.
        // Esto asegura que el agente gire visualmente en la dirección del movimiento.
        playerInput.OnMovement += agentRenderer.FaceDirection;
    }

    private void HandleMovement(Vector2 input)
    {
        // Este método maneja el movimiento del agente basado en la entrada del jugador.

        // Comprueba si hay movimiento horizontal (input.x != 0).
        if (Mathf.Abs(input.x) > 0)
        {
            // Si el agente está casi detenido (velocidad horizontal cercana a 0), inicia la animación de correr.
            if (Mathf.Abs(rb.velocity.x) < 0.01f)
            {
                animationManager.PlayAnimation(AnimationType.run);
            }

            // Ajusta la velocidad del Rigidbody2D para mover al agente en la dirección del input.x.
            rb.velocity = new Vector2(input.x * 5, rb.velocity.y);
        }
        else
        {
            // Si no hay movimiento horizontal, pero el agente tiene velocidad, cambia la animación a "idle".
            if (Mathf.Abs(rb.velocity.x) > 0f)
            {
                animationManager.PlayAnimation(AnimationType.idle);
            }

            // Detiene el movimiento horizontal del agente.
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
