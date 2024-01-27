using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Obtniene si es un audio o no
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Inicializar el Audio Maneger con los audios disponibles
    /// </summary>
    /// <param name="name">nombre del audio que se ejecutará</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;

        // Flash Light Sound        
        audioClips.Add(AudioClipName.FlashLightSound, Resources.Load<AudioClip>("FlashLightSound"));        
    }

    /// <summary>
    /// Ejecuta el audio con el clip y nombre incluido
    /// </summary>
    /// <param name="name">nombre del audio que se ejecutará</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
