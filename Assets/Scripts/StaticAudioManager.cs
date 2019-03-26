using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class StaticAudioManager : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public static StaticAudioManager current;

    private AudioSource BeatPreviewSound;
    
    private void Start()
    {
        current = this;
        BeatPreviewSound = gameObject.AddComponent<AudioSource>();
        BeatPreviewSound.outputAudioMixerGroup = mixer;
        BeatPreviewSound.clip = ResourceManager.GetMusic("PreviewBeat");
    }

    public AudioSource GetPreviewSound() {
        return BeatPreviewSound;
    }
}