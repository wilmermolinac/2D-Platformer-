using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    // Clase PlayerInput que maneja la entrada del jugador para realizar acciones en el juego.
    // Hereda de MonoBehaviour, lo que le permite asociarse a un GameObject en Unity.

    // Campo público con encapsulamiento automático para almacenar el vector de movimiento del jugador.
    // El atributo [SerializeField] permite que esta propiedad sea visible y editable desde el inspector de Unity,
    // pero sigue siendo privada para modificarla directamente desde fuera de esta clase.
    [field: SerializeField] public Vector2 MovementVector { get; private set; }

    // Eventos que se activan cuando ocurren acciones específicas, permitiendo que otros scripts reaccionen a ellas.
    public event Action OnAttack;           // Se activa cuando el jugador realiza un ataque.
    public event Action OnJumpPressed;     // Se activa cuando el jugador presiona el botón de salto.
    public event Action OnJumpReleased;    // Se activa cuando el jugador suelta el botón de salto.
    public event Action OnWeaponChange;    // Se activa cuando el jugador cambia de arma.

    // Evento con un parámetro (Vector2) para notificar cambios en el movimiento.
    public event Action<Vector2> OnMovement;

    // Teclas configurables para las acciones de salto, ataque y menú.
    public KeyCode jumpKey, attackKey, menuKey;

    // Evento de Unity para manejar acciones relacionadas con el menú. 
    // UnityEvent es útil para asignar funciones desde el editor sin necesidad de programación adicional.
    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        // Método Update: se ejecuta una vez por frame.

        // Comprueba si el tiempo del juego está en marcha (timeScale > 0). Esto evita que las entradas se procesen si el juego está pausado.
        if (Time.timeScale > 0)
        {
            // Llama a los métodos para manejar las diferentes entradas del jugador.
            GetMovementInput();    // Obtiene la entrada de movimiento.
            GetJumpInput();        // Obtiene la entrada de salto.
            GetAttackInput();      // Obtiene la entrada de ataque.
            GetWeaponSwapInput();  // Obtiene la entrada para cambiar de arma.
        }

        // La entrada para el menú se maneja incluso si el juego está pausado.
        GetMenuInput();
    }

    private void GetMenuInput()
    {
        // Comprueba si la tecla asignada al menú (menuKey) ha sido presionada.
        if (Input.GetKeyDown(menuKey))
        {
            // Si el evento OnMenuKeyPressed tiene suscriptores, los invoca.
            OnMenuKeyPressed?.Invoke();
        }
    }

    private void GetWeaponSwapInput()
    {
        // Comprueba si se ha presionado la tecla "E", usada para cambiar de arma.
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Invoca el evento OnWeaponChange si tiene suscriptores.
            OnWeaponChange?.Invoke();
        }
    }

    private void GetAttackInput()
    {
        // Comprueba si se ha presionado la tecla asignada al ataque (attackKey).
        if (Input.GetKeyDown(attackKey))
        {
            // Invoca el evento OnAttack si tiene suscriptores.
            OnAttack?.Invoke();
        }
    }

    private void GetJumpInput()
    {
        // Comprueba si se ha presionado la tecla asignada al salto (jumpKey).
        if (Input.GetKeyDown(jumpKey))
        {
            // Invoca el evento OnJumpPressed si tiene suscriptores.
            OnJumpPressed?.Invoke();
        }

        // Comprueba si se ha soltado la tecla asignada al salto.
        if (Input.GetKeyUp(jumpKey))
        {
            // Invoca el evento OnJumpReleased si tiene suscriptores.
            OnJumpReleased?.Invoke();
        }
    }

    private void GetMovementInput()
    {
        // Obtiene el vector de movimiento calculado por GetMovementVector().
        MovementVector = GetMovementVector();

        // Invoca el evento OnMovement y pasa el vector de movimiento como parámetro, si hay suscriptores.
        OnMovement?.Invoke(MovementVector);
    }

    protected Vector2 GetMovementVector()
    {
        // Crea un vector de movimiento basado en las entradas horizontales y verticales del jugador.
        // Usa "Input.GetAxisRaw" para obtener valores discretos (-1, 0, 1) que representan las direcciones.
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
