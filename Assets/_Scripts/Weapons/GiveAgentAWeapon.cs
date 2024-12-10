using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Asigna armas a un agente en la escena al iniciar el juego.
/// </summary>
namespace WeaponSystem
{
    public class GiveAgentAWeapon : MonoBehaviour
    {
        /// <summary>
        /// Lista de datos de armas que se pueden asignar al agente.
        /// </summary>
        public List<WeaponData> weaponDataList = new List<WeaponData>();

        /// <summary>
        /// Método llamado al iniciar el juego. Asigna armas al agente.
        /// </summary>
        private void Start()
        {
            // Obtiene el componente del agente en los hijos de este objeto.
            Agent agent = GetComponentInChildren<Agent>();

            // Si no hay un agente, termina la ejecución.
            if (agent == null)
            {
                return;
            }

            // Asigna cada arma de la lista al administrador de armas del agente.
            foreach (var weaponData in weaponDataList)
            {
                agent.agentWeaponManager.AddWeaponData(weaponData);
            }
        }
    }
}
