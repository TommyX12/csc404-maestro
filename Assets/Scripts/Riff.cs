using System;
using System.Collections.Generic;
using UnityEngine;

public class Riff {

    private int beatsPerBar;
    private List<Note> notes;
    private NoteIndexWithBar lastHit;
    private int currentBar;
    private float currentBeat;
    private float currentTime;
    public float hitMarginBefore = 0.2f; // in seconds
    public float hitMarginAfter = 0.2f; // in seconds
    public float hitOffset = 0.2f; // in seconds

    private MusicManager musicManager;

    // notes have to be sorted by phase
    public Riff(int beatsPerBar,
                List<Note> notes,
                MusicManager musicManager) {
        this.beatsPerBar = beatsPerBar;
        this.notes = notes;

        this.musicManager = musicManager;

        Reset();
    }

    private void Reset() {
        currentTime = 0;
        currentBar = 0;
        currentBeat = 0;

        ResetLastHit();
    }

    private void ResetLastHit() {
        lastHit.noteIndex = 0;
        lastHit.bar = -1;
    }

    private float GetNoteTotalBeat(NoteIndexWithBar pos) {
        return beatsPerBar * pos.bar + notes[pos.noteIndex].beat;
    }

    // call this in FixedUpdate
    public void Update() {
        float time = musicManager.GetTotalTimer();
        float beat = musicManager.GetBeatIndex(beatsPerBar);
        int bar = musicManager.GetBarIndex(beatsPerBar);
        
        if (time < currentTime) {
            ResetLastHit();
        }

        // set current index
        currentTime = time;
        currentBeat = beat;
        currentBar = bar;
    }

    private void CheckNoteHitable(NoteIndexWithBar index, ref float bestError, ref NoteIndexWithBar best, ref float bestDeltaTime) {
        float totalBeat = GetNoteTotalBeat(index);
        float noteTime = musicManager.BeatToTime(totalBeat);
        float deltaTime = currentTime - (noteTime + hitOffset);
        float error = Mathf.Abs(deltaTime);
        if (lastHit.LessThan(index) &&
            deltaTime <= hitMarginAfter &&
            deltaTime >= -hitMarginBefore &&
            (bestError < 0 || error < bestError)) {
            bestError = error;
            best = index;
            bestDeltaTime = deltaTime;
        }
    }

    public ButtonPressResult ButtonPress() {
        // find coordinates
        Debug.Log("time: " + currentTime + ", beat: " + currentBeat + ", bar: " + currentBar);
        Debug.Log("track position: " + musicManager.GetMusicTrack("csc404-test-1").GetPosition());
        
        ButtonPressResult result = new ButtonPressResult();
        
        float bestError = -1;
        NoteIndexWithBar next = new NoteIndexWithBar();

        if (currentBar > 0) {
            CheckNoteHitable(new NoteIndexWithBar(currentBar - 1,
                                                  notes.Count - 1),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }
        
        for (int i = 0; i < notes.Count; ++i) {
            Note note = notes[i];
            CheckNoteHitable(new NoteIndexWithBar(currentBar, i),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }

        CheckNoteHitable(new NoteIndexWithBar(currentBar + 1, 0),
                         ref bestError,
                         ref next,
                         ref result.deltaTime);
        
        if (bestError < 0) {
            next.noteIndex = -1;
        }
        else {
            lastHit = next;
        }

        Debug.Log("next: " + next.noteIndex + ", bar: " + next.bar);

        result.noteIndex = next.noteIndex;

        return result;
    }

    public float GetBeatIndex() {
        return currentBeat;
    }

    public int GetBarIndex() {
        return currentBar;
    }
    
    private struct NoteIndexWithBar {
        public int noteIndex;
        public int bar;

        public NoteIndexWithBar(int bar, int noteIndex) {
            this.bar = bar;
            this.noteIndex = noteIndex;
        }

        public bool LessThan(NoteIndexWithBar other) {
            return this.bar < other.bar || (this.bar == other.bar && this.noteIndex < other.noteIndex);
        }
    }

    public class Note {
        public float beat; // position, in beats relative to start of rift

        public Note(float beat) {
            this.beat = beat;
        }
    }

    public struct ButtonPressResult {
        public int noteIndex; // -1 means too early
        public float deltaTime; // actual press time - desired press time
    }
    
}
