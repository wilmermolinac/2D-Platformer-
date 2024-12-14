using UnityEngine;

/// <summary>
/// Clase base abstracta que define un comportamiento para un agente AI.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public abstract class AiBehaviour : MonoBehaviour
    {
        /// <summary>
        /// Método que define la acción específica que realizará el agente.
        /// Este método debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="enemyAI">Instancia del agente enemigo que ejecuta este comportamiento.</param>
        public abstract void PerformAction(AiEnemy enemyAI);
    }
}
