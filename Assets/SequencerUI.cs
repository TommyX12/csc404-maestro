﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerUI : MonoBehaviour
{

    private Dictionary<int, GameObject> button_sprites = new Dictionary<int, GameObject>();

    public GameObject NotePrefab;

    public RectTransform MusicBar;

    public RectTransform hitZone;
    public RectTransform spawnZone;
    public RectTransform SpawnQ;
    public RectTransform SpawnW;
    public RectTransform SpawnE;
    public RectTransform SpawnR;

    public AudioSource track;

    private HardcodedSequenceEventEmitter hcsee;

    // units per sec
    public float ScrollSpeed;
    // units
    private float dist;

    public float GetScrollTime() {
        Debug.Log(ScrollSpeed == 0 ? -1f: dist/(ScrollSpeed));
        return ScrollSpeed == 0 ? -1f: dist/(ScrollSpeed);
    }

    public float GetTimeToMiss() {
        return GetScrollTime()*1.1f;
    }

    void FixedUpdate() {
        hcsee.Step(Time.fixedDeltaTime);
    }

    public void BeginSequence() {
        hcsee.Start();
    }

    void Start() {
        dist = spawnZone.anchoredPosition.x - hitZone.anchoredPosition.x;
        hcsee = new HardcodedSequenceEventEmitter(GetScrollTime());
        hcsee.addSpriteSpawnEventHandler(SpriteSpawnEventHandler);
        hcsee.addStartMusicEventHandler(StartMusicEventHandler);
        hcsee.addMissedBeatEventHandler(MissedBeatEventHandler);
        BeginSequence();
    }

    void Update() {
        SequenceEventEmitter.ButtonHitResult Bhit = null;
        if(Input.GetKeyDown(KeyCode.Q)) {
            Bhit = hcsee.ButtonPress(0);
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            Bhit = hcsee.ButtonPress(1);
        }
        if(Input.GetKeyDown(KeyCode.E)) {
            Bhit = hcsee.ButtonPress(2);
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            Bhit = hcsee.ButtonPress(3);
        }

        if(Bhit!=null) {
            Debug.Log(Bhit.spriteID);
            if(Bhit.spriteID != -1) {
                if(button_sprites.ContainsKey(Bhit.spriteID)) {
                    GameObject g = button_sprites[Bhit.spriteID];
                    button_sprites.Remove(Bhit.spriteID);
                    GameObject.Destroy(g);
                }
            }
        }
    }

    public void SpriteSpawnEventHandler(int buttonID, int spriteID, float offset) {
        RectTransform target;

        GameObject g = GameObject.Instantiate(NotePrefab);
        RectTransform gRect = g.GetComponent<RectTransform>();
        gRect.SetParent(MusicBar);
        switch(buttonID) {
            case 0:
                target = SpawnQ;
                g.GetComponent<NoteScroller>().txt.text = "Q";
            break;
            case 1:
                target = SpawnW;
                g.GetComponent<NoteScroller>().txt.text = "W";
            break;
            case 2:
                target = SpawnE;
                g.GetComponent<NoteScroller>().txt.text = "E";
            break;
            case 3:
                target = SpawnR;
                g.GetComponent<NoteScroller>().txt.text = "R";
            break;
            default:
                target = SpawnQ;
                g.GetComponent<NoteScroller>().txt.text = "Q";
            break;
        }
        gRect.anchoredPosition = target.anchoredPosition;
        gRect.sizeDelta = target.sizeDelta;
        gRect.GetComponent<NoteScroller>().dv = ScrollSpeed;
        button_sprites[spriteID] = g;
    }

    public void MissedBeatEventHandler(int spriteID, float offset) {
        if(button_sprites.ContainsKey(spriteID)) {
            GameObject g = button_sprites[spriteID];
            button_sprites.Remove(spriteID);
            GameObject.Destroy(g);
        }
    }

    public void StartMusicEventHandler(float offset) {
        track.Play();
    }

}
