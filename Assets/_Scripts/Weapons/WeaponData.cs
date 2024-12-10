using System;
using UnityEngine;

namespace WeaponSystem
{
    /// <summary>
    /// Clase base para definir datos de armas.
    /// </summary>
    public abstract class WeaponData : ScriptableObject, IEquatable<WeaponData>
    {
        /// <summary>
        /// Nombre del arma.
        /// </summary>
        public string weaponName;

        /// <summary>
        /// Sprite asociado al arma (visualización en el juego).
        /// </summary>
        public Sprite weaponSprite;

        /// <summary>
        /// Daño base que inflige el arma.
        /// </summary>
        public int weaponDamage = 1;

        /// <summary>
        /// Sonido que se reproduce al usar el arma.
        /// </summary>
        public AudioClip weaponSwingSound;

        /// <summary>
        /// Compara si dos armas son iguales basándose en su nombre.
        /// </summary>
        /// <param name="other">El arma a comparar.</param>
        /// <returns>True si los nombres coinciden, false de lo contrario.</returns>
        public bool Equals(WeaponData other)
        {
            return weaponName == other.weaponName;
        }

        /// <summary>
        /// Determina si el arma puede ser usada según el estado del jugador.
        /// </summary>
        /// <param name="isGrounded">Indica si el jugador está en el suelo.</param>
        public abstract bool CanBeUsed(bool isGrounded);

        /// <summary>
        /// Realiza el ataque del arma.
        /// </summary>
        /// <param name="agent">Agente que usa el arma.</param>
        /// <param name="hittableMask">Capa de los objetos que pueden ser golpeados.</param>
        /// <param name="direction">Dirección del ataque.</param>
        public abstract void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction);

        /// <summary>
        /// Dibuja un gizmo representando el arma (para debug).
        /// </summary>
        /// <param name="origin">Posición de inicio del gizmo.</param>
        /// <param name="direction">Dirección del gizmo.</param>
        public virtual void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            // Método opcional para personalizar el gizmo del arma.
        }
    }
}
