﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackTrigger : MonoBehaviour
{
    public UnityEvent OnBeat;
    public float tol = 0.05f;
    public int bars = 1;
    int last_hit = 0;
    public List<Riff.Note> notes;
    private Riff riff;

    private void Start()
    {
        riff = new Riff(4, notes, MusicManager.current);
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
    }

    private void Update() {
        
    }

    private void FixedUpdate()
    {
        riff.Update();
    }

    private void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (e.automatic && e.noteIndex != -1 && e.deltaTime < tol) {
            OnBeat.Invoke();
        }
    }
}
