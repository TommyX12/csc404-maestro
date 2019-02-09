﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequencerSequence : MonoBehaviour{

    // references
    public GameObject sequencerNotePrefab;
    public GameObject sequencerMarkerPrefab;
    public Image backgroundPanel;

    private Color idleColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color hitColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    private Color missColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    private SequenceNote[] noteObjects;
    private SequenceMarker[] markerObjects;

    private MusicManager musicManager;
    private Riff riff = null;
    private int beatsPerCycle;

    private int lastSeenCycle = 0;

    private float hitEffectTimer = 0;
    private float hitEffectDuration= 0.5f;
    private bool hitSuccessful = false;
    
    public SequencerSequence() {
        
    }

    public void SetRiff(Riff riff) {
        if (this.riff != null) {
            CleanUp();
        }
        this.riff = riff;
        this.beatsPerCycle = riff.GetBeatsPerCycle();

        this.riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;

        markerObjects = new SequenceMarker[beatsPerCycle];
        for (int i = 0; i < beatsPerCycle; ++i) {
            float position = ((float) i) / beatsPerCycle;
            
            SequenceMarker markerObject = GameObject.Instantiate(sequencerMarkerPrefab, this.gameObject.transform).GetComponent<SequenceMarker>();
            markerObject.SetPosition(position);

            markerObjects[i] = markerObject;
        }
        
        List<Riff.Note> notes = riff.GetNotes();
        noteObjects = new SequenceNote[notes.Count];
        for (int i = 0; i < notes.Count; ++i) {
            Riff.Note note = notes[i];
            float position = note.beat / beatsPerCycle;
            SequenceNote noteObject = GameObject.Instantiate(sequencerNotePrefab, this.gameObject.transform).GetComponent<SequenceNote>();
            noteObject.SetPosition(position);
            noteObject.note = note;

            noteObjects[i] = noteObject;
        }
    }

    private void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (!e.automatic) {
            if (e.noteIndex >= 0) {
                noteObjects[e.noteIndex].SetHitState(SequenceNote.HIT_STATE_HIT);
                hitSuccessful = true;
            }
            else {
                hitSuccessful = false;
            }
            hitEffectTimer = hitEffectDuration;

        }
    }

    public void SetColor(Color color) {
        idleColor.r = color.r;
        idleColor.g = color.g;
        idleColor.b = color.b;
    }

    protected void Awake() {
        musicManager = MusicManager.Current;
    }

    protected void Start() {
        
    }

    private void CleanUp() {
        this.riff.delayedNoteHitEvent -= DelayedNoteHitEventHandler;
        this.riff = null;

        foreach (var obj in noteObjects) {
            Destroy(obj);
        }
        foreach (var obj in markerObjects) {
            Destroy(obj);
        }
    }

    protected void Update() {
        if (riff == null) {
            return;
        }

        int cycle = musicManager.GetCycleIndex(beatsPerCycle);
        if (cycle != lastSeenCycle) {
            lastSeenCycle = cycle;
        }
        
        float currentPos = musicManager.GetBeatPosition(beatsPerCycle);
        foreach (var noteObject in noteObjects) {
            float dist = musicManager.GetDistanceToBeat(noteObject.note.beat, beatsPerCycle);
            noteObject.SetBeatDistance(dist);
        }

        // hit color effect
        if (hitEffectTimer > 0) {
            hitEffectTimer -= Time.deltaTime;
        }
        if (hitEffectTimer < 0) {
            hitEffectTimer = 0;
        }
        
        Color excitedColor = hitSuccessful ? hitColor : missColor;
        backgroundPanel.color = idleColor + (0.75f * hitEffectTimer / hitEffectDuration) * (excitedColor - idleColor);
    }
}
