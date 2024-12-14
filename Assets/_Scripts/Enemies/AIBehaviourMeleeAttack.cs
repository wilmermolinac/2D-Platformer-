using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comportamiento de ataque cuerpo a cuerpo para un agente AI.
/// Detecta al jugador y realiza ataques con un retardo entre cada uno.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class AIBehaviourMeleeAttack : AiBehaviour
    {
        /// <summary>
        /// Detector que verifica si el jugador está en rango para un ataque cuerpo a cuerpo.
        /// </summary>
        public AIMeleeAttackDetector meleeRangeDetector;

        /// <summary>
        /// Indica si el agente está esperando para realizar el próximo ataque.
        /// </summary>
        [SerializeField] private bool _isWaiting;

        /// <summary>
        /// Tiempo de espera entre ataques.
        /// </summary>
        [SerializeField] private float _delay = 1;

        /// <summary>
        /// Método llamado al inicializar el script.
        /// Asegura que el detector de rango de ataque esté configurado correctamente.
        /// </summary>
        private void Awake()
        {
            if (meleeRangeDetector == null)
                meleeRangeDetector = transform.parent.GetComponentInParent<AIMeleeAttackDetector>();
        }

        /// <summary>
        /// Ejecuta el comportamiento de ataque si el jugador está en rango.
        /// </summary>
        /// <param name="enemyAI">Instancia del agente enemigo que ejecuta este comportamiento.</param>
        public override void PerformAction(AiEnemy enemyAI)
        {
            // Si el agente está esperando, no realiza un ataque.
            if (_isWaiting)
                return;

            // Si el jugador no está detectado, no realiza un ataque.
            if (meleeRangeDetector.PlayerDetected == false)
                return;

            // Invoca el ataque en el agente.
            enemyAI.CallOnAttack();

            // Inicia el retardo entre ataques.
            _isWaiting = true;
            StartCoroutine(AttackDelayCoroutine());
        }

        /// <summary>
        /// Corrutina que gestiona el tiempo de espera entre ataques.
        /// </summary>
        public IEnumerator AttackDelayCoroutine()
        {
            yield return new WaitForSeconds(_delay); // Espera el tiempo definido en "_delay".
            _isWaiting = false; // Permite realizar el próximo ataque.
        }
    }
}
