using System;
using System.Collections.Generic;
using UnityEngine;

public class Riff {

    public delegate void NoteHitEventHandler(NoteHitEvent e);
    public event NoteHitEventHandler noteHitEvent;

    private int beatsPerCycle;
    private List<Note> notes;
    private NoteIndexWithCycle lastHit;
    private NoteIndexWithCycle lastAutoHit;
    private int currentCycle;
    private float currentBeat;
    private float currentTime;
    public float hitMarginBefore = 0.2f; // in seconds
    public float hitMarginAfter = 0.2f; // in seconds
    public float hitOffset = 0.2f; // in seconds

    private MusicManager musicManager;

    // notes have to be sorted by phase
    public Riff(int beatsPerCycle,
                List<Note> notes,
                MusicManager musicManager) {
        this.beatsPerCycle = beatsPerCycle;
        this.notes = notes;

        this.musicManager = musicManager;

        Reset();
    }

    private void Reset() {
        currentTime = 0;
        currentCycle = 0;
        currentBeat = 0;
        ResetLastHit();
    }

    private void ResetLastHit() {
        lastHit.noteIndex = 0;
        lastHit.cycle = -1;
        lastAutoHit.cycle = currentCycle;
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
            lastAutoHit.cycle--;
        }
    }

    private void CheckAutoHit() {
        NoteIndexWithCycle next = lastAutoHit.next(this);
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

    private float GetNoteTotalBeat(NoteIndexWithCycle pos) {
        return beatsPerCycle * pos.cycle + notes[pos.noteIndex].beat;
    }

    private float GetCurrentTotalBeat() {
        return beatsPerCycle * currentCycle + currentBeat;
    }

    // call this in FixedUpdate
    public void Update() {
        float time = musicManager.GetTotalTimer();
        float beat = musicManager.GetBeatIndex(beatsPerCycle);
        int cycle = musicManager.GetCycleIndex(beatsPerCycle);

        currentBeat = beat;
        currentCycle = cycle;
        
        if (time < currentTime) {
            ResetLastHit();
        }
        currentTime = time;
        
        CheckAutoHit();
    }

    private void CheckNoteHitable(NoteIndexWithCycle index, ref float bestError, ref NoteIndexWithCycle best, ref float bestDeltaTime) {
        float totalBeat = GetNoteTotalBeat(index);
        float noteTime = musicManager.BeatToTime(totalBeat, beatsPerCycle);
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
        // Debug.Log("time: " + currentTime + ", beat: " + currentBeat + ", cycle: " + currentCycle);
        // Debug.Log("track position: " + musicManager.GetMusicTrack("csc404-test-1").GetPosition());
        
        NoteHitEvent result = new NoteHitEvent();
        result.automatic = false;
        
        float bestError = -1;
        NoteIndexWithCycle next = new NoteIndexWithCycle();

        if (currentCycle > 0) {
            CheckNoteHitable(new NoteIndexWithCycle(currentCycle - 1,
                                                  notes.Count - 1),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }
        
        for (int i = 0; i < notes.Count; ++i) {
            Note note = notes[i];
            CheckNoteHitable(new NoteIndexWithCycle(currentCycle, i),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }

        CheckNoteHitable(new NoteIndexWithCycle(currentCycle + 1, 0),
                         ref bestError,
                         ref next,
                         ref result.deltaTime);
        
        if (bestError < 0) {
            next.noteIndex = -1;
        }
        else {
            lastHit = next;
        }

        // Debug.Log("next: " + next.noteIndex + ", cycle: " + next.cycle);

        result.noteIndex = next.noteIndex;

        DispatchNoteHitEvent(result);

        return result;
    }

    public float GetBeatIndex() {
        return currentBeat;
    }

    public int GetCycleIndex() {
        return currentCycle;
    }

    private struct NoteIndexWithCycle {
        public int noteIndex;
        public int cycle;

        public NoteIndexWithCycle(int cycle, int noteIndex) {
            this.cycle = cycle;
            this.noteIndex = noteIndex;
        }

        public bool LessThan(NoteIndexWithCycle other) {
            return this.cycle < other.cycle || (this.cycle == other.cycle && this.noteIndex < other.noteIndex);
        }

        public NoteIndexWithCycle next(Riff riff) {
            NoteIndexWithCycle result = this;
            result.noteIndex++;
            if (result.noteIndex >= riff.notes.Count) {
                result.cycle++;
                result.noteIndex = 0;
            }
            return result;
        }

        public NoteIndexWithCycle prev(Riff riff) {
            NoteIndexWithCycle result = this;
            result.noteIndex--;
            if (result.noteIndex < 0) {
                result.cycle--;
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
