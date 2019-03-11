using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Scrubber : MonoBehaviour
{
    public static Scrubber instance;
    public bool replay;
    public int bpm=60;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource source;

    public void PlayMusic() {
        source.Play();
    }

    public void StopMusic() {
        source.Stop();
    }

    public void PauseMusic() {
        source.Pause();
    }

    public void SetAsMaster() {
        instance = this;
    }
}