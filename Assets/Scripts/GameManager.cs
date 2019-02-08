using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager current;
    
    // references
    public MusicManager musicManager;
    public string ConfigName = "default-music-config";
    public string Track = "csc404-test-1";
    public int TrackBPM = 80;
    public Gladiator player;

    private Riff riff;

    protected void Awake() {
        GameManager.current = this;
        DontDestroyOnLoad(this);
    }
    
    protected void Start() {
        musicManager.LoadConfig(ConfigName);
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
        riff = new Riff(4, notes, musicManager);
        riff.noteHitEvent += noteHitEventHandler;

        musicManager.StartRiff(Track, TrackBPM);
        musicManager.PlayPattern("csc404-test-weapon-1", 4);
    }

    protected void FixedUpdate() {
        riff.Update();
    }

    protected void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Riff.NoteHitEvent result = riff.ButtonPress();
        }
    }

    private void noteHitEventHandler(Riff.NoteHitEvent e) {
        // Debug.Log("automatic: " + e.automatic + ", noteIndex: " + e.noteIndex + ", deltaTime: " + e.deltaTime);
    }
    
}
