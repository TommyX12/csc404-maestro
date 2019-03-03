using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrubber : MonoBehaviour
{
    public AudioSource source;

    [ExecuteInEditMode]
    public void PlayMusic() {
        source.Play();
    }

    [ExecuteInEditMode]
    public void StopMusic() {
        source.Stop();
    }

    [ExecuteInEditMode]
    public void PauseMusic() {
        source.Pause();
    }
}