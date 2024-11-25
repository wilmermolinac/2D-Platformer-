using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Clase para manejar la dirección visual del agente (hacia dónde mira).
public class AgentRenderer : MonoBehaviour
{
    public void FaceDirection(Vector2 input)
    {
        // Cambia la escala del agente para que mire hacia la dirección horizontal del movimiento.

        if (input.x < 0)
        {
            // Si la entrada es negativa (moviéndose a la izquierda), voltea al agente hacia la izquierda.
            transform.parent.localScale = new Vector3(-1 * Mathf.Abs(transform.parent.localScale.x),
                transform.parent.localScale.y, transform.parent.localScale.z);
        }
        else if (input.x > 0)
        {
            // Si la entrada es positiva (moviéndose a la derecha), asegura que el agente mire a la derecha.
            transform.parent.localScale = new Vector3(Mathf.Abs(transform.parent.localScale.x),
                transform.parent.localScale.y, transform.parent.localScale.z);
        }
    }
}
