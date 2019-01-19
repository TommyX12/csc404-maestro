using System;
using System.Collections.Generic;
using UnityEngine;

public class HardcodedSequenceEventEmitter : SequenceEventEmitter {

    private float musicStartTime;
    private bool musicStarted;
    private int nextNoteSpawnIndex;
    private int nextNoteHitIndex;

    protected float spawnToPressTime;
    protected float missBeatTime;
    protected float tooEarlyTime;
    protected float currentTime;

    protected class Note {
        public float time; // in terms of beats
        public int buttonID;

        public Note(float time, int buttonID) {
            this.time = time;
            this.buttonID = buttonID;
        }
    }

    protected List<Note> notes = new List<Note>() {
        new Note(0.076f, 0),
        new Note(0.244f, 0),
        new Note(0.616f, 0),
        new Note(0.791f, 1),
        new Note(0.965f, 2),
        new Note(1.302f, 0),
        new Note(1.477f, 0),
        new Note(1.657f, 0),
        new Note(1.832f, 1),
        new Note(2.018f, 2),
    };

    public HardcodedSequenceEventEmitter
        (float spawnToPressTime, float missBeatTime = 0.10f, float tooEarlyTime = 0.20f)
    {
        this.spawnToPressTime = spawnToPressTime;
        this.missBeatTime = missBeatTime;
        this.tooEarlyTime = tooEarlyTime;
    }

    override public void Start() {
        // have to have at least one note in notes
        
        currentTime = 0.0f;
        nextNoteSpawnIndex = 0;
        nextNoteHitIndex = 0;
        musicStarted = false;
        
        // compute how late to start the music, or start it now
        musicStartTime = spawnToPressTime - notes[0].time;
        if (musicStartTime < 0) {
            musicStartTime = 0.0f;
            emitStartMusicEvent(0.0f);
            musicStarted = true;
        }
    }

    override public void Step(float deltaTime) {
        currentTime += deltaTime;

        // start music if need to
        if (!musicStarted && currentTime >= musicStartTime) {
            emitStartMusicEvent(currentTime - musicStartTime);
            musicStarted = true;
        }

        // spawning notes
        while (true) {
            if (nextNoteSpawnIndex < notes.Count) {
                break;
            }

            Note nextNote = notes[nextNoteSpawnIndex];
            float spawnTime = nextNote.time + musicStartTime - spawnToPressTime;
            if (currentTime < spawnTime) {
                break;
            }

            emitSpriteSpawnEvent(nextNote.buttonID,
                                 nextNoteSpawnIndex,
                                 currentTime - spawnTime);

            nextNoteSpawnIndex++;
        }

        // checking missed notes
        while (true) {
            if (nextNoteHitIndex < notes.Count) {
                break;
            }

            Note nextNote = notes[nextNoteHitIndex];
            float missTime = nextNote.time + musicStartTime + missBeatTime;
            if (currentTime < missTime) {
                break;
            }

            emitMissedBeatEvent(nextNoteHitIndex,
                                currentTime - missTime);

            nextNoteHitIndex++;
        }
    }

    override protected ButtonHitResult ButtonPress(int buttonID) {
        ButtonHitResult result = new ButtonHitResult();

        if (nextNoteHitIndex < notes.Count) {
            result.spriteID = -1;
        }
        else {
            Note nextNote = notes[nextNoteHitIndex];
            float hitTime = nextNote.time + musicStartTime;
            if (currentTime < hitTime - tooEarlyTime) {
                result.spriteID = -1;
            }
            else {
                result.spriteID = nextNoteHitIndex;
                result.deltaTime = currentTime - hitTime;
                result.buttonCorrect = buttonID == nextNote.buttonID;
                nextNoteHitIndex++;
            }
        }

        return result;
    }
    
}
