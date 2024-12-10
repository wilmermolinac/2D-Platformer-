using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representa los datos de un arma cuerpo a cuerpo.
/// Permite configurar el rango de ataque y define su comportamiento.
/// </summary>
namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "Melee weapon data", menuName = "Weapons/MeleeWeaponData")]
    public class MeleeWeaponData : WeaponData
    {
        /// <summary>
        /// Rango máximo del ataque del arma.
        /// </summary>
        public float attackRange = 2;

        /// <summary>
        /// Verifica si el arma puede ser utilizada.
        /// </summary>
        /// <param name="isGrounded">Indica si el agente está en el suelo.</param>
        /// <returns>Verdadero si el agente está en el suelo; falso en caso contrario.</returns>
        public override bool CanBeUsed(bool isGrounded)
        {
            return isGrounded == true;
        }

        /// <summary>
        /// Realiza un ataque con el arma.
        /// </summary>
        /// <param name="agent">El agente que realiza el ataque.</param>
        /// <param name="hittableMask">Máscara de objetos que pueden ser impactados.</param>
        /// <param name="direction">Dirección del ataque.</param>
        public override void PerformAttack(Agent agent, LayerMask hittableMask, Vector3 direction)
        {
            // Muestra el nombre del arma en la consola para depuración.
            Debug.Log($"Weapon name: {weaponName}");
            
            // Realiza un raycast para detectar colisiones con objetos atacables.
            RaycastHit2D hit = Physics2D.Raycast(agent.agentWeaponManager.transform.position, direction, attackRange, hittableMask);

            // Si el raycast detecta un objeto atacable, procesa el impacto.
            if (hit.collider != null)
            {
                // Obtiene todos los componentes que implementan la interfaz IHittable.
                foreach (var hittable in hit.collider.GetComponents<IHittable>())
                {
                    // Llama al método GetHit de cada objeto impactado.
                    hittable.GetHit(agent.gameObject, weaponDamage);
                }
            }
        }

        /// <summary>
        /// Dibuja un gizmo en el editor para visualizar el rango del arma.
        /// </summary>
        /// <param name="origin">Posición inicial del arma.</param>
        /// <param name="direction">Dirección del ataque.</param>
        public override void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
        {
            // Dibuja una línea que representa el rango del arma.
            Gizmos.DrawLine(origin, origin + direction * attackRange);
        }
    }
}
