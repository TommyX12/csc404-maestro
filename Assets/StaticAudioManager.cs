using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAudioManager : MonoBehaviour
{

    public static StaticAudioManager current;

    private AudioSource BeatPreviewSound;
    
    private void Start()
    {
        current = this;
        BeatPreviewSound = gameObject.AddComponent<AudioSource>();
        BeatPreviewSound.clip = ResourceManager.GetMusic("PreviewBeat");
    }

    public AudioSource GetPreviewSound() {
        return BeatPreviewSound;
    }

}
