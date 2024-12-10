using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define una interfaz para objetos que pueden ser impactados.
/// </summary>
namespace WeaponSystem
{
    public interface IHittable
    {
        /// <summary>
        /// Maneja el impacto en el objeto.
        /// </summary>
        /// <param name="agentGameObject">El objeto del agente que realizó el ataque.</param>
        /// <param name="weaponDamage">El daño causado por el arma.</param>
        void GetHit(GameObject agentGameObject, int weaponDamage);
    }
}
