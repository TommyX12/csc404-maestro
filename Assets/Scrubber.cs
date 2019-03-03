using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Scrubber : MonoBehaviour
{
    public static Scrubber instance;
    public bool replay;

    private void Start()
    {
        instance = this;
    }

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

    [ExecuteInEditMode]
    public void SetAsMaster() {
        instance = this;
    }
}