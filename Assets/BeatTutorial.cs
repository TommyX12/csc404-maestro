using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Zenject;

public class BeatTutorial : BasicAgent
{

    bool ready = false;
    Riff riff;
    private List<BeatTutorialDisplay> displays = new List<BeatTutorialDisplay>();
    private int noteNum;
    private int noteIndex;
    private bool[] hits;

    public GameObject start;
    public GameObject end;
    public GameObject pos;
    public GameObject displayPrefab;

    public AudioSource failSound;
    public AudioSource successSound;

    public UnityEvent onSuccess;

    private bool hitFirstNoteBeforeTime = false;

    // Injected references
    private GameplayModel model;

    // Start is called before the first frame update
    public void SetRiffToPlayerWeapon() {
        riff = CombatGameManager.current.player.GetCurrentWeapon().GetRiff();
        noteNum = riff.GetNotes().Count;
        noteIndex = 0;
        hits = new bool[noteNum];
        Vector3 diff = end.transform.position - start.transform.position;
        Quaternion rot = pos.transform.rotation;
        for (int i = 0; i < noteNum; i++) {
            GameObject display = GameObject.Instantiate(displayPrefab);
            displays.Add(display.GetComponent<BeatTutorialDisplay>());
            display.transform.position = start.transform.position + (diff / noteNum) * i;
            display.transform.rotation = rot;
            displays[i].SetState(2);
            display.transform.SetParent(transform);
        }

        Subscribe();

        GetComponent<Collider>().enabled = true;
        ready = true;
    }

    new void Start()
    {
        base.Start();
    }

    [Inject]
    public void Construct(GameplayModel model) {
        this.model = model;
    }

    void OnBeat(Riff.NoteHitEvent hit) {
        if (!hit.automatic) return;
        if (!this.enabled || hit.noteIndex == -1) {
            return;
        }

        displays[hit.noteIndex].Pulse();

        noteIndex = hit.noteIndex;
        if (hit.noteIndex == 0)
        {
            for (int i = 0; i < noteNum; i++)
            {
                if (!hits[i])
                {
                    // play fail sound
                    failSound.Play();
                    int j = 0;
                    foreach (BeatTutorialDisplay d in displays) {
                        d.SetState(2);
                        hits[j] = false;
                        j++;
                    }

                    if (hitFirstNoteBeforeTime) {
                        hits[0] = true;
                        displays[0].SetState(1);
                        hitFirstNoteBeforeTime = false;
                    }
                    
                    break;
                }

                if (hits[i] && i == noteNum - 1)
                {
                    // success
                    foreach (BeatTutorialDisplay d in displays)
                    {
                        d.SetState(2);
                        d.Pulse();
                    }
                    successSound.Play();
                    onSuccess.Invoke();
                    this.enabled = false;
                    CleanUp();
                    model.NotifyShowMoveTutorial();
                    GameObject.Destroy(gameObject, 1f);
                    return;
                }
            }
        }
        else {
            displays[noteIndex - 1].SetState(hits[noteIndex-1] ? 1 : 0);
        }

        hitFirstNoteBeforeTime = false;
    }

    private void Subscribe() {
        riff.delayedNoteHitEvent += OnBeat;
        riff.delayedNoteHitEvent += OnHit;
    }

    private void CleanUp() {
        riff.delayedNoteHitEvent -= OnBeat;
        riff.delayedNoteHitEvent -= OnHit;
    }

    private void OnHit(Riff.NoteHitEvent e) {
        if (e.automatic) return;
        if (e.noteIndex == -1 || !this.enabled || !this.ready)
        {
            return;
        }
        hits[e.noteIndex] = true;
        displays[e.noteIndex].SetState(1);

        if (e.noteIndex == 0 && e.deltaTime < 0) {
            hitFirstNoteBeforeTime = true;
        }
    }

    public override void ReceiveEvent(Event.Damage damage)
    {
        
    }
}
