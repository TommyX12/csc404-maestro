using System;
using System.Collections.Generic;
using UnityEngine;

public class Riff {

    public delegate void NoteHitEventHandler(NoteHitEvent e);
    
    // this correct for audio delay, so it triggers when user hear the beat
    public event NoteHitEventHandler delayedNoteHitEvent;
    
    // this does NOT correct for audio delay, so sound that plays when this triggers will align.
    public event NoteHitEventHandler noteHitEvent;

    private int beatsPerCycle;
    private List<Note> notes;

    private RiffPosition currentPosition;
    private RiffPosition currentPositionDelayed;
    
    public float hitMarginBefore = 0.2f; // in seconds
    public float hitMarginAfter = 0.2f; // in seconds
    public float hitOffset = 0.0f; // in seconds. already taken care of in MusicManager
    public float hitFailedBlockBeats = 0.5f; // in beats

    public float autoResetThreshold = 0.5f;

    public string defaultSound = null;

    public bool playing = false;

    private NoteIndexWithCycle lastPlayed;
    public float soundPreloadTime = 0.25f;
    private double lastScheduledPlayDSPTime;

    private float blockTimer = 0;

    private MusicManager musicManager;

    // notes have to be sorted by phase
    public Riff(int beatsPerCycle,
                List<Note> notes,
                MusicManager musicManager) {
        this.beatsPerCycle = beatsPerCycle;
        this.notes = notes;

        this.currentPosition = new RiffPosition();
        this.currentPositionDelayed = new RiffPosition();
        this.lastPlayed = new NoteIndexWithCycle();

        this.musicManager = musicManager;

        Reset();
    }

    public List<Note> GetNotes() {
        return notes;
    }

    public int GetBeatsPerCycle() {
        return beatsPerCycle;
    }

    private void Reset() {
        currentPosition.Reset();
        currentPositionDelayed.Reset();
        ResetLastHit(false);
        ResetLastHit(true);
        ResetLastPlayed();
    }

    private void ResetLastHit(bool delayed = true) {
        if (delayed) {
            currentPositionDelayed.ResetLastHit(this);
        }
        else {
            currentPosition.ResetLastHit(this);
        }
    }

    private void ResetLastPlayed() {
        lastPlayed.cycle = currentPosition.cycle;
        lastPlayed.noteIndex = -1;
        for (int i = 0; i < notes.Count; ++i) {
            Note note = notes[i];
            if (note.beat < currentPosition.beat) {
                lastPlayed.noteIndex = i;
                break;
            }
        }
        if (lastPlayed.noteIndex == -1) {
            lastPlayed.noteIndex = notes.Count - 1;
            lastPlayed.cycle--;
        }
    }

    private void CheckAutoHit(bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);
        NoteIndexWithCycle next = position.lastAutoHit.next(this);
        float currentTotalBeat = GetCurrentTotalBeat(delayed);
        while (currentTotalBeat >= GetNoteTotalBeat(next)) {
            NoteHitEvent e;
            e.noteIndex = next.noteIndex;
            e.deltaTime = 0;
            e.automatic = true;
            DispatchNoteHitEvent(e, delayed);
            position.lastAutoHit = next;
            next = next.next(this);
        }
    }

    private float GetNoteTotalBeat(NoteIndexWithCycle pos) {
        return beatsPerCycle * pos.cycle + notes[pos.noteIndex].beat;
    }

    private float GetCurrentTotalBeat(bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);
        return beatsPerCycle * position.cycle + position.beat;
    }

    private void UpdatePosition(bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);

        float time = musicManager.GetTotalTimer(delayed);
        float beat = musicManager.GetBeatIndex(beatsPerCycle, delayed);
        int cycle = musicManager.GetCycleIndex(beatsPerCycle, delayed);

        position.beat = beat;
        position.cycle = cycle;
        
        if (time < position.time || time - position.time > autoResetThreshold) {
            ResetLastHit(delayed);
            ResetLastPlayed();
        }
        position.time = time;
        CheckAutoHit(delayed);
    }

    protected void PlaySound() {
        // if (!playing) return;
        if (AudioSettings.dspTime < lastScheduledPlayDSPTime) {
            return;
        }
        RiffPosition position = GetCurrentPosition(false);
        NoteIndexWithCycle next = lastPlayed.next(this);
        float currentTotalBeat = GetCurrentTotalBeat(false);
        float nextTotalBeat = GetNoteTotalBeat(next);
        float currentTotalTime = musicManager.BeatToTime(currentTotalBeat);
        float nextTotalTime = musicManager.BeatToTime(nextTotalBeat);
        float delaySeconds = Mathf.Max(0, nextTotalTime - currentTotalTime);
        if (delaySeconds <= soundPreloadTime) {
            if (playing) {
                // Debug.Log(delaySeconds);
                string sound = notes[next.noteIndex].sound;
                if (sound == null || sound == "") {
                    sound = defaultSound;
                }
                if (sound != null && sound != "") {
                    musicManager.PlayOnce(sound, delaySeconds);
                    lastScheduledPlayDSPTime = AudioSettings.dspTime + delaySeconds;
                }
            }
            lastPlayed = next;
        }
    }

    protected void UpdateBlockTimer() {
        if (blockTimer > 0) {
            blockTimer -= Time.deltaTime;
            if (blockTimer < 0) {
                blockTimer = 0;
            }
        }
    }

    // call this in FixedUpdate
    public void Update() {
        UpdatePosition(false);
        UpdatePosition(true);
        PlaySound();
        UpdateBlockTimer();
    }

    private RiffPosition GetCurrentPosition(bool delayed = true) {
        return delayed ? currentPositionDelayed : currentPosition;
    }

    private void CheckNoteHitable(NoteIndexWithCycle index, ref float bestError, ref NoteIndexWithCycle best, ref float bestDeltaTime, bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);
        float totalBeat = GetNoteTotalBeat(index);
        float noteTime = musicManager.BeatToTime(totalBeat, beatsPerCycle);
        float deltaTime = position.time - (noteTime + hitOffset);
        float error = Mathf.Abs(deltaTime);
        if (position.lastHit.LessThan(index) &&
            deltaTime <= hitMarginAfter &&
            deltaTime >= -hitMarginBefore &&
            (bestError < 0 || error < bestError)) {
            bestError = error;
            best = index;
            bestDeltaTime = deltaTime;
        }
    }

    private void DispatchNoteHitEvent(NoteHitEvent e, bool delayed) {
        if (delayed) {
            if (delayedNoteHitEvent != null) {
                delayedNoteHitEvent(e);
            }
        }
        else {
            if (noteHitEvent != null) {
                noteHitEvent(e);
            }
        }
    }

    public NoteHitEvent ButtonPress(bool delayed = true) {
        // find coordinates
        // Debug.Log("time: " + position.time + ", beat: " + position.beat + ", cycle: " + position.cycle);
        // Debug.Log("track position: " + musicManager.GetMusicTrack("csc404-test-1").GetPosition());

        NoteHitEvent result = new NoteHitEvent();
        result.automatic = false;
        
        float bestError = -1;
        NoteIndexWithCycle next = new NoteIndexWithCycle();

        RiffPosition position = GetCurrentPosition(delayed);

        if (position.cycle > 0) {
            CheckNoteHitable(new NoteIndexWithCycle(position.cycle - 1,
                                                    notes.Count - 1),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }
        
        for (int i = 0; i < notes.Count; ++i) {
            Note note = notes[i];
            CheckNoteHitable(new NoteIndexWithCycle(position.cycle, i),
                             ref bestError,
                             ref next,
                             ref result.deltaTime);
        }

        CheckNoteHitable(new NoteIndexWithCycle(position.cycle + 1, 0),
                         ref bestError,
                         ref next,
                         ref result.deltaTime);
        
        if (bestError < 0) { // failed to hit
            next.noteIndex = -1;
            blockTimer = musicManager.BeatToTime(hitFailedBlockBeats);
        }
        else { // hit successful
            position.lastHit = next;
        }

        if (blockTimer <= 0) {
            result.noteIndex = next.noteIndex;
        }
        else {
            result.noteIndex = -1;
        }
        
        // Debug.Log("next: " + next.noteIndex + ", cycle: " + next.cycle);

        DispatchNoteHitEvent(result, delayed);

        return result;
    }

    public float GetBeatIndex(bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);
        return position.beat;
    }

    public int GetCycleIndex(bool delayed = true) {
        RiffPosition position = GetCurrentPosition(delayed);
        return position.cycle;
    }

    public struct NoteIndexWithCycle {
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
        public float index = 0; // position, in beats relative to start of rift
        public float unitPerBeat = 2; // position, in beats relative to start of rift
        public string sound = null;
        
        public float beat {
            get {
                return index / unitPerBeat;
            }
        }

        public Note(float index, float unitPerBeat = 1) {
            this.index = index;
            this.unitPerBeat = unitPerBeat;
        }

        public static List<Note> MakeRandomNotes(int beatsPerCycle, int unitPerBeat, int numNotes) {
            int numUnits = beatsPerCycle * unitPerBeat;
            bool[] units = new bool[numUnits];
            numNotes = Math.Min(numUnits, numNotes);
            for (int i = 0; i < numNotes; ++i) {
                units[i] = true;
            }

            List<Note> notes = new List<Note>();
            
            // Fisher–Yates shuffle
            for (int i = 0; i < units.Length; ++i) {
                int j = Mathf.FloorToInt(UnityEngine.Random.Range(i, units.Length - 0.0001f));
                bool temp = units[i];
                units[i] = units[j];
                units[j] = temp;

                if (units[i]) {
                    notes.Add(new Note(i, unitPerBeat));
                }
            }

            return notes;
        }
    }

    public struct NoteHitEvent {
        public int noteIndex;
        public float deltaTime; // actual press time - desired press time
        public bool automatic;
    }

    public class RiffPosition {
        public float time;
        public float beat;
        public int cycle;
        public NoteIndexWithCycle lastHit;
        public NoteIndexWithCycle lastAutoHit;

        public void Reset() {
            time = beat = cycle = 0;
        }

        public void ResetLastHit(Riff riff) {
            lastHit.noteIndex = 0;
            lastHit.cycle = -1;
            lastAutoHit.cycle = cycle;
            lastAutoHit.noteIndex = -1;
            for (int i = 0; i < riff.notes.Count; ++i) {
                Note note = riff.notes[i];
                if (note.beat < beat) {
                    lastAutoHit.noteIndex = i;
                    break;
                }
            }
            if (lastAutoHit.noteIndex == -1) {
                lastAutoHit.noteIndex = riff.notes.Count - 1;
                lastAutoHit.cycle--;
            }
        }
    }
    
}
