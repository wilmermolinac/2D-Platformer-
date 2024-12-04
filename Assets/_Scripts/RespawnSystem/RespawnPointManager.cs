using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RespawnSystem
{
    /// <summary>
    /// Gestiona los puntos de reaparición de la escena y controla cuál está activo.
    /// </summary>
    public class RespawnPointManager : MonoBehaviour
    {
        public List<RespawnPoint> respawnPoints = new List<RespawnPoint>(); // Lista de todos los puntos de reaparición.
        public RespawnPoint currentRespawnPoint; // Punto de reaparición actualmente activo.

        /// <summary>
        /// Inicializa los puntos de reaparición al inicio.
        /// </summary>
        private void Awake()
        {
            foreach (Transform item in transform)
            {
                // Agrega cada hijo que tenga un componente RespawnPoint a la lista.
                respawnPoints.Add(item.GetComponent<RespawnPoint>());
            }

            currentRespawnPoint = respawnPoints[0]; // Establece el primer punto como activo por defecto.
        }

        /// <summary>
        /// Actualiza el punto de reaparición activo.
        /// </summary>
        /// <param name="newRespawnPoint">El nuevo punto de reaparición.</param>
        public void UpdateRespawnPoint(RespawnPoint newRespawnPoint)
        {
            currentRespawnPoint.DisableRespawnPoint(); // Desactiva el punto actual.
            currentRespawnPoint = newRespawnPoint; // Establece el nuevo punto como activo.
        }

        /// <summary>
        /// Reaparece un objeto en el punto de reaparición actual.
        /// </summary>
        /// <param name="objectToRespawn">El objeto que será reaparecido.</param>
        public void Respawn(GameObject objectToRespawn)
        {
            currentRespawnPoint.RespawnPositionPlayer(); // Posiciona el objeto en el punto actual.
            objectToRespawn.SetActive(true); // Activa el objeto.
        }

        /// <summary>
        /// Reaparece un objeto en un punto de reaparición específico.
        /// </summary>
        /// <param name="spawnPoint">El punto de reaparición deseado.</param>
        /// <param name="playerGo">El objeto del jugador.</param>
        public void RespawnAt(RespawnPoint spawnPoint, GameObject playerGo)
        {
            spawnPoint.SetPlayerGo(playerGo); // Configura el jugador en el punto específico.
            Respawn(playerGo); // Llama al método de reaparición.
        }

        /// <summary>
        /// Restablece todos los puntos de reaparición a su estado inicial.
        /// </summary>
        public void ResetAllSpawnPoints()
        {
            foreach (var respawnPoint in respawnPoints)
            {
                respawnPoint.ResetRespawnPoint(); // Resetea cada punto de reaparición.
            }

            currentRespawnPoint = respawnPoints[0]; // Restablece el primer punto como activo.
        }
    }
}
