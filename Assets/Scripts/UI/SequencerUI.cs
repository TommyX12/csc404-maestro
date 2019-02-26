using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SequencerUI : MonoBehaviour {

    // references
    public RectTransform pointerBar;
    public List<SequencerSequence> sequences; // 0: outer, 1: inner

    protected SequencerSequence innerSequence {
        get {
            return sequences[1];
        }
    }
    protected SequencerSequence outerSequence {
        get {
            return sequences[0];
        }
    }

    private RectTransform rectTransform;

    private MusicManager musicManager;

    private Riff riff1;
    private Riff riff2;

    private Canvas canvas;

    protected Vector3 playerPosition = new Vector3(0.5f, 0.5f, 10.0f);
    protected Vector3 sequencePosition = new Vector3(0.5f, 1.5f, 10.0f);
    protected float positionSmoothFriction = 0.7f;

    protected Riff outerRiff;
    protected Riff innerRiff;
    
    public SequencerUI() {
        
    }

    protected void Awake() {
        rectTransform = GetComponent<RectTransform>();
        
        musicManager = MusicManager.current;
        canvas = GetComponentInParent<Canvas>();
        
        pointerBar.gameObject.SetActive(true);

        innerSequence.SetVisible(false, true);
        outerSequence.SetVisible(false, true);

        innerSequence.SetColor(new Color(0.8f, 0.4f, 0.8f));
        outerSequence.SetColor(new Color(0.2f, 0.4f, 0.8f));
    }

    // public void SetRiff(Riff OuterRiff, Riff InnerRiff) {
    //     sequences[0].SetRiff(OuterRiff);
    //     sequences[0].SetColor(new Color(0.2f, 0.4f, 0.8f));
    //     sequences[1].SetRiff(InnerRiff);
    //     sequences[1].SetColor(new Color(0.8f, 0.4f, 0.8f));
    // }

    // protected void Start() {
    //     List<Riff.Note> notes = new List<Riff.Note>() {
    //         new Riff.Note(0, 2.0f),
    //         new Riff.Note(4, 2.0f),
    //         new Riff.Note(5, 2.0f),
    //     };
    //     riff1 = new Riff(4, notes, musicManager);
    //     sequences[0].SetRiff(riff1);
    //     sequences[0].SetColor(new Color(0.2f, 0.4f, 0.8f));
        
    //     notes = new List<Riff.Note>() {
    //         new Riff.Note(1, 1.0f),
    //         new Riff.Note(3, 1.0f),
    //     };
    //     riff2 = new Riff(4, notes, musicManager);
    //     sequences[1].SetRiff(riff2);
    //     sequences[1].SetColor(new Color(0.8f, 0.4f, 0.8f));
    // }

    protected void FixedUpdate() {
        if (CombatGameManager.current.player == null) {
            outerSequence.SetVisible(false);
            innerSequence.SetVisible(false);
            return;
        }

        var player = CombatGameManager.current.player;

        if (player) {
            // transform update
            Vector3 v = new Vector3();
            playerPosition = player.transform.position;
            // Agent target = player.GetTarget();

            playerPosition = Util.WorldToScreenAnchor(canvas, playerPosition);
            sequencePosition = Vector3.Lerp(sequencePosition, playerPosition, 1 - positionSmoothFriction);
            rectTransform.anchorMin = rectTransform.anchorMax = sequencePosition;

            // outer ring
            Weapon weapon = player.GetCurrentWeapon();
            Riff playerRiff = weapon ? weapon.GetRiff() : null;
            if (playerRiff != outerRiff) {
                if (outerRiff != null) {
                    outerRiff.playing = false;
                }

                if (playerRiff != null) {
                    outerSequence.SetRiff(playerRiff);
                    outerSequence.SetVisible(true);
                    playerRiff.playing = true;
                }
                else {
                    outerSequence.SetVisible(false);
                }

                outerRiff = playerRiff;
            }
            
            // inner ring
            Countermeasure countermeasure = player.GetCurrentCountermeasure();
            Riff targetRiff = countermeasure ? countermeasure.GetTargetRiff() : null;
            if (targetRiff != innerRiff) {
                if (innerRiff != null) {
                    innerRiff.playing = false;
                }

                if (targetRiff != null) {
                    innerSequence.SetRiff(targetRiff);
                    innerSequence.SetVisible(true);
                    targetRiff.playing = true;
                }
                else {
                    innerSequence.SetVisible(false);
                }

                innerRiff = targetRiff;
            }
        }
        else {
            innerSequence.SetVisible(false);
            outerSequence.SetVisible(false);
            outerRiff = null;
        }
    }

    protected void Update() {
        Vector3 eulerAngles = pointerBar.eulerAngles;
        eulerAngles.z = -360.0f * ((musicManager.GetBeatIndex(4)) / 4);
        pointerBar.eulerAngles = eulerAngles;
    }
}

