﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class SequencerSequence : MonoBehaviour{

    // references
    public GameObject sequencerNotePrefab;
    public GameObject sequencerMarkerPrefab;
    public Image backgroundPanel;

    private Color idleColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color hitColor = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    private Color missColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);

    protected float targetScale = 1.0f;
    protected float currentScale = 1.0f;
    protected float scaleSmoothFriction = 0.7f;
    protected float staticScale = 1.0f;

    private SequenceNote[] noteObjects;
    private SequenceMarker[] markerObjects;

    private Riff riff = null;
    private int beatsPerCycle;

    private int lastSeenCycle = 0;

    private float hitEffectTimer = 0;
    private float hitEffectDuration = 0.5f;
    private bool hitSuccessful = false;

    // self reference
    private RectTransform rectTransform;

    // Injected references
    private GameplayModel model;
    private GlobalConfiguration config;
    private GlobalRules rules;
    private MusicManager musicManager;
    
    public SequencerSequence() {
        
    }

    [Inject]
    public void Construct(GameplayModel model,
                          GlobalConfiguration config,
                          GlobalRules rules,
                          MusicManager musicManager) {
        this.model = model;
        this.config = config;
        this.rules = rules;
        this.musicManager = musicManager;
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

    private void PlayHitSound() {
        // musicManager.PlayOnce(config.CorrectNoteHitSoundName, 0);
        // TODO problem due to delay
    }

    private void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (!e.automatic) {
            if (e.noteIndex >= 0) {
                noteObjects[e.noteIndex].SetHitState(SequenceNote.HIT_STATE_HIT);
                hitSuccessful = true;
                PlayHitSound();
            }
            else {
                hitSuccessful = false;
            }
            hitEffectTimer = hitEffectDuration;

            var score = rules.GetHitScore(e);
            model.NotifyBeatPressed(new GameplayModel.BeatPressedEvent {
                Score = score
            });
        }
    }

    public void SetColor(Color color) {
        idleColor.r = color.r;
        idleColor.g = color.g;
        idleColor.b = color.b;
    }

    public void SetVisible(bool visible, bool immediate = false) {
        if (visible) {
            targetScale = 1.0f;
        }
        else {
            targetScale = 0.01f;
        }
    }

    protected void UpdateTransform() {
        currentScale = Mathf.Lerp(currentScale, targetScale, 1 - scaleSmoothFriction);
        rectTransform.localScale = new Vector3(currentScale, currentScale, currentScale) * staticScale;
    }

    protected void Awake() {
        rectTransform = GetComponent<RectTransform>();
        staticScale = rectTransform.localScale.x;
    }

    protected void Start() {
        musicManager = MusicManager.current;
    }

    private void CleanUp() {
        this.riff.delayedNoteHitEvent -= DelayedNoteHitEventHandler;
        this.riff = null;

        foreach (var obj in noteObjects) {
            GameObject.Destroy(obj.gameObject);
        }
        foreach (var obj in markerObjects) {
            GameObject.Destroy(obj.gameObject);
        }
        noteObjects = null;
        markerObjects = null;
    }

    protected void FixedUpdate() {
        UpdateTransform();
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
        float hitMarginAfterBeats = musicManager.TimeToBeat(riff.GetHitMarginAfter());
        foreach (var noteObject in noteObjects) {
            float dist = musicManager.GetDistanceToBeat(noteObject.note.beat, beatsPerCycle);
            noteObject.SetBeatDistance(dist, hitMarginAfterBeats);
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

