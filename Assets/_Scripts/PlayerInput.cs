using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Clase encargada de gestionar la entrada del jugador y emitir eventos relacionados con las acciones del juego.
/// Hereda de MonoBehaviour, lo que permite asociarla a un GameObject en Unity.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // **Movimiento**
    /// <summary>
    /// Vector que representa la dirección de movimiento del jugador.
    /// Se define como público con acceso de solo lectura desde fuera de la clase.
    /// </summary>
    [field: SerializeField] public Vector2 MovementVector { get; private set; }

    // Eventos que se activan cuando ocurren acciones específicas, permitiendo que otros scripts reaccionen a ellas.
    // **Eventos de acciones del jugador**
    /// <summary>Se activa cuando el jugador realiza un ataque.</summary>
    public event Action OnAttack;

    /// <summary>Se activa cuando el jugador presiona el botón de salto.</summary>
    public event Action OnJumpPressed;

    /// <summary>Se activa cuando el jugador suelta el botón de salto.</summary>
    public event Action OnJumpReleased;

    /// <summary>Se activa cuando el jugador cambia de arma.</summary>
    public event Action OnWeaponChange;

    /// <summary>Notifica cambios en el movimiento del jugador, pasando el vector de movimiento como parámetro.</summary>
    public event Action<Vector2> OnMovement;

    // **Teclas de entrada**
    /// <summary>Tecla asignada para realizar la acción de salto.</summary>
    public KeyCode jumpKey;

    /// <summary>Tecla asignada para realizar la acción de ataque.</summary>
    public KeyCode attackKey;

    /// <summary>Tecla asignada para abrir el menú.</summary>
    public KeyCode menuKey;

    // **Evento del menú**
    /// <summary>
    /// Evento invocado cuando se presiona la tecla del menú. Puede configurarse directamente en el editor de Unity.
    /// </summary>
    public UnityEvent OnMenuKeyPressed;

    /// <summary>
    /// Método llamado cada frame para manejar las entradas del jugador.
    /// </summary>
    private void Update()
    {
        // Comprueba si el tiempo del juego está activo. Si el juego está pausado (timeScale = 0), ignora la mayoría de las entradas.
        if (Time.timeScale > 0)
        {
            // Gestiona las entradas principales del jugador.
            GetMovementInput();    // Maneja la entrada de movimiento.
            GetJumpInput();        // Maneja la entrada de salto.
            GetAttackInput();      // Maneja la entrada de ataque.
            GetWeaponSwapInput();  // Maneja la entrada para cambiar de arma.
        }

        // La entrada para el menú siempre se procesa, incluso si el juego está pausado.
        GetMenuInput();
    }

    /// <summary>
    /// Detecta si el jugador ha presionado la tecla asignada al menú y activa el evento correspondiente.
    /// </summary>
    private void GetMenuInput()
    {
        // Comprueba si la tecla asignada al menú fue presionada.
        if (Input.GetKeyDown(menuKey))
        {
            // Si hay suscriptores al evento, los invoca.
            OnMenuKeyPressed?.Invoke();
        }
    }

    /// <summary>
    /// Detecta si el jugador quiere cambiar de arma y activa el evento correspondiente.
    /// </summary>
    private void GetWeaponSwapInput()
    {
        // Comprueba si se presionó la tecla "E" para cambiar de arma.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Invoca el evento OnWeaponChange si hay suscriptores.
            OnWeaponChange?.Invoke();
        }
    }

    /// <summary>
    /// Detecta si el jugador ha realizado un ataque y activa el evento correspondiente.
    /// </summary>
    private void GetAttackInput()
    {
        // Comprueba si se presionó la tecla asignada al ataque.
        if (Input.GetKeyDown(attackKey))
        {
            // Invoca el evento OnAttack si hay suscriptores.
            OnAttack?.Invoke();
        }
    }

    /// <summary>
    /// Detecta si el jugador ha presionado o soltado la tecla de salto y activa los eventos correspondientes.
    /// </summary>
    private void GetJumpInput()
    {
        // Comprueba si se presionó la tecla asignada al salto.
        if (Input.GetKeyDown(jumpKey))
        {
            // Invoca el evento OnJumpPressed si hay suscriptores.
            OnJumpPressed?.Invoke();
        }

        // Comprueba si se soltó la tecla asignada al salto.
        if (Input.GetKeyUp(jumpKey))
        {
            // Invoca el evento OnJumpReleased si hay suscriptores.
            OnJumpReleased?.Invoke();
        }
    }

    /// <summary>
    /// Detecta la entrada de movimiento del jugador y notifica los cambios en el vector de movimiento.
    /// </summary>
    private void GetMovementInput()
    {
        // Obtiene el vector de movimiento calculado por GetMovementVector().
        MovementVector = GetMovementVector();

        // Invoca el evento OnMovement pasando el vector de movimiento, si hay suscriptores.
        OnMovement?.Invoke(MovementVector);
    }

    /// <summary>
    /// Calcula el vector de movimiento del jugador en función de las teclas de entrada.
    /// </summary>
    /// <returns>Un Vector2 que representa la dirección de movimiento.</returns>
    protected Vector2 GetMovementVector()
    {
        // Obtiene las entradas horizontales y verticales del jugador.
        // "Input.GetAxisRaw" devuelve valores discretos (-1, 0, 1) ideales para movimientos binarios.
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
