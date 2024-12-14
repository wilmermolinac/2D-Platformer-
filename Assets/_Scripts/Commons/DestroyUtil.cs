using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utilidad para manejar la destrucción de objetos en la escena.
/// </summary>
namespace WAMCSTUDIOS.AI
{
    public class DestroyUtil : MonoBehaviour
    {
        /// <summary>
        /// Destruye este objeto (el objeto al que está asociado este script).
        /// </summary>
        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// Destruye un objeto específico proporcionado como parámetro.
        /// </summary>
        /// <param name="objectToDestroy">El objeto que se destruirá.</param>
        public void DestroyObject(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }
    }
}
