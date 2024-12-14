using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del cerebro de un enemigo AI con patrullaje.
/// Coordina los comportamientos de ataque y patrullaje.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class AiPatrollingEnemyBrain : AiEnemy
    {
        /// <summary>
        /// Detector que verifica si el agente está en el suelo.
        /// </summary>  
        public GroundDetector agentGroundDetector;

        /// <summary>
        /// Comportamiento de ataque del agente.
        /// </summary>
        public AiBehaviour attackBehaviour;

        /// <summary>
        /// Comportamiento de patrullaje del agente.
        /// </summary>
        public AiBehaviour patrolBehaviour;

        /// <summary>
        /// Método llamado al inicializar el script.
        /// Configura los detectores y comportamientos si no están asignados.
        /// </summary>
        private void Awake()
        {
            if (agentGroundDetector == null || attackBehaviour == null || patrolBehaviour == null)
            {
                agentGroundDetector = GetComponentInChildren<GroundDetector>();
                attackBehaviour = GetComponentInChildren<AIBehaviourMeleeAttack>();
                patrolBehaviour = GetComponentInChildren<AIBehaviourPatrol>();
            }
        }

        /// <summary>
        /// Método llamado en cada frame.
        /// Ejecuta los comportamientos de ataque y patrullaje si el agente está en el suelo.
        /// </summary>
        private void Update()
        {
            if (agentGroundDetector.isGrounded)
            {
                attackBehaviour.PerformAction(this);
                patrolBehaviour.PerformAction(this);
            }
        }
    }
}
