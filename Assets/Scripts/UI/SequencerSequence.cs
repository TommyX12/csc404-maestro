using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SequencerSequence : MonoBehaviour{

    public GameObject sequencerNotePrefab;

    private SequenceNote[] noteObjects;

    private MusicManager musicManager;
    private Riff riff = null;
    
    public SequencerSequence() {
        
    }

    private void AddNotes(Riff riff) {
        this.riff = riff;
        
        List<Riff.Note> notes = riff.GetNotes();
        noteObjects = new SequenceNote[notes.Count];
        for (int i = 0; i < notes.Count; ++i) {
            Riff.Note note = notes[i];
            float position = note.beat / riff.GetBeatsPerCycle();
            SequenceNote noteObject = GameObject.Instantiate(sequencerNotePrefab, this.gameObject.transform).GetComponent<SequenceNote>();
            noteObject.SetPosition(position);
            noteObject.note = note;

            noteObjects[i] = noteObject;
        }
    }

    protected void Awake() {
        musicManager = MusicManager.Current;
        
        List<Riff.Note> notes = new List<Riff.Note>() {
            new Riff.Note(0, 4.0f),
            new Riff.Note(2, 4.0f),
            new Riff.Note(4, 4.0f),
            new Riff.Note(6, 4.0f),
            new Riff.Note(7, 4.0f),
            new Riff.Note(9, 4.0f),
            new Riff.Note(10, 4.0f),
            new Riff.Note(12, 4.0f),
            new Riff.Note(14, 4.0f),
        };
        Riff riff = new Riff(4, notes, musicManager);
        AddNotes(riff);
    }

    protected void Start() {
        
    }

    protected void Update() {
        float currentPos = musicManager.GetBeatPosition(riff.GetBeatsPerCycle());
        foreach (var noteObject in noteObjects) {
            float dist = musicManager.GetDistanceToBeat(noteObject.note.beat, riff.GetBeatsPerCycle());
            noteObject.SetExcitement(dist);
        }
    }
}

