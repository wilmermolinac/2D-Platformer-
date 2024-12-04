using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RespawnSystem
{
    /// <summary>
    /// Ayuda a gestionar la reaparición del jugador interactuando con el RespawnPointManager.
    /// </summary>
    public class RespawnHelper : MonoBehaviour
    {
        private RespawnPointManager _manager; // Referencia al gestor de puntos de reaparición.

        /// <summary>
        /// Busca y almacena una referencia al RespawnPointManager en la escena.
        /// </summary>
        private void Awake()
        {
            _manager = FindObjectOfType<RespawnPointManager>();
        }

        /// <summary>
        /// Reaparece al jugador en el punto de reaparición actual.
        /// </summary>
        public void RespawnPlayer()
        {
            _manager.Respawn(gameObject); // Llama al método de reaparición en el gestor.
        }

        /// <summary>
        /// Restablece todos los puntos de reaparición y reaparece al jugador.
        /// </summary>
        public void ResetPlayer()
        {
            _manager.ResetAllSpawnPoints(); // Resetea todos los puntos de reaparición.
            _manager.Respawn(gameObject); // Reaparece al jugador.
        }
    }
}
