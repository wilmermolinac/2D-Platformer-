using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Proporciona retroalimentación de audio mediante la reproducción de clips de sonido.
/// </summary>
public class AudioFeedback : MonoBehaviour
{
    public AudioClip clip; // Clip de audio que se reproducirá.
    public AudioSource targetAudioSource; // Fuente de audio donde se reproducirán los sonidos.
    [Range(0, 1)] public float volume = 1; // Controla el volumen del sonido (de 0 a 1).

    /// <summary>
    /// Reproduce el clip de audio asignado si está configurado.
    /// </summary>
    public void PlayClip()
    {
        // Si no hay clip asignado, termina la ejecución.
        if (clip == null)
            return;
        
        // Configura el volumen de la fuente de audio.
        targetAudioSource.volume = volume;
        // Reproduce el clip de audio.
        targetAudioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Reproduce un clip de audio específico, o el clip predeterminado si no se pasa ninguno.
    /// </summary>
    /// <param name="clipToPlay">Clip de audio a reproducir (opcional).</param>
    public void PlaySpecificClip(AudioClip clipToPlay = null)
    {
        // Si no se pasa un clip, usa el clip predeterminado.
        if (clipToPlay == null)
        {
            clipToPlay = clip;
        }

        // Si aún no hay clip disponible, termina la ejecución.
        if (clipToPlay == null)
        {
            return;
        }

        // Configura el volumen y reproduce el clip especificado.
        targetAudioSource.volume = volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
