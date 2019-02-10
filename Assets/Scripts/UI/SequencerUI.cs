using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequencerUI : MonoBehaviour {

    // references
    public RectTransform pointerBar;
    public List<SequencerSequence> sequences;

    private RectTransform rectTransform;

    private MusicManager musicManager;

    private Riff riff1;
    private Riff riff2;

    private Canvas canvas;
    
    public SequencerUI() {
        
    }

    protected void Awake() {
        rectTransform = GetComponent<RectTransform>();
        
        musicManager = MusicManager.Current;
        canvas = GetComponentInParent<Canvas>();
        
        pointerBar.gameObject.SetActive(true);
    }

    protected void Start() {
        List<Riff.Note> notes = new List<Riff.Note>() {
            new Riff.Note(0, 2.0f),
            new Riff.Note(4, 2.0f),
            new Riff.Note(5, 2.0f),
        };
        riff1 = new Riff(4, notes, musicManager);
        sequences[0].SetRiff(riff1);
        sequences[0].SetColor(new Color(0.2f, 0.4f, 0.8f));
        
        notes = new List<Riff.Note>() {
            new Riff.Note(1, 1.0f),
            new Riff.Note(3, 1.0f),
        };
        riff2 = new Riff(4, notes, musicManager);
        sequences[1].SetRiff(riff2);
        sequences[1].SetColor(new Color(0.8f, 0.4f, 0.8f));
    }

    private void TrackPlayerPosition() {
        rectTransform.anchorMin = rectTransform.anchorMax = Util.WorldToScreenAnchor(canvas, CombatGameManager.current.player.transform.position);
    }

    protected void Update() {
        TrackPlayerPosition();
        
        Vector3 eulerAngles = pointerBar.eulerAngles;
        eulerAngles.z = -360.0f * ((musicManager.GetBeatIndex(4)) / 4);
        pointerBar.eulerAngles = eulerAngles;
        
        riff1.Update();
        riff2.Update();
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.E)) {
            riff1.ButtonPress();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            riff2.ButtonPress();
        }
    }
}

