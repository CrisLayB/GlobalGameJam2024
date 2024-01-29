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
        
        audioClips.Add(AudioClipName.BellDone, Resources.Load<AudioClip>("BellDone"));
        audioClips.Add(AudioClipName.ButtonsElevator, Resources.Load<AudioClip>("ButtonsElevator"));
        audioClips.Add(AudioClipName.MakeCoffee, Resources.Load<AudioClip>("MakeCoffee"));
        audioClips.Add(AudioClipName.ElevatorArrives, Resources.Load<AudioClip>("ElevatorArrives"));
        audioClips.Add(AudioClipName.Plunger, Resources.Load<AudioClip>("Plunger"));
        audioClips.Add(AudioClipName.FlashLightSound, Resources.Load<AudioClip>("FlashLightSound"));
        audioClips.Add(AudioClipName.Yeso, Resources.Load<AudioClip>("Yeso"));
        audioClips.Add(AudioClipName.PlungerShortened, Resources.Load<AudioClip>("PlungerShortened"));
        audioClips.Add(AudioClipName.ToiletFlushSOund, Resources.Load<AudioClip>("ToiletFlushSOund"));
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
