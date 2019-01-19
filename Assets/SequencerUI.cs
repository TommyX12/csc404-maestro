using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencerUI : MonoBehaviour
{

    private Dictionary<int, GameObject> button_sprites = new Dictionary<int, GameObject>();

    public GameObject NotePrefab;

    public RectTransform MusicBar;

    public GameObject YouDied;

    public RectTransform hitZone;
    public RectTransform spawnZone;
    public RectTransform SpawnQ;
    public RectTransform SpawnW;
    public RectTransform SpawnE;
    public RectTransform SpawnR;

    public ParticleSystem QParticles;
    public ParticleSystem WParticles;
    public ParticleSystem EParticles;
    public ParticleSystem RParticles;

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

        int button = -1;

        if(Input.GetKeyDown(KeyCode.Q)) {
            Bhit = hcsee.ButtonPress(0);
            button = 0;
        } else 
        if(Input.GetKeyDown(KeyCode.W)) {
            Bhit = hcsee.ButtonPress(1);
            button = 1;
        } else 
        if(Input.GetKeyDown(KeyCode.E)) {
            Bhit = hcsee.ButtonPress(2);
            button = 2;
        } else 
        if(Input.GetKeyDown(KeyCode.R)) {
            Bhit = hcsee.ButtonPress(3);
            button = 3;
        }

        if(Bhit!=null) {
            Debug.Log(Bhit.spriteID);
            if(Bhit.spriteID != -1) {
                if(button_sprites.ContainsKey(Bhit.spriteID)) {
                    GameObject g = button_sprites[Bhit.spriteID];
                    button_sprites.Remove(Bhit.spriteID);
                    GameObject.Destroy(g);

                    ParticleSystem target;
                    switch(button) {
                    case 0:
                        target = QParticles;
                        break;
                    case 1:
                        target = WParticles;
                    break;
                    case 2:
                        target = EParticles;
                    break;
                    case 3:
                        target = RParticles;
                    break;
                    default:
                        target = QParticles;
                    break;
                    }
                    target.Emit((int)((1f-Bhit.deltaTime)*50));
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
            if(YouDied != null) {
                YouDied.SetActive(true);
            }
        }
    }

    public void StartMusicEventHandler(float offset) {
        track.Play();
    }

}
