using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [field:SerializeField]
    public Vector2 MovementVector { get; private set; }

    public event Action OnAttack, OnJumpPressed, OnJumReased, OnWeaponChange;

    public event Action<Vector2> OnMovement;
    public KeyCode jumpKey, attackKey, menuKey;
    public UnityEvent OnMenuKeyPressed;
}
