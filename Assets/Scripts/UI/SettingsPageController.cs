using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Zenject;

public class SettingsPageController : MonoBehaviour {

    // Dragged references
    [SerializeField]
    private Text offsetText;

    // Fields
    private Riff riff;
    private float colorFactor = 0;

    // Injected references
    private GlobalConfiguration config;
    private MusicManager musicManager;

    [Inject]
    public void Construct(GlobalConfiguration config,
                          MusicManager musicManager) {
        this.config = config;
        this.musicManager = musicManager;
    }
    
    private void Awake() {
        Assert.IsNotNull(offsetText);
        
        List<Riff.Note> notes = new List<Riff.Note>() {
            new Riff.Note(0) {
                sound = "chord-1"
            }
        };
        riff = new Riff(1, notes, musicManager, config);
        riff.playing = true;
        Subscribe();
    }

    private void Subscribe() {
        riff.delayedNoteHitEvent += DelayedNoteHitEventHandler;
    }

    private void Unsubscribe() {
        riff.delayedNoteHitEvent -= DelayedNoteHitEventHandler;
    }

    private void DelayedNoteHitEventHandler(Riff.NoteHitEvent e) {
        if (e.automatic) {
            colorFactor = config.SettingsOffsetTextBeatColorDuration;
        }
    }

    private void OnDestroy() {
        Unsubscribe();
    }

    private void Start() {
        
    }

    private void Update() {
        riff.Update();
        if (colorFactor > 0) {
            colorFactor -= Time.deltaTime;
            if (colorFactor < 0) {
                colorFactor = 0;
            }
        }
        float interp = colorFactor / config.SettingsOffsetTextBeatColorDuration;
        offsetText.color = Color.Lerp(config.SettingsOffsetTextIdleColor, config.SettingsOffsetTextBeatColor, interp);
        offsetText.text = config.AudioDelay.ToString("0.00");
        float scaleFactor = 1.0f + interp * config.CalibrationScaleFactor;
        Vector3 scale = new Vector3(scaleFactor, scaleFactor, 1.0f);
        offsetText.transform.localScale = scale;

        config.SetGlobalAudioDelay(Mathf.Clamp(config.AudioDelay + Input.GetAxis("Horizontal") * Time.deltaTime * config.AudioDelayStep, config.AudioDelayMin, config.AudioDelayMax));
    }
}
