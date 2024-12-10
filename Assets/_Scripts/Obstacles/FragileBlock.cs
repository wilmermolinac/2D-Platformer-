using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

/// <summary>
/// Representa un bloque frágil que puede ser destruido al recibir un impacto.
/// </summary>
public class FragileBlock : MonoBehaviour, IHittable
{
    /// <summary>
    /// Evento que se dispara al impactar el bloque.
    /// </summary>
    public UnityEvent OnHit;

    /// <summary>
    /// Referencia al componente Animator del bloque.
    /// </summary>
    private Animator _anim;

    /// <summary>
    /// Referencia al componente Collider2D del bloque.
    /// </summary>
    private Collider2D _collider;

    /// <summary>
    /// Método llamado al inicializar el objeto.
    /// </summary>
    private void Awake()
    {
        // Obtiene las referencias al Animator y al Collider2D.
        _anim = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Maneja el impacto en el bloque.
    /// </summary>
    /// <param name="agentGameObject">El objeto del agente que realizó el ataque.</param>
    /// <param name="weaponDamage">El daño causado por el arma.</param>
    public void GetHit(GameObject agentGameObject, int weaponDamage)
    {
        // Dispara el evento OnHit.
        OnHit?.Invoke();

        // Reproduce la animación de impacto.
        _anim.SetTrigger("Attack");

        // Desactiva el colisionador del bloque.
        _collider.enabled = false;
    }

    /// <summary>
    /// Destruye el bloque eliminándolo de la escena.
    /// </summary>
    public void DestroySelf()
    {
        // Destruye el objeto del bloque.
        Destroy(gameObject);
    }
}
