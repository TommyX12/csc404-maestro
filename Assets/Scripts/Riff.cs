using System;
using System.Collections.Generic;
using UnityEngine;

public class Riff {

    public delegate void NoteHitEventHandler(NoteHitEvent e);
    public event NoteHitEventHandler noteHitEvent;

    private int beatsPerBar;
    private List<Note> notes;
    private NoteIndexWithBar lastHit;
    private NoteIndexWithBar lastAutoHit;
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
        lastAutoHit.bar = currentBar;
        lastAutoHit.noteIndex = -1;
        for (int i = 0; i < notes.Count; ++i) {
            Note note = notes[i];
            if (note.beat < currentBeat) {
                lastAutoHit.noteIndex = i;
                break;
            }
        }
        if (lastAutoHit.noteIndex == -1) {
            lastAutoHit.noteIndex = notes.Count - 1;
            lastAutoHit.bar--;
        }
    }

    private void CheckAutoHit() {
        NoteIndexWithBar next = lastAutoHit.next(this);
        float currentTotalBeat = GetCurrentTotalBeat();
        while (currentTotalBeat >= GetNoteTotalBeat(next)) {
            NoteHitEvent e;
            e.noteIndex = next.noteIndex;
            e.deltaTime = 0;
            e.automatic = true;
            DispatchNoteHitEvent(e);
            lastAutoHit = next;
            next = next.next(this);
        }
    }

    private float GetNoteTotalBeat(NoteIndexWithBar pos) {
        return beatsPerBar * pos.bar + notes[pos.noteIndex].beat;
    }

    private float GetCurrentTotalBeat() {
        return beatsPerBar * currentBar + currentBeat;
    }

    // call this in FixedUpdate
    public void Update() {
        float time = musicManager.GetTotalTimer();
        float beat = musicManager.GetBeatIndex(beatsPerBar);
        int bar = musicManager.GetBarIndex(beatsPerBar);

        // set current index
        currentTime = time;
        currentBeat = beat;
        currentBar = bar;
        
        if (time < currentTime) {
            ResetLastHit();
        }

        CheckAutoHit();
    }

    private void CheckNoteHitable(NoteIndexWithBar index, ref float bestError, ref NoteIndexWithBar best, ref float bestDeltaTime) {
        float totalBeat = GetNoteTotalBeat(index);
        float noteTime = musicManager.BeatToTime(totalBeat, beatsPerBar);
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

    private void DispatchNoteHitEvent(NoteHitEvent e) {
        if (noteHitEvent != null) {
            noteHitEvent(e);
        }
    }

    public NoteHitEvent ButtonPress() {
        // find coordinates
        // Debug.Log("time: " + currentTime + ", beat: " + currentBeat + ", bar: " + currentBar);
        // Debug.Log("track position: " + musicManager.GetMusicTrack("csc404-test-1").GetPosition());
        
        NoteHitEvent result = new NoteHitEvent();
        result.automatic = false;
        
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

        // Debug.Log("next: " + next.noteIndex + ", bar: " + next.bar);

        result.noteIndex = next.noteIndex;

        DispatchNoteHitEvent(result);

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

        public NoteIndexWithBar next(Riff riff) {
            NoteIndexWithBar result = this;
            result.noteIndex++;
            if (result.noteIndex >= riff.notes.Count) {
                result.bar++;
                result.noteIndex = 0;
            }
            return result;
        }

        public NoteIndexWithBar prev(Riff riff) {
            NoteIndexWithBar result = this;
            result.noteIndex--;
            if (result.noteIndex < 0) {
                result.bar--;
                result.noteIndex = riff.notes.Count - 1;
            }
            return result;
        }
    }

    [Serializable]
    public class Note {
        [SerializeField]
        public float index; // position, in beats relative to start of rift
        public float unitPerBeat; // position, in beats relative to start of rift
        public float beat {
            get {
                return index / unitPerBeat;
            }
        }

        public Note(float index, float unitPerBeat = 1) {
            this.index = index;
            this.unitPerBeat = unitPerBeat;
        }
    }

    public struct NoteHitEvent {
        public int noteIndex;
        public float deltaTime; // actual press time - desired press time
        public bool automatic;
    }
    
}
