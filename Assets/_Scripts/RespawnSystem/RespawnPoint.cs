using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RespawnSystem
{
    /// <summary>
    /// Representa un punto de reaparición donde el jugador puede reaparecer.
    /// </summary>
    public class RespawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _respawnTarget; // Referencia al objeto que reaparecerá en este punto.

        // Evento que se invoca al activar este punto de reaparición.
        [field: SerializeField] private UnityEvent OnSpawnPointActivated { get; set; }

        /// <summary>
        /// Inicializa el punto de reaparición al inicio.
        /// </summary>
        private void Start()
        {
            // Agrega un listener al evento OnSpawnPointActivated para que actualice automáticamente
            // el punto de reaparición en el RespawnPointManager. Esto elimina la necesidad de configurarlo manualmente.
            OnSpawnPointActivated.AddListener(() =>
                GetComponentInParent<RespawnPointManager>().UpdateRespawnPoint(this)
            );
        }

        /// <summary>
        /// Detecta si el jugador entra en el área de colisión del punto de reaparición.
        /// </summary>
        /// <param name="collision">El objeto que colisiona con el trigger.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) // Comprueba si el objeto que entra es el jugador.
            {
                _respawnTarget = collision.gameObject; // Almacena el jugador como objetivo de reaparición.
                OnSpawnPointActivated?.Invoke(); // Activa el evento de este punto.
            }
        }

        /// <summary>
        /// Reposiciona al jugador en este punto de reaparición.
        /// </summary>
        public void RespawnPositionPlayer()
        {
            _respawnTarget.transform.position = transform.position;
        }

        /// <summary>
        /// Establece un nuevo objetivo de reaparición (generalmente el jugador).
        /// </summary>
        /// <param name="player">El objeto del jugador.</param>
        public void SetPlayerGo(GameObject player)
        {
            _respawnTarget = player;
            GetComponent<Collider2D>().enabled = false; // Desactiva el collider para evitar reutilizaciones accidentales.
        }

        /// <summary>
        /// Desactiva este punto de reaparición.
        /// </summary>
        public void DisableRespawnPoint()
        {
            GetComponent<Collider2D>().enabled = false;
        }

        /// <summary>
        /// Restablece este punto de reaparición a su estado inicial.
        /// </summary>
        public void ResetRespawnPoint()
        {
            _respawnTarget = null; // Limpia el objetivo de reaparición.
            GetComponent<Collider2D>().enabled = true; // Habilita nuevamente el collider.
        }
    }
}
